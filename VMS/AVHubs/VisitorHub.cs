
using Microsoft.AspNetCore.SignalR;
using VMS.Data;
using VMS.Models.DTO;
using VMS.Services;

namespace VMS.AVHubs

{
    public class VisitorHub : Hub
    {
        private readonly ILogger<VisitorHub> _logger;

        private readonly DashboardService _dashboardService;

        private readonly ReportService _reportService;
        public VisitorHub(VisitorManagementDbContext dbContext, ILogger<VisitorHub> logger, DashboardService visitorTiles,ReportService reportService)
        {
            _dashboardService = visitorTiles;
            _reportService=reportService;
            _logger = logger;

        }
        public async Task SendInitialVisitorCount()
        {
            _logger.LogInformation("Client connected t0 intital count: {ConnectionId}", Context.ConnectionId);

            int count = await _dashboardService.GetVisitorCountAsync();
            await Clients.Caller.SendAsync("ReceiveVisitorCount", count);
        }
        public async Task SendInitialScheduledVisitorsCount()
        {
            int count = await _dashboardService.GetScheduledVisitorsCountAsync();
            await Clients.Caller.SendAsync("ReceiveScheduledVisitorsCount", count);
        }

        public async Task SendInitialTotalVisitorsCount()
        {
            int count = await _dashboardService.GetTotalVisitorsCountAsync();
            await Clients.Caller.SendAsync("ReceiveTotalVisitorsCount", count);
        }
        public async Task SendInitialLocationStatistics(int days)
        {
            var locationStatistics = await _dashboardService.GetLocationStatistics(days); // Get the location statistics
            await Clients.Caller.SendAsync("ReceiveLocationStatistics", locationStatistics); // Send the location statistics to the caller
        }
        public async Task SendInitialLocationStatisticsecurity(int days)
        {
            var locationStatisticssecurity = await _dashboardService.GetSecurityStatistics(days); // Get the location statistics
            await Clients.Caller.SendAsync("ReceiveLocationStatisticsecurity", locationStatisticssecurity); // Send the location statistics to the caller
        }
        public async Task SendInitialReports()
        {
            var report = await _reportService.GetAllVisitorReportsAsync(); 
            await Clients.Caller.SendAsync("ReceiveReport", report); 
        }


        public async Task BroadcastVisitorCounts()
        {
            int activeCount = await _dashboardService.GetVisitorCountAsync();
            int scheduledCount = await _dashboardService.GetScheduledVisitorsCountAsync();
            int totalCount = await _dashboardService.GetTotalVisitorsCountAsync();

            await Clients.All.SendAsync("ReceiveVisitorCount", activeCount);
            await Clients.All.SendAsync("ReceiveScheduledVisitorsCount", scheduledCount);
            await Clients.All.SendAsync("ReceiveTotalVisitorsCount", totalCount);
        }
        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("Client connected: {ConnectionId}", Context.ConnectionId);

            await SendInitialVisitorCount();
            await SendInitialScheduledVisitorsCount();
            await SendInitialTotalVisitorsCount();
            await base.OnConnectedAsync();
        }
      
    }
}
