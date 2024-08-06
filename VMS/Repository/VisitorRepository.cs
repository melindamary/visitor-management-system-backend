using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<VisitorRepository> _logger;

        public VisitorRepository(VisitorManagementDbContext context, IMapper mapper, ILogger<VisitorRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Visitor> GetVisitorByIdAsync(int id)
        {
            return await _context.Visitors.FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<int> GetVisitorCount(Func<IQueryable<Visitor>, IQueryable<Visitor>> filter)
        {
            DateTime today = DateTime.Today;
            _logger.LogInformation("Fetching visitor count for today.");

            var count = await filter(_context.Visitors
                .Where(v => v.VisitDate == today))
                .CountAsync();

            _logger.LogInformation("Visitor count for today: {Count}", count);
            return count;
        }

        public async Task<IEnumerable<VisitorLogDTO>> GetVisitorLogs(Func<IQueryable<Visitor>, IQueryable<Visitor>> filter)
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

        public async Task<VisitorLogDTO> UpdateCheckInTimeAndCardNumber(int id, UpdateVisitorPassCodeDTO updateVisitorPassCode)
        {
            _logger.LogInformation("Updating check-in time and pass code for visitor ID {VisitorId}.", id);

            var existingVisitor = await _context.Visitors.FindAsync(id);
            if (existingVisitor == null)
            {
                _logger.LogWarning("Visitor ID {VisitorId} not found.", id);
                return null;
            }

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

        public async Task<VisitorLogDTO> UpdateCheckOutTime(int id)
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
    }
}
