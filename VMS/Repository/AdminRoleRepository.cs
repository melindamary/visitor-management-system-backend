using VMS.Controllers;
using VMS.Data;
using VMS.Models.DTO;
using VMS.Models;
using Microsoft.EntityFrameworkCore;
using VMS.Repository.IRepository;
using System.Data;

namespace VMS.Repository
{
    public class AdminRoleRepository:IAdminRoleRepository
    {
        private readonly VisitorManagementDbContext _context;

        public AdminRoleRepository(VisitorManagementDbContext context)
        {
            _context = context;
        }

        // Role-related methods
        public async Task<IEnumerable<GetRoleIdAndNameDTO>> GetRoleIdAndNameAsync()
        {
/*            return await _context.Roles.Select(r => new GetRoleIdAndNameDTO { Id = r.Id, Name = r.Name }).ToListAsync();
*/
            return await _context.Roles.Select(r => new GetRoleIdAndNameDTO
            {
                Id = r.Id,
                Name = r.Name,
                CreatedBy = r.CreatedBy,
                CreatedDate = r.CreatedDate
            }).ToListAsync();
        }
        public async Task<Role> GetRolesAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }
        public async Task DeleteRoleAsync(int roleId)
        {
            var role = await _context.Roles.Include(r => r.PageControls).FirstOrDefaultAsync(r => r.Id == roleId);

            if (role == null)
            {
                throw new Exception($"Role with ID {roleId} not found.");
            }

            _context.PageControls.RemoveRange(role.PageControls);
            _context.Roles.Remove(role);

            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Page>> GetPagesByRoleIdAsync(int roleId)
        {
            return await _context.PageControls
                .Where(pc => pc.RoleId == roleId)
                .Include(pc => pc.Page)
                .Select(pc => pc.Page)
                .ToListAsync();
        }

        // Page-related methods
        public async Task<IEnumerable<Page>> GetPagesAsync()
        {
            return await _context.Pages.ToListAsync();
        }

        public async Task<Page> GetPageByIdAsync(int id)
        {
            return await _context.Pages.FindAsync(id);
        }

        public async Task<Page> CreatePageAsync(PageDTO pageDto)
        {
            var page = new Page
            {
                Name = pageDto.PageName,
                Url = pageDto.PageUrl,
                CreatedBy = pageDto.CreatedBy,
                UpdatedBy = pageDto.UpdatedBy,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _context.Pages.Add(page);
            await _context.SaveChangesAsync();

            return page;
        }

        public async Task UpdatePageAsync(int id, PageDTO pageDto)
        {
            var page = await _context.Pages.FindAsync(id);
            if (page != null)
            {
                page.Name = pageDto.PageName;
                page.Url = pageDto.PageUrl;
                page.UpdatedBy = pageDto.UpdatedBy;
                page.UpdatedDate = DateTime.Now;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePageAsync(int id)
        {
            var page = await _context.Pages.FindAsync(id);
            if (page != null)
            {
                _context.Pages.Remove(page);
                await _context.SaveChangesAsync();
            }
        }


        // PageControl-related methods
        public async Task<Role> CreateRoleAsync(AddNewRoleDTO roleDTO)
        {
            if (await _context.Roles.AnyAsync(p => p.Name == roleDTO.Name))
            {
                throw new InvalidOperationException("Role already exists");
            }

            var role = new Role
            {
                Name = roleDTO.Name,
                CreatedBy = roleDTO.CreatedBy,
                UpdatedBy = roleDTO.UpdatedBy,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }
        // PageControl-related methods
        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            return await _context.Roles.FindAsync(roleId);
        }

        public async Task AddPageControlsAsync(int roleId, List<AddPageControlDTO> pageControls)
        {
            var role = await GetRoleByIdAsync(roleId);
            if (role == null)
            {
                throw new Exception($"Role with ID {roleId} not found.");
            }

            foreach (var control in pageControls)
            {
                var pageControl = new PageControl
                {
                    RoleId = roleId,
                    PageId = control.PageId.Value,
                    CreatedBy = 1, // Replace with actual user ID
                    UpdatedBy = 1, // Replace with actual user ID
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };
                _context.PageControls.Add(pageControl);
            }
            await _context.SaveChangesAsync();
        }
        public async Task UpdateRolePagesAsync(UpdateRolePagesDTO updateRolePagesDTO)
        {
            var role = await _context.Roles.FindAsync(updateRolePagesDTO.RoleId);
            if (role == null)
            {
                throw new Exception($"Role with ID {updateRolePagesDTO.RoleId} not found.");
            }

            var existingPageControls = _context.PageControls.Where(pc => pc.RoleId == updateRolePagesDTO.RoleId);
            _context.PageControls.RemoveRange(existingPageControls);

            foreach (var pageId in updateRolePagesDTO.PageIds)
            {
                var pageControl = new PageControl
                {
                    RoleId = updateRolePagesDTO.RoleId,
                    PageId = pageId,
                    CreatedBy = 1, // Replace with actual user ID
                    UpdatedBy = 1, // Replace with actual user ID
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };
                _context.PageControls.Add(pageControl);
            }

            await _context.SaveChangesAsync();
        }

    }
}
