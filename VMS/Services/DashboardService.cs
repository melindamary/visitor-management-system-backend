using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using VMS.Data;
using VMS.Models.DTO;
using VMS.Repository;
using VMS.Repository.IRepository;

namespace VMS.Services
{
    public class DashboardService
    {
        private readonly VisitorManagementDbContext _context;
        private readonly IStatisticsRepository _repository;

        public DashboardService(VisitorManagementDbContext context, IStatisticsRepository repository)
        {
            _context = context;
            _repository = repository;
        }


        public async Task<int> GetVisitorCountAsync()
        {
            {
                var today = DateTime.Today;

                // Use LINQ to get the count of visitors where check-in time is not null, check-out time is null, and check-in time is today
                return await _context.Visitors
                    .CountAsync(v => v.CheckInTime != null
                                     && v.CheckOutTime == null
                                     && v.CheckInTime.Value.Date == today);
            }
        }

        public async Task<int> GetTotalVisitorsCountAsync()
        {
            var today = DateTime.Today;
            return await _context.Visitors.CountAsync(
                v => v.CheckInTime != null
                && v.CheckInTime.Value.Date == today
                );
        }

        public async Task<int> GetScheduledVisitorsCountAsync()
        {
            var today = DateTime.Today;
            return await _context.Visitors
                .CountAsync(v => v.CheckInTime != null
                                 && v.CheckOutTime != null
                                 && v.CheckInTime.Value.Date == today);
        }
        public async Task<IEnumerable<LocationStatisticsDTO>> GetLocationStatistics(int days)
        {
            return await _repository.GetLocationStatistics(7);
        
        }
        public async Task<IEnumerable<LocationStatisticsDTO>> GetSecurityStatistics(int days)
        {
            return await _repository.GetLocationStatistics(7);

        }
    }
}


