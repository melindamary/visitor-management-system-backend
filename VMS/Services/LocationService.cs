using AutoMapper;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository;
using VMS.Repository.IRepository;
using VMS.Services.IServices;

namespace VMS.Services
{
    public class LocationService: ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public LocationService(ILocationRepository locationRepository, IUserRepository userRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<LocationIdAndNameDTO>> GetLocationIdAndNameAsync()
        {
            return await _locationRepository.GetLocationIdAndNameAsync();
        }
        public async Task<IEnumerable<LocationDetailsDTO>> GetAllLocationDetailsAsync()
        {
            return await _locationRepository.GetAllLocationDetailsAsync();
        }
        public async Task<bool> AddLocationAsync(AddOfficeLocationDTO locationDto)
        {
            return await _locationRepository.AddLocationAsync(locationDto);
        }

        public async Task<bool> UpdateLocationAsync(int id, UpdateLocationDTO updateDto)
        {
            var location = await _locationRepository.GetLocationByIdAsync(id);
            if (location == null) 
            {
                return false;
            }

            return await _locationRepository.UpdateLocationAsync(id, updateDto);
        }
    }
}
