using VMS.Models.DTO;

namespace VMS.Services.IServices
{
    public interface IDeviceService
    {
        Task<IEnumerable<DeviceDTO>> GetDeviceListAsync();
        Task<bool> UpdateDeviceAsync(DeviceUpdateRequestDTO updateDeviceRequestDTO);

        Task<bool> UpdateDeviceStatusAsync(DeviceStatusUpdateRequestDTO updateDeviceStatusRequestDTO);
    }
}
