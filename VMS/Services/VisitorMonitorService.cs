/*using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using VMS.AVHubs;
using VMS.Data;

namespace VMS.Services
{
    public class VisitorMonitorService : BackgroundService
    {
        private readonly VisitorManagementDbContext _dbContext;
        private readonly IHubContext<VisitorHub> _hubContext;

        public VisitorMonitorService(VisitorManagementDbContext dbContext, IHubContext<VisitorHub> hubContext)
        {
            _dbContext = dbContext;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var count = await CountActiveVisitors();
                await _hubContext.Clients.All.SendAsync("ReceiveVisitorCount", count, stoppingToken);

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }

        private async Task<int> CountActiveVisitors()
        {
            return await _dbContext.Visitors
                .Where(v => v.CheckInTime != null && v.CheckOutTime == null).CountAsync();
        }
    }
}
*/