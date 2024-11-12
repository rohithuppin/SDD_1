using SDDUserApi.Data.DTO;
using SDDUserApi.Data.Model;

namespace SDDUserApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string EmailId);
        Task<int> AddUserAsync(UserDto userDto, string hashPwd);
        //Task AddUserAsync(UserDto userDto, string confirmpassword);
        Task<int> UpdateUserAsync(int id, UserDto userDto);
        Task<int> DeleteUserAsync(int id);
    }
}
