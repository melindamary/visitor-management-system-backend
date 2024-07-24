using Microsoft.EntityFrameworkCore;
using VMS.Data;
using VMS.Models.DTO;
using VMS.Repository.IRepository;

namespace VMS.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly VisitorManagementDbContext _context;
        public ReportRepository(VisitorManagementDbContext context)
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

        public async Task<VisitorDetailsDTO> GetVisitorDetailsAsync(int id)
        {

            var visitorDetails = await (from visitor in _context.Visitors
                                        join purpose in _context.PurposeOfVisits on visitor.PurposeId equals purpose.PurposeId
                                        join location in _context.OfficeLocations on visitor.OfficeLocationId equals location.OfficeLocationId
                                        where visitor.VisitorId == id && visitor.CheckInTime != null && visitor.CheckOutTime != null
                                        select new VisitorDetailsDTO
                                        {
                                            Name = visitor.VisitorName,
                                            Phone = visitor.Phone,
                                            VisitDate = visitor.VisitDate,
                                            HostName = visitor.HostName,
                                            OfficeLocation = location.LocationName,
                                            CheckInTime = visitor.CheckInTime,
                                            CheckOutTime = visitor.CheckOutTime,
                                            VisitPurpose = purpose.PurposeName,
                                            Photo = Convert.ToBase64String(visitor.Photo)

                                        }).FirstOrDefaultAsync();


            return visitorDetails;
        }
    }
}

 
