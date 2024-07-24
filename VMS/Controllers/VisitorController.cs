using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VMS.Repository.IRepository;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Data;

namespace VMS.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class VisitorController : ControllerBase
    {
        private readonly IVisitorFormRepository _visitorRepository;

        public VisitorController(IVisitorFormRepository visitorRepository)
        {
            _visitorRepository = visitorRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Visitor>> GetVisitorDetails()
        {
            return await _visitorRepository.GetVisitorDetailsAsync();
        }

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