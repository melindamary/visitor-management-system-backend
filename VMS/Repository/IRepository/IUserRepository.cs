using VMS.Models;
using VMS.Models.DTO;

namespace VMS.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<bool> ValidateUserAsync(string username, string password);
        Task AddUserAsync(User user);


    }
}
