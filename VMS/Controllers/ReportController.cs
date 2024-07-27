using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VMS.Models;
using VMS.Repository.IRepository;

namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReportController : ControllerBase
    {
        private IReportRepository _repository;
        public ReportController(IReportRepository repository)
        {
            _repository = repository;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAllVisitorReport()
        {
            var visitors = await _repository.GetAllVisitorsAsync();

            if (visitors == null)
            {
                var errorResponse = new APIResponse {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new List<string> { "No visitors found." }
                };
                return NotFound(errorResponse);
            }
            Console.WriteLine("Reports:", visitors);
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
        public async Task<ActionResult<APIResponse>> GetVisitorDetails(int id) {

            var visitor = await _repository.GetVisitorDetailsAsync(id);
            if (visitor == null) {
                var errorResponse = new APIResponse
                {
                    Result = null,
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.NotFound,
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
