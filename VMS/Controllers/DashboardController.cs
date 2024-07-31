using Microsoft.AspNetCore.Mvc;
using System.Threading.Channels;
using VMS.Repository.IRepository;
using VMS.Data;

namespace VMS.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

/*        [HttpGet("active-visitors")]
        public ActionResult<int> GetActiveVisitors()
        {
            var activeVisitors = _dashboardRepository.GetActiveVisitors();
            return Ok(activeVisitors);
        }

        [HttpGet("scheduled-visitors")]
        public ActionResult<int> GetScheduledVisitors()
        {
            var scheduledVisitors = _dashboardRepository.GetScheduledVisitors();
            return Ok(scheduledVisitors);
        }

        [HttpGet("total-visitors")]
        public ActionResult<int> GetTotalVisitors()
        {
            var totalVisitors = _dashboardRepository.GetTotalVisitors();
            return Ok(totalVisitors);
        }*/
    }

}
