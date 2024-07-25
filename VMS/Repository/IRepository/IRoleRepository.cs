using VMS.Models.DTO;
using VMS.Models;
namespace VMS.Repository.IRepository
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleByIdAsync(int roleId);
        Task<IEnumerable<GetRoleIdAndNameDTO>> GetRoleIdAndNameAsync();
    }
}
