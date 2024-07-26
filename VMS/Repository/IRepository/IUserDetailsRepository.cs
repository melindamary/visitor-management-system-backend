using VMS.Models;

namespace VMS.Repository.IRepository
{
    public interface IUserDetailsRepository
    {
        Task AddUserDetailAsync(UserDetail userDetail);
        Task<UserDetail> GetUserDetailByUserIdAsync(int userId);
        Task<List<UserDetail>> GetAllUserDetailsAsync();
        Task UpdateUserDetailAsync(UserDetail userDetail);
    }
}
