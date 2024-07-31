using VMS.Models;
using VMS.Models.DTO;

namespace VMS.Repository.IRepository
{
    public interface ILocationRepository
    {
        Task<IEnumerable<LocationIdAndNameDTO>> GetLocationIdAndNameAsync();
        Task<IEnumerable<LocationDetailsDTO>> GetAllLocationDetailsAsync();
        Task<bool> AddLocationAsync(AddOfficeLocationDTO locationdDTO);
        Task<bool> UpdateLocationAsync(int id, UpdateLocationDTO locationdDTO);
        Task<List<OfficeLocation>> GetAllLocationAsync();
        Task<OfficeLocation> GetLocationByIdAsync(int officeLocationId);
    }
}
