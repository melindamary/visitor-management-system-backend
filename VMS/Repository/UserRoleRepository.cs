using Microsoft.EntityFrameworkCore;
using VMS.Data;
using VMS.Models;
using VMS.Repository.IRepository;

namespace VMS.Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly VisitorManagementDbContext _context;
        public UserRoleRepository(VisitorManagementDbContext context) {
            _context = context;
        }
        public async Task<UserRole> GetUserRoleByUserIdAsync(int userId)
        {
            return await _context.UserRoles.SingleOrDefaultAsync(r => r.UserId == userId);
        }
    }
}
