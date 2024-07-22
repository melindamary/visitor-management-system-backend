
using VMS.Models;
namespace VMS.Repository.IRepository
{
    public interface IRoleRepository
    {
        Task<Roles> GetRoleByIdAsync(int roleId);
    }
}
