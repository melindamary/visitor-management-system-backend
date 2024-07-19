using VMS.Models;

namespace VMS.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<bool> ValidateUserAsync(string username, string password);
    }
}
