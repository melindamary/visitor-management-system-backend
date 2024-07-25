using VMS.Data;
using VMS.Models;
using VMS.Repository.IRepository;
namespace VMS.Repository
{
    public class UserLocationRepository : IUserLocationRepository
    {
        private readonly VisitorManagementDbContext _context;
        public UserLocationRepository(VisitorManagementDbContext context)
        {
            this._context = context;
            
        }
        public async Task AddUserLocationAsync(UserLocation userLocation)
        {
            _context.UserLocations.Add(userLocation);
            await _context.SaveChangesAsync();
        }
    }
}
