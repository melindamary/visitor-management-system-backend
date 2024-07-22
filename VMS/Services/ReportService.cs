using Microsoft.EntityFrameworkCore;
using System;
using VMS.Data;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Services.IServices;

namespace VMS.Services
{
    public class ReportService : IReportService
    {
        private readonly VisitorManagementDbContext _context;
        public ReportService(VisitorManagementDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<VisitorReportDetailsDTO>> GetAllVisitorsAsync()
        {
            var visitors = await (from visitor in _context.Visitors
                                  join purpose in _context.PurposeOfVisits on visitor.PurposeId equals purpose.PurposeId
                                  join location in _context.OfficeLocations on visitor.OfficeLocationId equals location.OfficeLocationId
                                  join user in _context.UserDetails on visitor.UserId equals user.UserId
                                  where visitor.CheckInTime != null && visitor.CheckOutTime != null
                                  select new VisitorReportDetailsDTO
                                  {
                                      VisitorId = visitor.VisitorId,
                                      VisitorName = visitor.VisitorName,
                                      Phone = visitor.Phone,
                                      VisitDate = visitor.VisitDate,
                                      HostName = visitor.HostName,
                                      PurposeName = purpose.PurposeName,
                                      LocationName = location.LocationName,
                                      StaffName = user.FirstName + " " + user.LastName,
                                      StaffPhoneNumber = user.Phone,
                                      CheckInTime = visitor.CheckInTime,
                                      CheckOutTime = visitor.CheckOutTime,
                                  }).ToListAsync();
            return visitors;
        }
    }
}
