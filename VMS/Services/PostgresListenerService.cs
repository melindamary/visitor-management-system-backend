
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System.Threading;
using System.Threading.Tasks;
using VMS.AVHubs;
using VMS.Data;
using VMS.Services;


namespace VMS.Services
{
    public class PostgresListenerService : BackgroundService
    {
        private readonly IHubContext<VisitorHub> _hubContext;
        private readonly string _connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
        private readonly ILogger<PostgresListenerService> _logger;  // Add this line
        private readonly VisitorService _visitorService;


        public PostgresListenerService(IHubContext<VisitorHub> hubContext, ILogger<PostgresListenerService> logger, VisitorService visitorService)
        {
            _hubContext = hubContext;
            _logger = logger;  // Initialize logger
            _visitorService = visitorService;


        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {



                await using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync(stoppingToken);
                using (var cmd = new NpgsqlCommand("LISTEN visitor_channel;", conn))
                {
                    await cmd.ExecuteNonQueryAsync(stoppingToken);
                    _logger.LogInformation("Listening for notifications...");
                }

                conn.Notification += async (o, e) =>
                {
                    _logger.LogInformation($"Received notification with payload: {e.Payload}");  // Log the payload
                    try
                    {
                        if (int.TryParse(e.Payload, out var visitorId))
                        {
                            _logger.LogInformation($"notification with payload");
                            // Get the updated visitor count
                            int count = await _visitorService.GetVisitorCountAsync();

                            // Send the updated visitor count to all clients
                            await _hubContext.Clients.All.SendAsync("ReceiveVisitorCount", count);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error processing notification payload.");
                    }
                };

                while (!stoppingToken.IsCancellationRequested)
                {
                    // Wait for notifications
                    await Task.Delay(1000, stoppingToken); // Avoid busy waiting
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PostgresListenerService.");

            }
        }

    }
}
