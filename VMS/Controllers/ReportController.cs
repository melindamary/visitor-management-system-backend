using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VMS.Models.DTO;
using VMS.Models;
using VMS.Services.IServices;

namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReportController : ControllerBase
    {
        private IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        /*[Authorize(Policy = "AdminOnly")]*/
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAllVisitorReport()
        {
            var visitors = await _reportService.GetAllVisitorsAsync();

            if (visitors == null)
            {
                var errorResponse = new APIResponse {
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
    }
}
