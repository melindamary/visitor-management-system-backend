using Microsoft.AspNetCore.Mvc;
using VMS.Data;
using VMS.Models;
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

        /*[Authorize(Policy = "AdminOnly")]*/
        [HttpGet]
        public async Task<IEnumerable<GetDeviceIdAndNameDTO>> GetDeviceIdAndName()
        {
            return await _deviceRepository.GetDevicesAsync();
        }

        /*[Authorize(Policy = "AdminOnly")]*/
        [HttpPost]
        public async Task<ActionResult<Device>> PostDevice(AddNewDeviceDTO deviceDto)
        {
            try
            {
                var device = await _deviceRepository.AddDeviceAsync(deviceDto);
                return CreatedAtAction(nameof(PostDevice), new { id = device.Id }, device);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
}

