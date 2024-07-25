using Microsoft.EntityFrameworkCore;
using VMS.Data;
using VMS.Models;
using VMS.Models.DTO;
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
        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            return await _context.Roles.FirstOrDefaultAsync(u => u.Id == roleId);
        }

        public async Task<IEnumerable<GetRoleIdAndNameDTO>> GetRoleIdAndNameAsync()
        {

            return await _context.Roles
                .Select(d => new GetRoleIdAndNameDTO
                {
                    Id = d.Id,
                   Name = d.Name
                })
                .ToListAsync();
        }

       
    }
}
