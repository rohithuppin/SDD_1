using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SDDUserApi.Data.DTO;
using SDDUserApi.Data.Model;
using SDDUserApi.Services;
using SDDUserApi.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SDDUserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuditService _auditService;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger<UsersController> _logger;
        private readonly AuthService _authService;

        public UsersController(IUserService userService, IAuditService auditService, IConfiguration configuration, IPasswordHasher passwordHasher, ILogger<UsersController> logger, AuthService authService)
        {
            _userService = userService;
            _auditService = auditService;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
            _authService = authService;
            _logger = logger;
        }

        // GET: api/user
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            string token = Request.Headers["Authorization"].ToString();               
            bool validToken = _authService.ValidateUserToken(token.Trim());
            if (!validToken)
                return Unauthorized(new { message = "InvalidToken expired please login again" });

            _logger.LogInformation("Fetching user data.");
            _auditService.LogActivity(1,"READ", $"Get all user");
            var userDtos = await _userService.GetAllUsersAsync();
            return Ok(userDtos);
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            string token = Request.Headers["Authorization"].ToString();
            bool validToken = _authService.ValidateUserToken(token.Trim());
            if (!validToken)
                return Unauthorized(new { message = "InvalidToken expired please login again" });

            _logger.LogInformation("Fetching user id - {0} data.",id);
            _auditService.LogActivity(1, "READById", $"Get user by Id");
            var userDto = await _userService.GetUserByIdAsync(id);
            if (userDto == null)
                return NotFound();

            return Ok(userDto);
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            _logger.LogInformation("Fetching user name - {0} data.", model.EmailId);

            _auditService.LogActivity(1, "LOGIN", $"Authorization of user :" + model.EmailId);
            var user = await _userService.GetUserByUsernameAsync(model.EmailId);
            if (user == null)
            {
                _logger.LogWarning("No user exists- {0} data.", model.EmailId);
                return Unauthorized(new { message = "Invalid EmailId or password." });
            }            

            var match = _passwordHasher.VerifyPasswordHash(model.Password, user.PasswordHash);

            if (!match)
            {
                _logger.LogWarning("Password incorrect for user - {0}", model.EmailId);
                return Unauthorized(new { message = "Invalid EmailId or password." });
            }
            else
            {
                var tokenJWT = _authService.GenerateJwtToken(user);
                return Ok(new { Token = tokenJWT, User = user }); 
            }
        }               

        // POST: api/user
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
        {
            string token = Request.Headers["Authorization"].ToString();
            bool validToken = _authService.ValidateUserToken(token.Trim());
            if (!validToken)
                return Unauthorized(new { message = "InvalidToken expired please login again" });

            if (userDto == null)
                return BadRequest();
            _auditService.LogActivity(1, "CREATE", $"Create new user");

            var hashPwd = _passwordHasher.HashPassword(userDto.PasswordHash);//Default password for everyone during first time creation
            int _newUserId = await _userService.AddUserAsync(userDto,hashPwd);
            return Ok(new { newUserId = _newUserId });
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<int> UpdateUser(int id, [FromBody] UserDto userDto)
        {
            string token = Request.Headers["Authorization"].ToString();
            bool validToken = _authService.ValidateUserToken(token.Trim());
            if (!validToken)
                return 0;

            _auditService.LogActivity(1, "UPDATE", $"Update user");

            var existingUser = await _userService.GetUserByIdAsync(id);
            if (existingUser == null)
                return 0;
            userDto.UserId = id;

            return await _userService.UpdateUserAsync(id, userDto);            
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<int> DeleteUser(int id)
        {
            string token = Request.Headers["Authorization"].ToString();
            bool validToken = _authService.ValidateUserToken(token.Trim());
            if (!validToken)
                return 0;

            _auditService.LogActivity(1, "DELETE", $"Deleting user id - " + id);
            
            return await _userService.DeleteUserAsync(id);
        }
    }
}
