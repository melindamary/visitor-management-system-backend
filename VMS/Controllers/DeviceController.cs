using Microsoft.AspNetCore.Mvc;
using System.Net;
using VMS.Data;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository.IRepository;
using VMS.Services.IServices;

namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IDeviceService _service;

        public DeviceController(IDeviceRepository deviceRepository, IDeviceService service)
        {
            _deviceRepository = deviceRepository;
            _service = service;
        }

        /*[Authorize(Policy = "AdminOnly")]*/
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        // public async Task<IEnumerable<GetDeviceIdAndNameDTO>> GetItems()
        // [HttpGet]
        public async Task<IEnumerable<GetDeviceIdAndNameDTO>> GetDeviceIdAndName()
        {
            return await _deviceRepository.GetDevicesAsync();
        }

        /*[Authorize(Policy = "AdminOnly")]*/
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<ActionResult<APIResponse>> DeviceList() { 
        

            var devices = await _service.GetDeviceListAsync();

            if (devices == null)
            {
                var errorResponse = new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new List<string> { "Devices not found." }
                };
                return NotFound(errorResponse);
            }

            return new APIResponse
            {
                Result = devices,
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
            };
        }

        /*[HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<ActionResult<APIResponse>> Device(int id)
        {
            var result = await _service.DeleteDeviceAsync(id);
            if (!result)
            {
                var errorResponse = new APIResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new List<string> { "Device does not exist" }
                };
                return NotFound(errorResponse);
            }
            return new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                ErrorMessages = null
            };

        }*/

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<ActionResult<APIResponse>> Device([FromBody] DeviceUpdateRequestDTO updateDeviceRequestDTO)
        {
            var result = await _service.UpdateDeviceAsync(updateDeviceRequestDTO);
            if (!result)
            {
                var errorResponse = new APIResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new List<string> { "Device does not exist" }
                };
                return NotFound(errorResponse);
            }
            return new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                ErrorMessages = null
            };

        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<ActionResult<APIResponse>> DeviceStatus([FromBody] DeviceStatusUpdateRequestDTO updateDeviceStatusRequestDTO)
        {
            var result = await _service.UpdateDeviceStatusAsync(updateDeviceStatusRequestDTO);
            if (!result)
            {
                var errorResponse = new APIResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new List<string> { "Device does not exist" }
                };
                return NotFound(errorResponse);
            }
            return new APIResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                ErrorMessages = null
            };

        }
    }
}

