using VMS.Models;

namespace VMS.Repository.IRepository
{
    public interface IUserRoleRepository
    {
        Task<UserRole> GetUserRoleByUserIdAsync(int userId);
    }
}
