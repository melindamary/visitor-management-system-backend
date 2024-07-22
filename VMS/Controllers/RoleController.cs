using Microsoft.AspNetCore.Mvc;
using System.Linq;

using VMS.Models;
using VMS.Data;
using VMS.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Data;
namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RoleController : ControllerBase
    {

        private VisitorManagementDbContext _context;
        public RoleController(VisitorManagementDbContext _context)
        {
            this._context = _context;
        }
        [HttpGet]
        public IEnumerable<Role> GetRoles() { 
            return _context.Roles.ToList();
        }



        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(AddNewRoleDTO roleDTO)
        {
            if (_context.Roles.Any(p => p.RoleName == roleDTO.Name))
            {
                return Conflict(new { message = "role already exists" });

            }
            var role = new Role
            {
                RoleName = roleDTO.Name,
                CreatedBy = roleDTO.CreatedBy,
                UpdatedBy = roleDTO.UpdatedBy,

                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(PostRole), new { id = role.RoleId, role});
        }


        [HttpPost]

        public async Task<ActionResult<Role>> PostPage_Control(AddNewRoleDTO roleDTO)
        {
            if (_context.Roles.Any(p => p.RoleName == roleDTO.Name))
            {
                return Conflict(new { message = "role already exists" });

            }
            var role = new Role
            {
                RoleName = roleDTO.Name,
                CreatedBy = roleDTO.CreatedBy,
                UpdatedBy = roleDTO.UpdatedBy,

                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(PostRole), new { id = role.RoleId, role });
        }

    }
   
}
