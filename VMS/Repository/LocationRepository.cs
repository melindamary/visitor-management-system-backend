using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VMS.Data;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository.IRepository;

namespace VMS.Repository
{
    public class LocationRepository : IlocationRepository
    {
        private readonly VisitorManagementDbContext _context;
        private readonly IMapper _mapper;
        public LocationRepository(VisitorManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LocationDetailsDTO>> GetAllLocationDetailsAsync()
        {
            var locations = await _context.OfficeLocations
                .ToListAsync();

            var locationDtos = _mapper.Map<List<LocationDetailsDTO>>(locations);
            return locationDtos;
        }

        public async Task<bool> AddLocationAsync(AddOfficeLocationDTO locationdDTO)
        {
            var newLocation = _mapper.Map<OfficeLocation>(locationdDTO);

            newLocation.CreatedDate = DateTime.Now;
            newLocation.CreatedBy = 1; // Replace with actual user ID

            _context.OfficeLocations.Add(newLocation);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateLocationAsync(int id, UpdateLocationDTO dto)
        {
            var location = await _context.OfficeLocations.FindAsync(id);
            if (location == null)
            {
                return false; // Location not found
            }

            _mapper.Map(dto, location);

            location.UpdatedDate = DateTime.Now;
            location.UpdatedBy = 1; // Replace with actual user ID

            _context.OfficeLocations.Update(location);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    

   
        public async Task<List<OfficeLocation>> GetAllLocationAsync()
        {
            return await _context.OfficeLocations.ToListAsync();
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
        

        public async Task<OfficeLocation> GetLocationByIdAsync(int officeLocationId)
        {
            return await _context.OfficeLocations.FirstOrDefaultAsync(u => u.Id == officeLocationId);
        }
    }
}
