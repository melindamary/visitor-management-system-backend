using Microsoft.EntityFrameworkCore;
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

        public async Task<List<UserLocation>> GetAllUserLocationsAsync()
        {
            return await _context.UserLocations.ToListAsync();
        }

        public async Task<UserLocation> GetUserLocationByUserIdAsync(int userId)
        {
            return await _context.UserLocations.FirstOrDefaultAsync(ul => ul.UserId == userId);

        }

        public async Task UpdateUserLocationAsync(UserLocation userLocation)
        {
            _context.UserLocations.Update(userLocation);
            await _context.SaveChangesAsync();
        }
    }
}
