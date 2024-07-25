using Microsoft.AspNetCore.Mvc;
using VMS.Data;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository.IRepository;

namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PurposeOfVisitController : ControllerBase
    {
        private readonly IPurposeOfVisitRepository _purposeOfVisitRepository;

        public PurposeOfVisitController(IPurposeOfVisitRepository purposeOfVisitRepository)
        {
            _purposeOfVisitRepository = purposeOfVisitRepository;
        }

        [HttpGet("get-purposes-id-Name")]
        public async Task<IEnumerable<PurposeOfVisitNameadnIdDTO>> GetPurposes()
        {
            return await _purposeOfVisitRepository.GetPurposesAsync();
        }

        [HttpPost]
        public async Task<ActionResult<PurposeOfVisit>> PostPurpose(AddNewPurposeDTO purposeDto)
        {
            try
            {
                var purpose = await _purposeOfVisitRepository.AddPurposeAsync(purposeDto);
                return CreatedAtAction(nameof(PostPurpose), new { id = purpose.PurposeId }, purpose);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
}
