using VMS.Models.DTO;

namespace VMS.Repository.IRepository
{
    public interface IDeviceRepository
    {
        Task<IEnumerable<GetDeviceIdAndNameDto>> GetDevicesAsync();
        Task<Device> AddDeviceAsync(AddNewDeviceDto deviceDto);
        Task SaveAsync();
    }
}
