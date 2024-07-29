
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using VMS.Data;
using VMS.Models;
using VMS.Services;

namespace VMS.AVHubs

{
    public class VisitorHub : Hub
    {
        private readonly ILogger<VisitorHub> _logger;

        private readonly VisitorService _visitorService;


        public VisitorHub(VisitorManagementDbContext dbContext, ILogger<VisitorHub> logger, VisitorService visitorTiles)
        {
            _visitorService = visitorTiles;

            _logger = logger;

        }
        public async Task SendInitialVisitorCount()
        {
            _logger.LogInformation("Client connected t0 intital count: {ConnectionId}", Context.ConnectionId);

            int count = await _visitorService.GetVisitorCountAsync();
            await Clients.Caller.SendAsync("ReceiveVisitorCount", count);
        }
        public async Task SendInitialScheduledVisitorsCount()
        {
            int count = await _visitorService.GetScheduledVisitorsCountAsync();
            await Clients.Caller.SendAsync("ReceiveScheduledVisitorsCount", count);
        }

        public async Task SendInitialTotalVisitorsCount()
        {
            int count = await _visitorService.GetTotalVisitorsCountAsync();
            await Clients.Caller.SendAsync("ReceiveTotalVisitorsCount", count);
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
