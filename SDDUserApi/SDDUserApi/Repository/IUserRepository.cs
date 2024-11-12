using SDDUserApi.Data.Model;

namespace SDDUserApi.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string EmailId);
        Task<int> AddUserAsync(User user);
        Task<int> UpdateUserAsync(User user);
        //Task DeleteUserAsync(int id);
    }
}
