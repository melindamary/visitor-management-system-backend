using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VMS.Models;
using VMS.Repository.IRepository;
using VMS.Services.IServices;

namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReportController : ControllerBase
    {
        private IReportService _service;
        public ReportController(IReportService service)
        {
            _service = service;
        }

       /* [Authorize(Policy = "AdminOnly")]*/
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<ActionResult<APIResponse>> GetAllVisitorReport()
        {
            var visitors = await _service.GetAllVisitorReportsAsync();

            if (visitors == null || !visitors.Any())
            {
                var errorResponse = new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new List<string> { "No visitors found." }
                };
                return NotFound(errorResponse);
            }

            var response = new APIResponse
            {
                Result = visitors,
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK
            };

            return Ok(response);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<ActionResult<APIResponse>> GetVisitorDetails(int id)
        {
            var visitor = await _service.GetVisitorDetailsAsync(id);

            if (visitor == null)
            {
                var errorResponse = new APIResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new List<string> { "Visitor not found." }
                };
                return NotFound(errorResponse);
            }

            var response = new APIResponse
            {
                Result = visitor,
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
            };
            return Ok(response);
        }
    }
}
