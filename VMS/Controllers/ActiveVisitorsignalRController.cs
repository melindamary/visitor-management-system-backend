/*using Microsoft.AspNetCore.Mvc;
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
*/
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VMS.AVHubs;
using VMS.Data;
using VMS.Models;

namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActiveVisitorsignalRController : ControllerBase
    {

        private readonly IHubContext<VisitorHub> _hubContext;
        private readonly VisitorManagementDbContext _context;

        public ActiveVisitorsignalRController(IHubContext<VisitorHub> hubContext, VisitorManagementDbContext context)
        {
            _hubContext = hubContext;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> UpdateVisitorCount()
        {
            // Add a new visitor
            _context.Visitors.Add(new Visitor());
            await _context.SaveChangesAsync();

            // Get the updated visitor count
            int count = await _context.Visitors.CountAsync();

            // Send the updated visitor count to all clients
            await _hubContext.Clients.All.SendAsync("ReceiveVisitorCount", count);

            return Ok(count);
        }
    }
}
