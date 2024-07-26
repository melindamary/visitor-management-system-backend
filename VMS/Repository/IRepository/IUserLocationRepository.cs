using VMS.Models;

namespace VMS.Repository.IRepository
{
    public interface IUserLocationRepository
    {
        Task AddUserLocationAsync(UserLocation userLocation);
        Task<UserLocation> GetUserLocationByUserIdAsync(int userId);
        Task<List<UserLocation>> GetAllUserLocationsAsync();
        Task UpdateUserLocationAsync(UserLocation userLocation);
    }
}
