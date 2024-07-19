// File: Repository/StatisticsRepository.cs
using Microsoft.EntityFrameworkCore;
using VMS.Data;
using VMS.Models.DTO;
using VMS.Repository.IRepository;

namespace VMS.Repository
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly VisitorManagementDbContext _context;

        public StatisticsRepository(VisitorManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LocationStatisticsDTO>> GetLocationStatistics()
        {
            var result = await _context.OfficeLocations
                .GroupJoin(
                    _context.UserDetails,
                    ol => ol.OfficeLocationId,
                    ud => ud.OfficeLocationId,
                    (ol, ud) => new { ol, ud }
                )
                .SelectMany(
                    x => x.ud.DefaultIfEmpty(),
                    (x, ud) => new { x.ol, ud }
                )
                .GroupJoin(
                    _context.Users,
                    x => x.ud.UserId,
                    u => u.UserId,
                    (x, u) => new { x.ol, x.ud, u }
                )
                .SelectMany(
                    x => x.u.DefaultIfEmpty(),
                    (x, u) => new { x.ol, x.ud, u }
                )
                .GroupJoin(
                    _context.UserRoles,
                    x => x.u.UserId,
                    ur => ur.UserId,
                    (x, ur) => new { x.ol, x.ud, x.u, ur }
                )
                .SelectMany(
                    x => x.ur.DefaultIfEmpty(),
                    (x, ur) => new { x.ol, x.ud, x.u, ur }
                )
                .GroupJoin(
                    _context.Roles,
                    x => x.ur.RoleId,
                    r => r.RoleId,
                    (x, r) => new { x.ol, x.ud, x.u, x.ur, r }
                )
                .SelectMany(
                    x => x.r.DefaultIfEmpty(),
                    (x, r) => new { x.ol, x.ud, x.u, x.ur, r }
                )
                .GroupJoin(
                    _context.Visitors,
                    x => x.ol.OfficeLocationId,
                    v => v.OfficeLocationId,
                    (x, v) => new { x.ol, x.ud, x.u, x.ur, x.r, v }
                )
                .SelectMany(
                    x => x.v.DefaultIfEmpty(),
                    (x, v) => new { x.ol, x.ud, x.u, x.ur, x.r, v }
                )
                .GroupBy(x => new { x.ol.LocationName })
                .Select(g => new LocationStatisticsDTO
                {
                    Location = g.Key.LocationName,
                    NumberOfSecurity = g.Select(x => x.u.UserId).Where(id => id != null && g.Any(y => y.r.RoleName == "Security" && y.u.UserId == id)).Distinct().Count(),
                    PassesGenerated = g.Select(x => x.v.VisitorPassCode).Where(code => code != null).Distinct().Count(),
                    TotalVisitors = g.Select(x => x.v.VisitorId).Where(id => id != null).Distinct().Count()
                })
                .ToListAsync();

            return result;
        }
        public async Task<IEnumerable<SecurityStatisticsDTO>> GetSecurityStatistics()
        {
            var sevenDaysAgo = DateTime.Now.AddDays(-7);

            var securityDetails = await (from ol in _context.OfficeLocations
                                         join ud in _context.UserDetails on ol.OfficeLocationId equals ud.OfficeLocationId
                                         join u in _context.Users on ud.UserId equals u.UserId
                                         join ur in _context.UserRoles on u.UserId equals ur.UserId
                                         join r in _context.Roles on ur.RoleId equals r.RoleId
                                         where r.RoleName == "Security"
                                         let visitors = _context.Visitors
                                             .Where(v => v.OfficeLocationId == ol.OfficeLocationId &&
                                                         v.UserId == u.UserId &&
                                                         v.VisitDate >= sevenDaysAgo)
                                             .Select(v => v.VisitorId)
                                             .Distinct()
                                             .ToList() // Convert IQueryable to List
                                         select new SecurityStatisticsDTO
                                         {
                                             Location = ol.LocationName,
                                             SecurityFirstName = ud.FirstName,
                                             SecurityLastName = ud.LastName,
                                             PhoneNumber = ud.Phone,
                                             Status = u.IsActive,
                                             VisitorsApproved = visitors.Count() // Use the Count property of List
                                         }).OrderBy(sd => sd.Location)
                                           .ThenBy(sd => sd.SecurityLastName)
                                           .ThenBy(sd => sd.SecurityFirstName)
                                           .ToListAsync();

            return securityDetails;
        }
    }
}