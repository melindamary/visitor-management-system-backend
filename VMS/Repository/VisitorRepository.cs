using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VMS.AVHubs;
using VMS.Data;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository.IRepository;
using VMS.Services;

namespace VMS.Repository
{
    public class VisitorRepository : IVisitorRepository
    {
        private readonly VisitorManagementDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<VisitorRepository> _logger;
        private readonly IHubContext<VisitorHub> _hubContext;
        private readonly DashboardService _dashboardService;


        public VisitorRepository(VisitorManagementDbContext context, IHubContext<VisitorHub> hubContext, IMapper mapper, ILogger<VisitorRepository> logger, DashboardService dashboardService)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _hubContext = hubContext;
            _dashboardService = dashboardService;

        }

        public async Task<Visitor> GetVisitorByIdAsync(int id)
        {
            return await _context.Visitors.FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<int> GetVisitorCount(Func<IQueryable<Visitor>, IQueryable<Visitor>> filter)
        {
            DateTime today = DateTime.Today;

            var count = await filter(_context.Visitors
                .Where(v => v.VisitDate == today))
                .CountAsync();

            return count;
        }

        public async Task<IEnumerable<VisitorLogDTO>> GetVisitorLogs(Func<IQueryable<Visitor>, IQueryable<Visitor>> filter,string locationName)
        {
            DateTime today = DateTime.Today;
            var officeLocation = await _context.OfficeLocations.FirstOrDefaultAsync(l => l.Name == locationName);
            var visitorDetail = await filter(_context.Visitors
                .Include(v => v.Purpose)
                .Include(v => v.VisitorDevices)
                    .ThenInclude(vd => vd.Device)
                .Where(v => v.VisitDate == today && v.OfficeLocationId == officeLocation.Id))
                .ToListAsync();

            var visitorLogDtos = _mapper.Map<List<VisitorLogDTO>>(visitorDetail);

            foreach (var dto in visitorLogDtos)
            {
                if (dto.Photo != null)
                {
                    dto.PhotoBase64 = Convert.ToBase64String(dto.Photo);
                }
            }

            return visitorLogDtos;
        }

        public async Task<VisitorLogDTO> UpdateCheckInTimeAndCardNumber(int id, UpdateVisitorPassCodeDTO updateVisitorPassCode)
        {

            var existingVisitor = await _context.Visitors.FindAsync(id);
            if (existingVisitor == null)
            {
                return null;
            }

            bool passCodeExists = await _context.Visitors.AnyAsync(v => v.VisitorPassCode == updateVisitorPassCode.VisitorPassCode
            && v.OfficeLocationId == existingVisitor.OfficeLocationId && v.Id != id);
            if (passCodeExists)
            {
                throw new ArgumentException("This visitor pass code has already been allocated.");
            }

            existingVisitor.CheckInTime = DateTime.Now;
            existingVisitor.VisitorPassCode = updateVisitorPassCode.VisitorPassCode;
            existingVisitor.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            var visitorLogDTO = _mapper.Map<VisitorLogDTO>(existingVisitor);
            await _hubContext.Clients.All.SendAsync("ReceiveVisitorCount", await _dashboardService.GetVisitorCountAsync());
            await _hubContext.Clients.All.SendAsync("ReceiveScheduledVisitorsCount", await _dashboardService.GetScheduledVisitorsCountAsync());
            await _hubContext.Clients.All.SendAsync("ReceiveTotalVisitorsCount", await _dashboardService.GetTotalVisitorsCountAsync());

           



            return visitorLogDTO;
        }

        public async Task<VisitorLogDTO> UpdateCheckOutTime(int id)
        {

            var existingVisitor = await _context.Visitors.FindAsync(id);
            if (existingVisitor == null)
            {
                return null;
            }

            existingVisitor.CheckOutTime = DateTime.Now;
            existingVisitor.UpdatedDate = DateTime.Now;
            existingVisitor.VisitorPassCode = 0;

            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveVisitorCount", await _dashboardService.GetVisitorCountAsync());
            await _hubContext.Clients.All.SendAsync("ReceiveScheduledVisitorsCount", await _dashboardService.GetScheduledVisitorsCountAsync());
            await _hubContext.Clients.All.SendAsync("ReceiveTotalVisitorsCount", await _dashboardService.GetTotalVisitorsCountAsync());


            var visitorLogDTO = _mapper.Map<VisitorLogDTO>(existingVisitor);



            _logger.LogInformation("Successfully updated check-out time for visitor ID {VisitorId}.", id);
            return visitorLogDTO;
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
