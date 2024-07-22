using Microsoft.EntityFrameworkCore;
using VMS.Data;
using VMS.Models;
using VMS.Repository.IRepository;

namespace VMS.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly VisitorManagementDbContext _context;

        public RoleRepository(VisitorManagementDbContext context)
        {
            _context = context;
        }
        public async Task<Roles> GetRoleByIdAsync(int roleId)
        {
            return await _context.Roles.FirstOrDefaultAsync(u => u.RoleId == roleId);
        }
    }
}
