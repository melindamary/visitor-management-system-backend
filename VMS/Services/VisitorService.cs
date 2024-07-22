using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using VMS.Data;
using VMS.Models.DTO;

namespace VMS.Services
{
    public class VisitorService
    {
        private readonly VisitorManagementDbContext _context;
        private readonly IMapper _mapper;

        public VisitorService(VisitorManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public string ConvertImageToBase64(byte[] imageBytes, int quality)
        {
            using (var image = Image.Load(imageBytes))
            using (var compressedStream = new MemoryStream())
            {
                var encoder = new JpegEncoder { Quality = quality };
                image.Save(compressedStream, encoder);
                return Convert.ToBase64String(compressedStream.ToArray());
            }
        }

        public async Task<IEnumerable<VisitorLogDTO>> GetVisitorDetailsToday()
        {
            DateTime today = DateTime.Today;

            var visitorDetail = await _context.Visitors
                .Include(v => v.Purpose)
                .Include(v => v.VisitorDevices)
                    .ThenInclude(vd => vd.Device)
                .Where(v => v.VisitDate == today)
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
    }
}
