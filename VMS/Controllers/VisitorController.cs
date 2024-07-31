using Microsoft.AspNetCore.Mvc;
using VMS.Repository.IRepository;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Services;

namespace VMS.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class VisitorController : ControllerBase
    {
        private readonly IVisitorFormService _visitorService;

        public VisitorController(IVisitorFormService visitorService)
        {
            _visitorService = visitorService;
        }

        [HttpGet("details")]
        public async Task<ActionResult<IEnumerable<Visitor>>> GetVisitorDetails()
        {
            var visitors = await _visitorService.GetVisitorDetailsAsync();
            return Ok(visitors);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetPersonInContact()
        {
            var contacts = await _visitorService.GetPersonInContactAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Visitor>> GetVisitorById(int id)
        {
            var visitor = await _visitorService.GetVisitorByIdAsync(id);
            if (visitor == null)
            {
                return NotFound();
            }
            return Ok(visitor);
        }

        [HttpPost]
        public async Task<ActionResult<Visitor>> CreateVisitor(VisitorCreationDTO visitorDto)
        {
            var visitor = await _visitorService.CreateVisitorAsync(visitorDto);
            return CreatedAtAction(nameof(GetVisitorById), new { id = visitor.Id }, visitor);
        }

        [HttpPost]
        public async Task<ActionResult<VisitorDevice>> AddVisitorDevice(AddVisitorDeviceDTO addDeviceDto)
        {
            var device = await _visitorService.AddVisitorDeviceAsync(addDeviceDto);
            return CreatedAtAction(nameof(GetVisitorById), new { id = device.VisitorId }, device);
        }
    }
}
