using VMS.Models.DTO;

namespace VMS.Repository.IRepository
{
    public interface IlocationRepository
    {
        Task<IEnumerable<LocationIdAndNameDTO>> GetLocationIdAndNameAsync();
        Task<IEnumerable<LocationDetailsDTO>> GetAllLocationDetailsAsync();
        Task<bool> AddLocationAsync(AddOfficeLocationDTO locationdDTO);
        Task<bool> UpdateLocationAsync(int id, UpdateLocationDTO locationdDTO);
    }
}
