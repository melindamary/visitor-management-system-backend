using Microsoft.EntityFrameworkCore;
using VMS.Data;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository.IRepository;

namespace VMS.Repository
{
    public class DeviceRepository : IDeviceRepository
    {
        
        private readonly VisitorManagementDbContext _context;
        public const int _systemUserId = 1;
        public const int _defaultDeviceStatus = 0;

        public DeviceRepository(VisitorManagementDbContext context)
        {
            _context = context;
        }
        public async Task<Device> AddDeviceAsync(AddNewDeviceDTO deviceDto)
        {
           /* if (_context.Devices.Any(d => d.Name == deviceDto.deviceName))
            {
                throw new InvalidOperationException("Device already exists");
            }*/

            var device = new Device
            {
                Name = deviceDto.deviceName,
                CreatedBy = _systemUserId,
                UpdatedBy = _systemUserId,
                Status = _defaultDeviceStatus,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            return device;
        }

        public async Task<IEnumerable<GetDeviceIdAndNameDTO>> GetDevicesAsync()
        {
           
            return await _context.Devices.Where(d => d.Status == 1)
                .Select(d => new GetDeviceIdAndNameDTO
                {
                    DeviceId = d.Id,
                    DeviceName = d.Name
                })
                .ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
