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

        public VisitorRepository(VisitorManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<int> GetVisitorCount(Func<IQueryable<Visitor>, IQueryable<Visitor>> filter)
        {
            DateTime today = DateTime.Today;
            return await filter(_context.Visitors
                .Where(v => v.VisitDate == today))
                .CountAsync();
        }

        private async Task<IEnumerable<VisitorLogDTO>> GetVisitorLogs(Func<IQueryable<Visitor>, IQueryable<Visitor>> filter)
        {
            DateTime today = DateTime.Today;

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

            return visitorLogDtos;
        }

        public Task<int> GetActiveVisitorsCountToday()
        {
            return GetVisitorCount(v => v.Where(visitor => visitor.CheckInTime != null && visitor.CheckOutTime == null));
        }

        public Task<int> GetTotalVisitorsCountToday()
        {
            return GetVisitorCount(v => v.Where(visitor => visitor.CheckInTime != null || visitor.CheckOutTime != null));
        }

        public Task<int> GetCheckedOutVisitorsCountToday()
        {
            return GetVisitorCount(v => v.Where(visitor => visitor.CheckOutTime != null));
        }

        public Task<IEnumerable<VisitorLogDTO>> GetVisitorDetailsToday()
        {
            return GetVisitorLogs(v => v);
        }

        public Task<IEnumerable<VisitorLogDTO>> GetUpcomingVisitorsToday()
        {
            return GetVisitorLogs(v => v.Where(visitor => visitor.CheckInTime == null));
        }

        public Task<IEnumerable<VisitorLogDTO>> GetActiveVisitorsToday()
        {
            return GetVisitorLogs(v => v.Where(visitor => visitor.CheckInTime != null && visitor.CheckOutTime == null));
        }

        public Task<IEnumerable<VisitorLogDTO>> GetCheckedOutVisitorsToday()
        {
            return GetVisitorLogs(v => v.Where(visitor => visitor.CheckInTime != null && visitor.CheckOutTime != null));
        }

        public async Task<VisitorLogDTO> UpdateCheckInTimeAndCardNumber(int id, UpdateVisitorPassCodeDTO updateVisitorPassCode)
        {
            var existingVisitor = await _context.Visitors.FindAsync(id);
            if (existingVisitor == null)
            {
                return null;
            }

            // Check if the VisitorPassCode already exists
            bool passCodeExists = await _context.Visitors.AnyAsync(v => v.VisitorPassCode == updateVisitorPassCode.VisitorPassCode && v.Id != id);
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
            existingVisitor.VisitorPassCode = "0";

            await _context.SaveChangesAsync();

            // Map the updated entity to VisitorLogDTO
            var visitorLogDTO = _mapper.Map<VisitorLogDTO>(existingVisitor);
            return visitorLogDTO;
        }
    }
}
