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
        [HttpGet("security")]
        public async Task<ActionResult<IEnumerable<SecurityStatisticsDTO>>> GetSecurityStatistics()
        {
            var result = await _statisticsRepository.GetSecurityStatistics();
            return Ok(result);
        }
    }
}