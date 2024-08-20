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

        public async Task<bool> DeleteDeviceAsync(int id)
        {
            var device = await _context.Devices.FirstOrDefaultAsync(u => u.Id == id);
            if (device == null)
            {
                return false;
            }

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DeviceDTO>> GetDeviceListAsync()
        {
            var devices = await(from device in _context.Devices
                                    join user in _context.UserDetails
                                    on device.UpdatedBy equals user.UserId into userGroup
                                    from user in userGroup.DefaultIfEmpty()
                                    select new DeviceDTO
                                    {
                                        Id = device.Id,
                                        Name = device.Name,
                                        Status = device.Status,
                                        CreatedBy = device.CreatedBy,
                                        UpdatedBy = user != null ? user.FirstName + " " + user.LastName : null,
                                        CreatedDate = device.CreatedDate,
                                        UpdatedDate = device.UpdatedDate

                                    }).ToListAsync();   
            return devices;

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

        public async Task<bool> UpdateDeviceAsync(DeviceUpdateRequestDTO updateDeviceRequestDTO)
        {
            var device = await _context.Devices.FindAsync(updateDeviceRequestDTO.Id);
            if (device == null)
            {
                return false;
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == updateDeviceRequestDTO.Username);
            Console.WriteLine(updateDeviceRequestDTO.Username);
            device.Name = updateDeviceRequestDTO.Device;
            device.UpdatedBy = user.Id;
            device.UpdatedDate = DateTime.Now;
            device.Status = 1;

            _context.Devices.Update(device);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
