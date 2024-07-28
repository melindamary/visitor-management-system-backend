using VMS.Models;
using VMS.Models.DTO;

namespace VMS.Repository.IRepository
{
    public interface IlocationRepository
    {
        Task<IEnumerable<LocationIdAndNameDTO>> GetLocationIdAndNameAsync();
        Task<List<OfficeLocation>> GetAllLocationAsync();
        Task<OfficeLocation> GetLocationByIdAsync(int officeLocationId);
    }
}
