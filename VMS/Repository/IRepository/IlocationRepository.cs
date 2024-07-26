using VMS.Models.DTO;

namespace VMS.Repository.IRepository
{
    public interface IlocationRepository
    {
        Task<IEnumerable<LocationIdAndNameDTO>> GetLocationIdAndNameAsync();
    }
}
