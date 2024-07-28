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
    public class VisitorLogController:ControllerBase
    {
        private readonly IVisitorRepository _visitorRepository;
        private readonly VisitorService _visitorService;

        public VisitorLogController(IVisitorRepository visitorRepository, VisitorService visitorService)
        {
            _visitorRepository = visitorRepository;
            _visitorService = visitorService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(APIResponse), 200)]
        [ProducesResponseType(typeof(APIResponse), 500)]
        public async Task<ActionResult<APIResponse>> GetVisitorLogToday()
        {
            var response = new APIResponse();
            try
            {
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
                var checkedInVisitor = await _visitorRepository.UpdateCheckInTimeAndCardNumber(id, updateVisitorPassCode);

                if (checkedInVisitor == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("Visitor not found.");
                    return NotFound(response);
                }

                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = checkedInVisitor;
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.Add(ex.Message);
                return BadRequest(response);
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(typeof(APIResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(APIResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateCheckOutTime(int id)
        {
            var response = new APIResponse();
            var checkedOutVisitor = await _visitorRepository.UpdateCheckOutTime(id);

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
    }
}

