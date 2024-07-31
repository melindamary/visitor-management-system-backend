using AutoMapper;
using VMS.Data;
using VMS.Models.DTO;
using VMS.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using VMS.Models;

namespace VMS.Repository
{
    public class VisitorRepository : IVisitorRepository
    {
        private readonly VisitorManagementDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<VisitorRepository> _logger;

        public VisitorRepository(VisitorManagementDbContext context, IMapper mapper, ILogger<VisitorRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        private async Task<int> GetVisitorCount(Func<IQueryable<Visitor>, IQueryable<Visitor>> filter)
        {
            DateTime today = DateTime.Today;
            _logger.LogInformation("Getting visitor count for date: {Date}.", today);
            try
            {
                int count = await filter(_context.Visitors
                    .Where(v => v.VisitDate == today))
                    .CountAsync();

                _logger.LogInformation("Visitor count for date {Date}: {Count}.", today, count);
                return count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting visitor count for date: {Date}.", today);
                throw;
            }
        }

        private async Task<IEnumerable<VisitorLogDTO>> GetVisitorLogs(Func<IQueryable<Visitor>, IQueryable<Visitor>> filter)
        {
            DateTime today = DateTime.Today;
            _logger.LogInformation("Getting visitor logs for date: {Date}.", today);
            try
            {
                var visitorDetail = await filter(_context.Visitors
                    .Include(v => v.Purpose)
                    .Include(v => v.VisitorDevices)
                        .ThenInclude(vd => vd.Device)
                    .Where(v => v.VisitDate == today))
                    .ToListAsync();

                var visitorLogDtos = _mapper.Map<List<VisitorLogDTO>>(visitorDetail);

                foreach (var dto in visitorLogDtos)
                {
                    if (dto.Photo != null)
                    {
                        dto.PhotoBase64 = Convert.ToBase64String(dto.Photo);
                    }
                }
                _logger.LogInformation("Retrieved {Count} visitor logs for date {Date}.", visitorLogDtos.Count, today);
                return visitorLogDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting visitor logs for date: {Date}.", today);
                throw;
            }
        }

        public Task<int> GetActiveVisitorsCountToday()
        {
            _logger.LogInformation("Getting active visitors count for today.");
            return GetVisitorCount(v => v.Where(visitor => visitor.CheckInTime != null && visitor.CheckOutTime == null));
        }

        public Task<int> GetTotalVisitorsCountToday()
        {
            _logger.LogInformation("Getting total visitors count for today.");
            return GetVisitorCount(v => v.Where(visitor => visitor.CheckInTime != null || visitor.CheckOutTime != null));
        }

        public Task<int> GetCheckedOutVisitorsCountToday()
        {
            _logger.LogInformation("Getting checked-out visitors count for today.");
            return GetVisitorCount(v => v.Where(visitor => visitor.CheckOutTime != null));
        }

        public Task<IEnumerable<VisitorLogDTO>> GetVisitorDetailsToday()
        {
            _logger.LogInformation("Getting visitor details for today.");
            return GetVisitorLogs(v => v.Where(visitor => visitor.CheckInTime != null || visitor.CheckOutTime != null));
        }

        public Task<IEnumerable<VisitorLogDTO>> GetUpcomingVisitorsToday()
        {
            _logger.LogInformation("Getting upcoming visitors for today.");
            return GetVisitorLogs(v => v.Where(visitor => visitor.CheckInTime == null));
        }

        public Task<IEnumerable<VisitorLogDTO>> GetActiveVisitorsToday()
        {
            _logger.LogInformation("Getting active visitors for today.");
            return GetVisitorLogs(v => v.Where(visitor => visitor.CheckInTime != null && visitor.CheckOutTime == null));
        }

        public Task<IEnumerable<VisitorLogDTO>> GetCheckedOutVisitorsToday()
        {
            _logger.LogInformation("Getting checked-out visitors for today.");
            return GetVisitorLogs(v => v.Where(visitor => visitor.CheckInTime != null && visitor.CheckOutTime != null));
        }

        public async Task<VisitorLogDTO> UpdateCheckInTimeAndCardNumber(int id, UpdateVisitorPassCodeDTO updateVisitorPassCode)
        {
            _logger.LogInformation("Updating check-in time and card number for visitor ID: {VisitorId}.", id);
            try
            {
                var existingVisitor = await _context.Visitors.FindAsync(id);
                if (existingVisitor == null)
                {
                    _logger.LogWarning("Visitor with ID {VisitorId} not found.", id);
                    return null;
                }

                // Check if the VisitorPassCode already exists
                bool passCodeExists = await _context.Visitors.AnyAsync(v => v.VisitorPassCode == updateVisitorPassCode.VisitorPassCode && v.Id != id);
                if (passCodeExists)
                {
                    _logger.LogWarning("Visitor pass code {PassCode} is already allocated.", updateVisitorPassCode.VisitorPassCode);
                    throw new ArgumentException("This visitor pass code has already been allocated.");
                }

                existingVisitor.CheckInTime = DateTime.Now;
                existingVisitor.VisitorPassCode = updateVisitorPassCode.VisitorPassCode;
                existingVisitor.UpdatedDate = DateTime.Now;

                await _context.SaveChangesAsync();
                var visitorLogDTO = _mapper.Map<VisitorLogDTO>(existingVisitor);

                _logger.LogInformation("Updated check-in time and card number for visitor ID: {VisitorId} successfully.", id);
                return visitorLogDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating check-in time and card number for visitor ID: {VisitorId}.", id);
                throw;
            }
        }


        public async Task<VisitorLogDTO> UpdateCheckOutTime(int id)
        {
            _logger.LogInformation("Updating check-out time for visitor ID: {VisitorId}.", id);
            try
            {
                var existingVisitor = await _context.Visitors.FindAsync(id);
                if (existingVisitor == null)
                {
                    _logger.LogWarning("Visitor with ID {VisitorId} not found.", id);
                    return null;
                }

                existingVisitor.CheckOutTime = DateTime.Now;
                existingVisitor.UpdatedDate = DateTime.Now;
                existingVisitor.VisitorPassCode = 0;

                await _context.SaveChangesAsync();

                // Map the updated entity to VisitorLogDTO
                var visitorLogDTO = _mapper.Map<VisitorLogDTO>(existingVisitor);
                _logger.LogInformation("Updated check-out time for visitor ID: {VisitorId} successfully.", id);
                return visitorLogDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating check-out time for visitor ID: {VisitorId}.", id);
                throw;
            }
        }
    }
}
