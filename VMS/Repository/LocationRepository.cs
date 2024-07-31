using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VMS.Data;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository.IRepository;

namespace VMS.Repository
{
    public class LocationRepository : ILocationRepository
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

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == locationdDTO.Username);
            if (user != null)
            {
                newLocation.CreatedBy = user.Id;
            }
            else
            {
                newLocation.CreatedBy = 1;
            }
            newLocation.CreatedDate = DateTime.Now;

            _context.OfficeLocations.Add(newLocation);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateLocationAsync(int id, UpdateLocationDTO updateDto)
        {
            var location = await _context.OfficeLocations.FindAsync(id);
            if (location == null)
            {
                return false; // Location not found
            }

            _mapper.Map(updateDto, location);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == updateDto.Username);
            if (user != null)
            {
                location.UpdatedBy = user.Id;
            }
            else
            {
                location.UpdatedBy = 1; 
            }
            location.UpdatedDate = DateTime.Now;

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
