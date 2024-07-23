// File: Controllers/StatisticsController.cs
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationStatisticsDTO>>> GetLocationStatistics()
        {
            var result = await _statisticsRepository.GetLocationStatistics();
            return Ok(result);
        }
        /*  [HttpGet("security")]
          public async Task<ActionResult<IEnumerable<SecurityStatisticsDTO>>> GetSecurityStatistics()
          {
              var result = await _statisticsRepository.GetSecurityStatistics();
              return Ok(result);
          }
  */

        [HttpGet("security")]
        public async Task<ActionResult<IEnumerable<SecurityStatisticsDTO>>> GetSecurityStatistics([FromQuery] int days = 7)
        {
            var result = await _statisticsRepository.GetSecurityStatistics(days);
            return Ok(result);
        }

        [HttpGet("purpose")]
        public async Task<ActionResult<IEnumerable<PurposeStatisticsDTO>>> GetPurposeStatistics()
        {
            var result = await _statisticsRepository.GetPurposeStatistics();
            return Ok(result);
        }
        
        [HttpGet("dashboard")]
        public async Task<ActionResult<IEnumerable<DashboardStatisticsDTO>>> GetDashboardStatistics()
        {
            var result = await _statisticsRepository.GetDashboardStatistics();
            return Ok(result);
        }
    }
}