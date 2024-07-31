using VMS.Models;
using VMS.Models.DTO;

namespace VMS.Repository.IRepository
{
    public interface IDeviceRepository
    {
        Task<IEnumerable<GetDeviceIdAndNameDTO>> GetDevicesAsync();
        Task<Device> AddDeviceAsync(AddNewDeviceDTO deviceDto);
        Task SaveAsync();
    }
}
