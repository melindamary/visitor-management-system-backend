using Microsoft.EntityFrameworkCore;
using VMS.Data;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository.IRepository;

namespace VMS.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly VisitorManagementDbContext _context;

        public UserRepository(VisitorManagementDbContext context) { 
            _context = context;
        }

      

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var user = await GetUserByUsernameAsync(username);
            if (user == null) return false;
            else if(password == user.Password) return true;
            else return false;
            // Assume a method to verify password hash
            /*return VerifyPasswordHash(password, user.PasswordHash);*/


        }
        private bool VerifyPasswordHash(string password, string storedHash)
        {
            // Implement your hash verification logic here
            return true;
        }
    }
}
