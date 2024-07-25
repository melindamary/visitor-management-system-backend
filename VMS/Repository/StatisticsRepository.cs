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
            var securityCount = from ur in _context.UserRoles
                                join ud in _context.UserDetails on ur.UserId equals ud.UserId
                                join ol in _context.OfficeLocations on ud.OfficeLocationId equals ol.Id
                                join r in _context.Roles on ur.RoleId equals r.Id
                                where r.Name == "Security"
                                group ol by ol.Name into scGroup
                                select new
                                {
                                    LocationName = scGroup.Key,
                                    NumberOfSecurity = scGroup.Count()
                                };

            var passesGenerated = from v in _context.Visitors
                                  join ol in _context.OfficeLocations on v.OfficeLocationId equals ol.Id
                                  group ol by ol.Name into pgGroup
                                  select new
                                  {
                                      LocationName = pgGroup.Key,
                                      PassesGenerated = pgGroup.Count()
                                  };

            var totalVisitors = from v in _context.Visitors
                                join ol in _context.OfficeLocations on v.OfficeLocationId equals ol.Id
                                where v.CheckInTime != null
                                group ol by ol.Name into tvGroup
                                select new
                                {
                                    LocationName = tvGroup.Key,
                                    TotalVisitors = tvGroup.Count()
                                };

            var locationStatistics = from ol in _context.OfficeLocations
                                     join sc in securityCount on ol.Name equals sc.LocationName into scLeftJoin
                                     from sc in scLeftJoin.DefaultIfEmpty()
                                     join pg in passesGenerated on ol.Name equals pg.LocationName into pgLeftJoin
                                     from pg in pgLeftJoin.DefaultIfEmpty()
                                     join tv in totalVisitors on ol.Name equals tv.LocationName into tvLeftJoin
                                     from tv in tvLeftJoin.DefaultIfEmpty()
                                     select new LocationStatisticsDTO
                                     {
                                         Location = ol.Name,
                                         NumberOfSecurity = sc != null ? sc.NumberOfSecurity : 0,
                                         PassesGenerated = pg != null ? pg.PassesGenerated : 0,
                                         TotalVisitors = tv != null ? tv.TotalVisitors : 0
                                     };

            return await locationStatistics.ToListAsync();
        }
        public async Task<IEnumerable<SecurityStatisticsDTO>> GetSecurityStatistics()
        {
            var sevenDaysAgo = DateTime.Now.AddDays(-7);

            var securityDetails = await (from ol in _context.OfficeLocations
                                         join ud in _context.UserDetails on ol.Id equals ud.OfficeLocationId
                                         join u in _context.Users on ud.UserId equals u.Id
                                         join ur in _context.UserRoles on u.Id equals ur.UserId
                                         join r in _context.Roles on ur.RoleId equals r.Id
                                         where r.Name == "Security"
                                         let visitors = _context.Visitors
                                             .Where(v => v.OfficeLocationId == ol.Id &&
                                                         v.UpdatedBy == u.Id && 
                                                         v.VisitDate >= sevenDaysAgo)
                                             .Select(v => v.Id)
                                             .Distinct()
                                             .ToList() // Convert IQueryable to List
                                         select new SecurityStatisticsDTO
                                         {
                                             Location = ol.Name,
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

         public async Task<IEnumerable<PurposeStatisticsDTO>> GetPurposeStatistics()
        {
            var thirtyDaysAgo = DateTime.Now.AddDays(-30);

            var purposeStatistics = await _context.PurposeOfVisits
                .GroupJoin(
                    _context.Visitors.Where(v => v.VisitDate >= thirtyDaysAgo),
                    pov => pov.Id,
                    v => v.PurposeId,
                    (pov, visitors) => new PurposeStatisticsDTO
                    {
                        Name = pov.Name,
                        Value = visitors.Count()
                    })
                .OrderByDescending(x => x.Value)
                .ToListAsync();

            return purposeStatistics;
        }



        public async Task<IEnumerable<DashboardStatisticsDTO>> GetDashboardStatistics()
        {
            var result = await (from o in _context.OfficeLocations
                                join v in _context.Visitors on o.Id equals v.OfficeLocationId into vGroup
                                from v in vGroup.DefaultIfEmpty()
                                group new { o, v } by o.Name into g
                                select new DashboardStatisticsDTO
                                {
                                    Location = g.Key,
                                    PassesGenerated = g.Count(x => x.v != null),
                                    ActiveVisitors = g.Count(x => x.v != null && x.v.VisitorPassCode != null && x.v.CheckOutTime == null),
                                    TotalVisitors = g.Count(x => x.v != null && x.v.CheckInTime != null)
                                })
                                .ToListAsync();

            return result;
        }

    }
}