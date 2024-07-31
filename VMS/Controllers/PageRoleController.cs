/*using Microsoft.AspNetCore.Mvc;
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
    public class PageRoleController : ControllerBase
    {
        private readonly VisitorManagementDbContext _context;

        public PageRoleController(VisitorManagementDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> CreatePageControls(int roleId, List<PageControlDTO> pageControls)
        {

            try
            {
                var role = await _context.Roles.FindAsync(roleId);
                if (role == null)
                {
                    return NotFound($"Role with ID {roleId} not found.");
                }

                foreach (var control in pageControls)
                {
                    var pageControl = new PageControl
                    {
                        RoleId = roleId,
                        PageId = control.PageId,
                        CreatedBy = 1, // Replace with actual user ID
                        UpdatedBy = 1, // Replace with actual user ID
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    };
                    _context.PageControls.Add(pageControl);
                }
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }

    public class PageControlDTO
    {
        public int PageId { get; set; }
    }

}
*/