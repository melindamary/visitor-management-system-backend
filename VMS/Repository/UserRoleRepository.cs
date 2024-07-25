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

        public Task<UserRole> AddVisitorDeviceAsync(AddUserRoleDTO addUserRoleDto)
        {
            throw new NotImplementedException();
        }

        public async Task<UserRole> GetUserRoleByUserIdAsync(int userId)
        {
            return await _context.UserRoles.SingleOrDefaultAsync(r => r.UserId == userId);
        }
    }
}
