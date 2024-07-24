using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;
using VMS.AVHubs;
using VMS.Data;
namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActiveVisitorsignalRController:ControllerBase
    {
        private readonly IHubContext<VisitorHub> _hubContext;
        private readonly VisitorManagementDbContext _context;

        public ActiveVisitorsignalRController(IHubContext<VisitorHub> hubContext, VisitorManagementDbContext context)
        {
            _hubContext = hubContext;
            _context = context;
        }

        [HttpGet("activeVisitors")]
        public async Task<IActionResult> GetActiveVisitors()
        {
            var activeVisitors = _context.Visitors
                .Where(v => v.CheckInTime<= DateTime.Today && v.CheckOutTime == null)
                .Count();

            await _hubContext.Clients.All.SendAsync("ReceiveActiveVisitors", activeVisitors);
            return Ok(activeVisitors);
        }
    }
}
