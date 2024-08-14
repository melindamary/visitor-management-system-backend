using VMS.Models.DTO;
using VMS.Repository.IRepository;
using VMS.Services.IServices;

namespace VMS.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _repository;
        public DeviceService(IDeviceRepository repository)
        {
            _repository = repository;   
        }

        public async Task<bool> DeleteDeviceAsync(int id)
        {
            return await _repository.DeleteDeviceAsync(id);
        }

        public async Task<IEnumerable<DeviceDTO>> GetDeviceListAsync()
        {
            return await _repository.GetDeviceListAsync();
        }

        public async Task<bool> UpdateDeviceAsync(DeviceUpdateRequestDTO updateDeviceRequestDTO)
        {
            return await _repository.UpdateDeviceAsync(updateDeviceRequestDTO);
        }
    }
}
