using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository.IRepository;
using VMS.Services;
using VMS.Services.IServices;

namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class VisitorLogController : ControllerBase
    {
        private readonly IVisitorService _service;
        private readonly ILogger<VisitorLogController> _logger;

        public VisitorLogController(IVisitorService service, ILogger<VisitorLogController> logger)
        {
            _service = service;
            _logger = logger; 
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<ActionResult<APIResponse>> VisitorLogList(string locationName)
        {
            var response = new APIResponse();
            try
            {
                _logger.LogInformation("Fetching visitor log details for today.");

                var activeVisitorsCount = await _service.GetActiveVisitorsCountToday(locationName);
                var totalVisitorsCount = await _service.GetTotalVisitorsCountToday(locationName);
                var checkedOutVisitorsCount = await _service.GetCheckedOutVisitorsCountToday(locationName);
                var upcomingVisitors = await _service.GetUpcomingVisitorsToday(locationName);
                var activeVisitors = await _service.GetActiveVisitorsToday(locationName);
                var checkedOutVisitors = await _service.GetCheckedOutVisitorsToday(locationName);
                var visitorsToday = await _service.GetVisitorDetailsToday(locationName);

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

                _logger.LogInformation("Successfully fetched visitor log details for today.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching visitor log details for today.");
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
        public async Task<IActionResult> VisitorLogCheckIn(int id, [FromBody] UpdateVisitorPassCodeDTO updateVisitorPassCode)
        {
            var response = new APIResponse();
            try
            {
                _logger.LogInformation("Updating check-in time and pass code for visitor ID {VisitorId}.", id);

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

                    _logger.LogWarning("Invalid input data for visitor ID {VisitorId}. Errors: {Errors}", id, response.ErrorMessages);
                    return BadRequest(response);
                }

                // Call the repository method to update the visitor details
                var checkedInVisitor = await _service.UpdateCheckInTimeAndCardNumber(id, updateVisitorPassCode);

                // If the visitor is not found, return a NotFound response
                if (checkedInVisitor == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("Visitor not found.");

                    _logger.LogWarning("Visitor ID {VisitorId} not found during check-in update.", id);
                    return NotFound(response);
                }

                // If successful, return the updated visitor details
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = checkedInVisitor;

                _logger.LogInformation("Successfully updated check-in time and pass code for visitor ID {VisitorId}.", id);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                // Handle business logic errors
                _logger.LogWarning(ex, "Business logic error occurred while updating check-in time and pass code for visitor ID {VisitorId}.", id);
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.Add(ex.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                _logger.LogError(ex, "An unexpected error occurred while updating check-in time and pass code for visitor ID {VisitorId}.", id);
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add("An unexpected error occurred.");
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> VisitorLogCheckOut(int id)
        {
            var response = new APIResponse();
            try
            {
                _logger.LogInformation("Updating check-out time for visitor ID {VisitorId}.", id);

                var checkedOutVisitor = await _service.UpdateCheckOutTime(id);

                if (checkedOutVisitor == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("Visitor not found.");

                    _logger.LogWarning("Visitor ID {VisitorId} not found during check-out update.", id);
                    return NotFound(response);
                }

                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = checkedOutVisitor;

                _logger.LogInformation("Successfully updated check-out time for visitor ID {VisitorId}.", id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating check-out time for visitor ID {VisitorId}.", id);
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add("An unexpected error occurred.");
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }
}
