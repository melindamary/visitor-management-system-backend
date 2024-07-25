using Microsoft.EntityFrameworkCore;
using VMS.Models;
using VMS.Repository.IRepository;

using VMS.Data;

namespace VMS.Repository
{
    public class UserDetailsRepository : IUserDetailsRepository
    {
        private readonly VisitorManagementDbContext _context;
        public UserDetailsRepository(VisitorManagementDbContext context)
        {
            _context = context;
        }
        public async Task AddUserDetailAsync(UserDetail userDetail)
        {
            _context.UserDetails.Add(userDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserDetail>> GetAllUserDetailsAsync()
        {
            return await _context.UserDetails.ToListAsync();
        }

        public async Task<UserDetail> GetUserDetailByUserIdAsync(int userId)
        {
            return await _context.UserDetails.FirstOrDefaultAsync(ud => ud.UserId == userId);

        }

        public async Task UpdateUserDetailAsync(UserDetail userDetail)
        {
            _context.UserDetails.Update(userDetail);
            await _context.SaveChangesAsync();
        }
    }
}
