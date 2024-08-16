using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VMS.Models;
using VMS.Services.IServices;

namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;
        private readonly ILogger<ReportController> _logger; 

        public ReportController(IReportService service, ILogger<ReportController> logger) 
        {
            _service = service;
            _logger = logger;
        }

       
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<ActionResult<APIResponse>> VisitorList()
        {
            _logger.LogInformation("Fetching visitor list");
            var visitors = await _service.GetAllVisitorReportsAsync();

            if (visitors == null || !visitors.Any())
            {
                var errorResponse = new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new List<string> { "No visitors found." }
                };
                _logger.LogWarning("No visitors found.");
                return NotFound(errorResponse);
            }

            var response = new APIResponse
            {
                Result = visitors,
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK
            };

            _logger.LogInformation("Successfully fetched visitor list");
            return Ok(response);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<ActionResult<APIResponse>> Visitor(int id)
        {
            _logger.LogInformation($"Fetching details for visitor with ID {id}");
            var visitor = await _service.GetVisitorDetailsAsync(id);

            if (visitor == null)
            {
                var errorResponse = new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new List<string> { "Visitor not found." }
                };
                _logger.LogWarning($"Visitor with ID {id} not found.");
                return NotFound(errorResponse);
            }

            var response = new APIResponse
            {
                Result = visitor,
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
            };

            _logger.LogInformation($"Successfully fetched details for visitor with ID {id}");
            return Ok(response);
        }
    }
}
