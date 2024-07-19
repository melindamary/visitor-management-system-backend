using Microsoft.AspNetCore.Mvc;
using System.Linq;

using VMS.Models;
using VMS.Data;
using VMS.Models.DTO;

namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DeviceController : ControllerBase
    {
        private VisitorManagementDbContext _context;
        public DeviceController(VisitorManagementDbContext _context)
        {
            this._context = _context;

        }

        [HttpGet("get-device-id-name")]
        public IEnumerable<GetDeviceIdAndNameDto> GetItems()
        {
            return _context.Devices
                   .Select(d => new GetDeviceIdAndNameDto
                   {
                       DeviceId = d.DeviceId,
                       DeviceName = d.DeviceName
                   })
                   .ToList();

        }

        [HttpPost]
        public async Task<ActionResult<Device>> PostDevice(AddNewDeviceDto deviceDto)
        {
            if (_context.Devices.Any(p => p.DeviceName == deviceDto.deviceName))
            {
                return Conflict(new { message = "Purpose already exists" });
            }

            var device = new Device
            {
                DeviceName = deviceDto.deviceName,
                CreatedBy = 1,
                UpdatedBy = 1,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostDevice), new { id = device.DeviceId }, device);
        }
    }
}

