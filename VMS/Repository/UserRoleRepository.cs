using Microsoft.EntityFrameworkCore;
using VMS.Data;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository.IRepository;

namespace VMS.Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly VisitorManagementDbContext _context;
        public UserRoleRepository(VisitorManagementDbContext context) {
            _context = context;
        }

        public async Task AddUserRoleAsync(UserRole userRole)
        {
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserRole>> GetAllUserRolesAsync()
        {
            return await _context.UserRoles.ToListAsync();
        }

        public async Task<UserRole> GetUserRoleByUserIdAsync(int userId)
        {
            return await _context.UserRoles.SingleOrDefaultAsync(r => r.UserId == userId);
        }

        public async Task UpdateUserRoleAsync(UserRole userRole)
        {
            _context.UserRoles.Update(userRole);
            await _context.SaveChangesAsync();
        }
    }
}
