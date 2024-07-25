using VMS.Models;

namespace VMS.Repository.IRepository
{
    public interface IUserLocationRepository
    {
        Task AddUserLocationAsync(UserLocation userLocation);
    }
}
