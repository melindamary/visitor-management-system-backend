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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        // public async Task<IEnumerable<PurposeOfVisitNameadnIdDTO>> GetPurposes()
        
        public async Task<IEnumerable<PurposeOfVisitNameadnIdDTO>> GetApprovedPurposesIdAndName()
        {
            return await _repository.GetPurposesAsync();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<ActionResult<APIResponse>> PurposeList() {

            var purposes = await _repository.GetPurposeListAsync();

            if (purposes == null) {
                var errorResponse = new APIResponse {
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new List<string> { "No purposes of visit found" }
                };

                return NotFound(errorResponse);
            }

            var response = new APIResponse
            {
                Result = purposes,
                StatusCode = HttpStatusCode.OK,
            };

            return Ok(purposes);

        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<ActionResult<APIResponse>> Purpose([FromBody] PurposeUpdateRequestDTO updatePurposeRequestDTO)
        {
            var result = await _repository.UpdatePurposeAsync(updatePurposeRequestDTO);
            if (!result) {
                var errorResponse = new APIResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new List<string> { "Purpose does not exist" }
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

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<ActionResult<APIResponse>> Purpose(int id)
        {
            var result = await _repository.DeletePurposeAsync(id);
            if (!result)
            {
                var errorResponse = new APIResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new List<string> { "Purpose does not exist" }
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
