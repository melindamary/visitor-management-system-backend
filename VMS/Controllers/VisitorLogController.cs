using Microsoft.AspNetCore.Mvc;
using System.Net;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository.IRepository;
using VMS.Services;

namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class VisitorLogController : ControllerBase
    {
        private readonly IVisitorRepository _repository;

        public VisitorLogController(IVisitorRepository repository, VisitorService visitorService)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(APIResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(APIResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetVisitorLogToday()
        {
            var response = new APIResponse();
            try
            {
                var activeVisitorsCount = await _repository.GetActiveVisitorsCountToday();
                var totalVisitorsCount = await _repository.GetTotalVisitorsCountToday();
                var checkedOutVisitorsCount = await _repository.GetCheckedOutVisitorsCountToday();
                var upcomingVisitors = await _repository.GetUpcomingVisitorsToday();
                var activeVisitors = await _repository.GetActiveVisitorsToday();
                var checkedOutVisitors = await _repository.GetCheckedOutVisitorsToday();
                var visitorsToday = await _repository.GetVisitorDetailsToday();
                var activeVisitorsCount = await _visitorRepository.GetActiveVisitorsCountToday();
                var totalVisitorsCount = await _visitorRepository.GetTotalVisitorsCountToday();
                var checkedOutVisitorsCount = await _visitorRepository.GetCheckedOutVisitorsCountToday();
                var upcomingVisitors = await _visitorRepository.GetUpcomingVisitorsToday();
                var activeVisitors = await _visitorRepository.GetActiveVisitorsToday();

                var result = new
                {
                    ActiveVisitorsCount = activeVisitorsCount,
                    TotalVisitorsCount = totalVisitorsCount,
                    CheckedOutVisitorsCount = checkedOutVisitorsCount,
                    UpcomingVisitors = upcomingVisitors,
                    ActiveVisitors = activeVisitors,
                    VisitorsToday = visitorsToday,
                    CheckedOutVisitors = checkedOutVisitors
                };

                response.IsSuccess = true;
                response.Result = result;
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
                response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(APIResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(APIResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(APIResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateCheckInTimeAndCardNumber(int id, [FromBody] UpdateVisitorPassCodeDTO updateVisitorPassCode)
        {
            var response = new APIResponse();
            try
            {
                // Check if the model state is valid
                if (!ModelState.IsValid)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Invalid input data.");

                    // Collect all validation errors
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        response.ErrorMessages.Add(error.ErrorMessage);
                    }
                    return BadRequest(response);
                }

                // Call the repository method to update the visitor details
                var checkedInVisitor = await _repository.UpdateCheckInTimeAndCardNumber(id, updateVisitorPassCode);

                // If the visitor is not found, return a NotFound response
                if (checkedInVisitor == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("Visitor not found.");
                    return NotFound(response);
                }

                // If successful, return the updated visitor details
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = checkedInVisitor;
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                // Handle business logic errors
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.Add(ex.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add("An unexpected error occurred.");
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(typeof(APIResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(APIResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateCheckOutTime(int id)
        {
            var response = new APIResponse();
            try
            {
                var checkedOutVisitor = await _repository.UpdateCheckOutTime(id);

                if (checkedOutVisitor == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("Visitor not found.");
                    return NotFound(response);
                }

                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = checkedOutVisitor;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add("An unexpected error occurred.");
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }
}
