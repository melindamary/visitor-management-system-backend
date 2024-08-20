using VMS.Models.DTO;
using VMS.Models;

namespace VMS.Services.IServices
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationIdAndNameDTO>> GetLocationIdAndNameAsync();
        Task<IEnumerable<LocationDetailsDTO>> GetAllLocationDetailsAsync();
        Task<bool> AddLocationAsync(AddOfficeLocationDTO locationDto);
        Task<bool> UpdateLocationAsync(int id, UpdateLocationDTO updateDto);
    }
}
