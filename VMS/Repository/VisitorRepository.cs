using AutoMapper;
using VMS.Data;
using VMS.Models.DTO;
using VMS.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace VMS.Repository
{
    public class VisitorRepository : IVisitorRepository
    {
        private readonly VisitorManagementDbContext _context;
        private readonly IMapper _mapper;

        public VisitorRepository(VisitorManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> GetActiveVisitorsCountToday()
        {
            DateTime today = DateTime.Today;
            return await _context.Visitors
                .Where(v => v.CheckInTime != null && v.CheckOutTime == null && v.VisitDate == today)
               .CountAsync();
        }

        public async Task<int> GetTotalVisitorsCountToday()
        {
            DateTime today = DateTime.Today;
            return await _context.Visitors
                .Where(v => v.VisitDate == today)
                .CountAsync();
        }

        public async Task<IEnumerable<VisitorLogDTO>> GetUpcomingVisitorsToday()
        {
            DateTime today = DateTime.Today;
            var upcomingVisitors = await _context.Visitors
                .Include(v => v.Purpose)
                .Include(v => v.VisitorDevices)
                    .ThenInclude(vd => vd.Device)
                .Where(v => v.CheckInTime == null && v.VisitDate == today)
                .ToListAsync();
            var visitorLogDtos = _mapper.Map<List<VisitorLogDTO>>(upcomingVisitors);
            foreach (var dto in visitorLogDtos)
            {
                if (dto.Photo != null)
                {
                    dto.PhotoBase64 = Convert.ToBase64String(dto.Photo);
                }
            }

            return visitorLogDtos;
        }

        public async Task<IEnumerable<VisitorLogDTO>> GetActiveVisitorsToday()
        {
            DateTime today = DateTime.Today;
            var activeVisitors = await _context.Visitors
                .Include(v => v.Purpose)
                .Include(v => v.VisitorDevices)
                    .ThenInclude(vd => vd.Device)
                .Where(v => v.CheckInTime != null && v.CheckOutTime == null && v.VisitDate == today)
                .ToListAsync();

            var visitorLogDtos = _mapper.Map<List<VisitorLogDTO>>(activeVisitors);
            foreach (var dto in visitorLogDtos)
            {
                if (dto.Photo != null)
                {
                    dto.PhotoBase64 = Convert.ToBase64String(dto.Photo);
                }
            }

            return visitorLogDtos;
        }


        public async Task<int> GetCheckedOutVisitorsCountToday()
        {
            DateTime today = DateTime.Today;
            return await _context.Visitors
                .Where(v => v.CheckOutTime != null && v.VisitDate == today)
                .CountAsync();
        }

        public async Task<VisitorLogDTO> UpdateCheckInTimeAndCardNumber(int id, UpdateVisitorPassCodeDTO updateVisitorPassCode)
        {
            var existingVisitor = await _context.Visitors.FindAsync(id);
            if (existingVisitor == null)
            {
                return null;
            }

            // Check if the VisitorPassCode already exists
            bool passCodeExists = await _context.Visitors.AnyAsync(v => v.VisitorPassCode == updateVisitorPassCode.VisitorPassCode && v.VisitorId != id);
            if (passCodeExists)
            {
                throw new ArgumentException("This visitor pass code has already been allocated.");
            }

            existingVisitor.CheckInTime = DateTime.Now;
            existingVisitor.VisitorPassCode = updateVisitorPassCode.VisitorPassCode;
            existingVisitor.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            var visitorLogDTO = _mapper.Map<VisitorLogDTO>(existingVisitor);
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

            await _context.SaveChangesAsync();

            // Map the updated entity to VisitorLogDTO
            var visitorLogDTO = _mapper.Map<VisitorLogDTO>(existingVisitor);
            return visitorLogDTO;
        }
    }
}
