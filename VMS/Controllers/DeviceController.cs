using Microsoft.AspNetCore.Mvc;
using System.Linq;

using VMS.Models;
using VMS.Data;
using VMS.Models.DTO;
using VMS.Repository.IRepository;

namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeviceController(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        [HttpGet("get-device-id-name")]
        public async Task<IEnumerable<GetDeviceIdAndNameDto>> GetItems()
        {
            return await _deviceRepository.GetDevicesAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Device>> PostDevice(AddNewDeviceDto deviceDto)
        {
            try
            {
                var device = await _deviceRepository.AddDeviceAsync(deviceDto);
                return CreatedAtAction(nameof(PostDevice), new { id = device.DeviceId }, device);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
}

