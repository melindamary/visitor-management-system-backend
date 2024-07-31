using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Add this using directive
using VMS.Data;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository.IRepository;

namespace VMS.Repository
{
    public class VisitorRepository : IVisitorRepository
    {
        private readonly VisitorManagementDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<VisitorRepository> _logger; // Add this field

        public VisitorRepository(VisitorManagementDbContext context, IMapper mapper, ILogger<VisitorRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger; // Initialize the logger
        }
        public async Task<Visitor> GetVisitorByIdAsync(int id)
        {
            return await _context.Visitors.FirstOrDefaultAsync(v => v.Id == id);
        }
        public async Task<int> GetVisitorCount(Func<IQueryable<Visitor>, IQueryable<Visitor>> filter)
        {
            try
            {
                DateTime today = DateTime.Today;
                _logger.LogInformation("Fetching visitor count for today.");

                var count = await filter(_context.Visitors
                    .Where(v => v.VisitDate == today))
                    .CountAsync();

                _logger.LogInformation("Visitor count for today: {Count}", count);
                return count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching visitor count for today.");
                throw;
            }
        }

        public async Task<IEnumerable<VisitorLogDTO>> GetVisitorLogs(Func<IQueryable<Visitor>, IQueryable<Visitor>> filter)
        {
            try
            {
                DateTime today = DateTime.Today;
                _logger.LogInformation("Fetching visitor logs for today.");

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

                _logger.LogInformation("Fetched {Count} visitor logs for today.", visitorLogDtos.Count);
                return visitorLogDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching visitor logs for today.");
                throw;
            }
        }

        public async Task<VisitorLogDTO> UpdateCheckInTimeAndCardNumber(int id, UpdateVisitorPassCodeDTO updateVisitorPassCode)
        {
            try
            {
                _logger.LogInformation("Updating check-in time and pass code for visitor ID {VisitorId}.", id);

                var existingVisitor = await _context.Visitors.FindAsync(id);
                if (existingVisitor == null)
                {
                    _logger.LogWarning("Visitor ID {VisitorId} not found.", id);
                    return null;
                }

                // Check if the VisitorPassCode already exists
                bool passCodeExists = await _context.Visitors.AnyAsync(v => v.VisitorPassCode == updateVisitorPassCode.VisitorPassCode && v.Id != id);
                if (passCodeExists)
                {
                    _logger.LogWarning("Visitor pass code {PassCode} already allocated.", updateVisitorPassCode.VisitorPassCode);
                    throw new ArgumentException("This visitor pass code has already been allocated.");
                }

                existingVisitor.CheckInTime = DateTime.Now;
                existingVisitor.VisitorPassCode = updateVisitorPassCode.VisitorPassCode;
                existingVisitor.UpdatedDate = DateTime.Now;

                await _context.SaveChangesAsync();
                var visitorLogDTO = _mapper.Map<VisitorLogDTO>(existingVisitor);

                _logger.LogInformation("Successfully updated check-in time and pass code for visitor ID {VisitorId}.", id);
                return visitorLogDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating check-in time and pass code for visitor ID {VisitorId}.", id);
                throw;
            }
        }

        public async Task<VisitorLogDTO> UpdateCheckOutTime(int id)
        {
            try
            {
                _logger.LogInformation("Updating check-out time for visitor ID {VisitorId}.", id);

                var existingVisitor = await _context.Visitors.FindAsync(id);
                if (existingVisitor == null)
                {
                    _logger.LogWarning("Visitor ID {VisitorId} not found.", id);
                    return null;
                }

                existingVisitor.CheckOutTime = DateTime.Now;
                existingVisitor.UpdatedDate = DateTime.Now;
                existingVisitor.VisitorPassCode = 0;

                await _context.SaveChangesAsync();

                var visitorLogDTO = _mapper.Map<VisitorLogDTO>(existingVisitor);

                _logger.LogInformation("Successfully updated check-out time for visitor ID {VisitorId}.", id);
                return visitorLogDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating check-out time for visitor ID {VisitorId}.", id);
                throw;
            }
        }
    }
}
