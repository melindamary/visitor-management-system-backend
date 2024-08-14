using VMS.Models.DTO;

namespace VMS.Services.IServices
{
    public interface IDeviceService
    {
        Task<IEnumerable<DeviceDTO>> GetDeviceListAsync();
        Task<bool> DeleteDeviceAsync(int id);

        Task<bool> UpdateDeviceAsync(DeviceUpdateRequestDTO updateDeviceRequestDTO);
    }
}
