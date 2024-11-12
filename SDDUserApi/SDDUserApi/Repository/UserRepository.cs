using Microsoft.EntityFrameworkCore;
using SDDUserApi.Data.DBConfiguration;
using SDDUserApi.Data.Model;

namespace SDDUserApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.Where(s => s.IsActive == true).ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.Where(u => u.UserId == id && u.IsActive == true)
            .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string EmailId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.EmailId == EmailId && u.IsActive == true);
        }

        public async Task<int> AddUserAsync(User user)
        {
            if (user != null)
            {
                user.UserId = 0;
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.UserId;
        }

        public async Task<int> UpdateUserAsync(User user)
        {           
            _context.Users.Update(user);
            return await _context.SaveChangesAsync();
        }        
    }
}
