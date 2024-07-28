using VMS.Controllers;
using VMS.Models.DTO;
using VMS.Models;
using System.Data;


namespace VMS.Repository.IRepository
{
    public interface IAdminRoleRepository
    {

            // Role-related methods
            Task<IEnumerable<GetRoleIdAndNameDTO>> GetRoleIdAndNameAsync();
            Task<IEnumerable<Page>> GetPagesByRoleIdAsync(int roleId); // Add this method

        // Page-related methods
        Task<IEnumerable<Page>> GetPagesAsync();
            Task<Role> GetRolesAsync(int id);
            Task DeleteRoleAsync(int id);
            Task<Page> GetPageByIdAsync(int id);
            Task<Page> CreatePageAsync(PageDTO pageDto);
            Task UpdatePageAsync(int id, PageDTO pageDto);
            Task DeletePageAsync(int id);
            Task<Role> CreateRoleAsync(AddNewRoleDTO roleDTO);


        // PageControl-related methods
        Task<Role> GetRoleByIdAsync(int roleId);
        Task AddPageControlsAsync(int roleId, List<AddPageControlDTO> pageControls);
        Task UpdateRolePagesAsync(UpdateRolePagesDTO updateRolePagesDTO);

    }

}
