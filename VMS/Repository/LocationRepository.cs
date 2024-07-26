using Microsoft.EntityFrameworkCore;
using VMS.Data;
using VMS.Models.DTO;
using VMS.Repository.IRepository;

namespace VMS.Repository
{
    public class LocationRepository : IlocationRepository
    {
        private readonly VisitorManagementDbContext _context;
        public LocationRepository(VisitorManagementDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<LocationIdAndNameDTO>> GetLocationIdAndNameAsync()
        {
            return await _context.OfficeLocations
                .Select(d => new LocationIdAndNameDTO
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .ToListAsync();
        }
    }
}
