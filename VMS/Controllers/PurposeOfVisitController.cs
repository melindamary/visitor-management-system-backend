using Microsoft.AspNetCore.Mvc;
using System.Net;
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
        private readonly IPurposeOfVisitRepository _repository;

        public PurposeOfVisitController(IPurposeOfVisitRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("get-purposes-id-Name")]
        public async Task<IEnumerable<PurposeOfVisitNameadnIdDTO>> GetPurposes()
        {
            return await _repository.GetPurposesAsync();
        }

        [HttpPost]
        public async Task<ActionResult<PurposeOfVisit>> PostPurpose(AddNewPurposeDTO purposeDto)
        {
            try
            {
                var purpose = await _repository.AddPurposeAsync(purposeDto);
                return CreatedAtAction(nameof(PostPurpose), new { id = purpose.Id }, purpose);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetPurposeList() {

            var purposes = await _repository.GetPurposeListAsync();

            if (purposes == null) {
                var errorResponse = new APIResponse { 
                    Result = null,
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new List<string> {"No purposes of visit found" }
                };

                return NotFound(errorResponse);
            }

            var response = new APIResponse
            {
                Result = purposes,
                StatusCode = HttpStatusCode.OK,
                ErrorMessages = null,
            };

            return Ok(purposes);

        }
    }
}
