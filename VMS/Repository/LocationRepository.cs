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
        public async Task<IEnumerable<GetLocationIdAndNameDTO>> GetLocationIdAndNameAsync()
        {
            return await _context.OfficeLocations
                .Select(d => new GetLocationIdAndNameDTO
                {
                    Id = d.OfficeLocationId,
                    Name = d.LocationName
                })
                .ToListAsync();
        }
    }
}
