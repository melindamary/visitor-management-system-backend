using VMS.Models;

namespace VMS.Repository.IRepository
{
    public interface IUserDetailsRepository
    {
        Task AddUserDetailAsync(UserDetail userDetail);
    }
}
