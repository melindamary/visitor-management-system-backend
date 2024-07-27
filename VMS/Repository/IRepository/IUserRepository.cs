using VMS.Models;
using VMS.Models.DTO;

namespace VMS.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<bool> ValidateUserAsync(string username, string password);
        // Task<User> AddNewUserAsync(AddNewUserDTO addNewUserDTO);
        Task UpdateLoggedInStatusAsync(string username);

        Task<LocationIdAndNameDTO> GetUserLocationAsync(int userId);
        Task AddUserAsync(User user);
        Task<User> GetUserByIdAsync(int userId);
        Task<List<User>> GetAllUsersAsync();
        Task UpdateUserAsync(User user);


    }
}
