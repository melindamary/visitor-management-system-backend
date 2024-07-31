using Microsoft.AspNetCore.Mvc;
using VMS.Models.DTO;
using VMS.Repository.IRepository;
using VMS.Models;
using System.Data;

namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AdminRoleController : ControllerBase
    {
        private readonly IAdminRoleRepository _adminRoleRepository;

        public AdminRoleController(IAdminRoleRepository adminRoleRepository)
        {
            _adminRoleRepository = adminRoleRepository;
        }

        // Role-related actions
        [HttpGet("get-role-id-name")]
        public async Task<IEnumerable<GetRoleIdAndNameDTO>> GetRoleIdAndName()
        {
            return await _adminRoleRepository.GetRoleIdAndNameAsync();
        }

        [HttpGet("get-role-by-id/{id}")]
        public async Task<ActionResult<Role>> Getroles(int id)
        {
            var role = await _adminRoleRepository.GetRolesAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return role;
        }

        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(int roleId)
        {
            try
            {
                await _adminRoleRepository.DeleteRoleAsync(roleId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // Page-related actions
        [HttpGet("get-pages")]
        public async Task<IEnumerable<Page>> GetPages()
        {
            return await _adminRoleRepository.GetPagesAsync();
        }

        [HttpGet("get-page/{id}")]
        public async Task<ActionResult<Page>> GetPage(int id)
        {
            var page = await _adminRoleRepository.GetPageByIdAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            return page;
        }
        [HttpGet("{roleId}")]
        public async Task<ActionResult<IEnumerable<Page>>> GetPagesByRoleId(int roleId)
        {
            var pages = await _adminRoleRepository.GetPagesByRoleIdAsync(roleId);
            if (pages == null || !pages.Any())
            {
                return NotFound();
            }
            return Ok(pages);
        }

        [HttpPost("create-page")]
        public async Task<ActionResult<Page>> CreatePage(PageDTO pageDto)
        {
            var page = await _adminRoleRepository.CreatePageAsync(pageDto);
            return CreatedAtAction(nameof(GetPage), new { id = page.Id }, page);
        }


        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(AddNewRoleDTO roleDTO)
        {
            try
            {
                var role = await _adminRoleRepository.CreateRoleAsync(roleDTO);
                return CreatedAtAction(nameof(PostRole), new { id = role.Id }, role);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }



        }
        [HttpPost]
        public async Task<ActionResult> CreatePageControls(int roleId, List<AddPageControlDTO> pageControls, int status)
        {
            try
            {
                await _adminRoleRepository.AddPageControlsAsync(roleId, pageControls);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



        [HttpPatch]
        public async Task<IActionResult> UpdateRolePages(UpdateRolePagesDTO updateRolePagesDTO)
        {
            try
            {
                await _adminRoleRepository.UpdateRolePagesAsync(updateRolePagesDTO);
                return Ok(new { message = "Role pages updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }




    }
}
