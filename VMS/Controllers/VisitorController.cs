using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VMS.Repository.IRepository;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Data;
using Microsoft.AspNetCore.Authorization;

namespace VMS.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class VisitorController : ControllerBase
    {
        private readonly IVisitorFormRepository _visitorRepository;


        /*[Authorize(Policy = "AdminOnly")]*/
        public VisitorController(IVisitorFormRepository visitorRepository)
        {
            _visitorRepository = visitorRepository;
        }

        /*[Authorize(Policy = "AdminOnly")]*/
        [HttpGet]
        public async Task<IEnumerable<Visitor>> GetVisitorDetails()
        {
            return await _visitorRepository.GetVisitorDetailsAsync();
        }

        /*[Authorize(Policy = "AdminOnly")]*/
        [HttpGet]
        public async Task<IEnumerable<string>> GetPersonInContact()
        {
            return await _visitorRepository.GetPersonInContactAsync();
        }

        [HttpPost("create-and-add-item")]
        public async Task<IActionResult> CreateVisitorAndAddItem([FromBody] VisitorCreationDTO visitorDto)
        {
            if (visitorDto == null)
            {
                return BadRequest("Visitor data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var visitor = await _visitorRepository.CreateVisitorAsync(visitorDto);

            return Ok(new { CreatedVisitor = visitor, AddedItems = visitorDto.SelectedDevice });
        }

        // Method to call the AddVisitorDevice API
        private VisitorDevice AddVisitorDevice(AddVisitorDeviceDto addDeviceDto)
        {
            // Create VisitorDevice entity from AddVisitorDeviceDto
            var visitorDevice = new VisitorDevice
            {
                VisitorId = addDeviceDto.VisitorId,
                DeviceId = addDeviceDto.DeviceId,
                SerialNumber = addDeviceDto.SerialNumber
            };

            // Add VisitorDevice to context
            _context.VisitorDevices.Add(visitorDevice);

            // Save changes to the VisitorDevices
            _context.SaveChanges();

            return visitorDevice;
        }

        /*[Authorize(Policy = "AdminOnly")]*/
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVisitorById(int id)
        {
            var visitor = await _visitorRepository.GetVisitorByIdAsync(id);

            if (visitor == null)
            {
                return NotFound();
            }

            return Ok(visitor);
        }


    }
    }

