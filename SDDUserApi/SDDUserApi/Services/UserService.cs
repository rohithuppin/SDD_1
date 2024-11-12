using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SDDUserApi.Data.DTO;
using SDDUserApi.Data.Model;
using SDDUserApi.Repository;

namespace SDDUserApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return null;

            return _mapper.Map<UserDto>(user);
        }

        public async Task<User> GetUserByUsernameAsync(string EmailId)
        {
            var user = await _userRepository.GetUserByUsernameAsync(EmailId);
            if (user == null) return null;

            return user;
        }

        //public async Task AddUserAsync(UserDto userDto, string confirmpassword)
        //{
        //    var user = _mapper.Map<User>(userDto, s => s.Items["ConfirmPassword"] = confirmpassword);                       
        //    await _userRepository.AddUserAsync(user);
        //}

        public async Task<int> AddUserAsync(UserDto userDto, string hashPwd)
        {
            var user = _mapper.Map<User>(userDto);
            user.PasswordHash = hashPwd;
            return await _userRepository.AddUserAsync(user);
        }

        public async Task<int> UpdateUserAsync(int id, UserDto userDto)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser != null)
            {
                var updatedUser = _mapper.Map(userDto, existingUser);
                return await _userRepository.UpdateUserAsync(updatedUser);
            }
            return 0;
        }

        public async Task<int> DeleteUserAsync(int id)
        {            
            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser != null)
            {
                existingUser.IsActive = false;
                return await _userRepository.UpdateUserAsync(existingUser);
            }
            return 0;
        }
    }
}
