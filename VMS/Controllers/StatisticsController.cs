// File: Controllers/StatisticsController.cs
using Microsoft.AspNetCore.Mvc;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository.IRepository;

namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsController(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        [HttpGet("location")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<ActionResult<IEnumerable<LocationStatisticsDTO>>> GetLocationStatistics([FromQuery] int days =7)
        {
            try
            {
                var statistics = await _statisticsRepository.GetLocationStatistics(days);
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while retrieving location statistics.");
            }
        }

        /*        [HttpGet]
                public async Task<ActionResult<IEnumerable<LocationStatisticsDTO>>> GetLocationStatistics()
                {
                    var result = await _statisticsRepository.GetLocationStatistics();
                    return Ok(result);
                }*/
        /*  [HttpGet("security")]
          public async Task<ActionResult<IEnumerable<SecurityStatisticsDTO>>> GetSecurityStatistics()
          {
              var result = await _statisticsRepository.GetSecurityStatistics();
              return Ok(result);
          }
  */

        [HttpGet("security")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<ActionResult<IEnumerable<SecurityStatisticsDTO>>> GetSecurityStatistics([FromQuery] int days = 7)
        {
            var result = await _statisticsRepository.GetSecurityStatistics(days);
            return Ok(result);
        }

        [HttpGet("purpose")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<ActionResult<IEnumerable<PurposeStatisticsDTO>>> GetPurposeStatistics()
        {
            var result = await _statisticsRepository.GetPurposeStatistics();
            return Ok(result);
        }
        
        [HttpGet("dashboard")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<ActionResult<IEnumerable<DashboardStatisticsDTO>>> GetDashboardStatistics()
        {
            var result = await _statisticsRepository.GetDashboardStatistics();
            return Ok(result);
        }
    }
}