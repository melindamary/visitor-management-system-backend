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
                                  join purpose in _context.PurposeOfVisits on visitor.PurposeId equals purpose.Id
                                  join location in _context.OfficeLocations on visitor.OfficeLocationId equals location.Id
                                  join user in _context.UserDetails on visitor.CheckedInBy equals user.UserId
                                  where visitor.CheckInTime != null && visitor.CheckOutTime != null
                                    let devices = (from visitorDevice in _context.VisitorDevices
                                                  join device in _context.Devices on visitorDevice.DeviceId equals device.Id
                                                  where visitorDevice.VisitorId == visitor.Id
                                                  select new DeviceDetailsDTO
                                                    {
                                                     SerialNumber = visitorDevice.SerialNumber,
                                                     Name = device.Name
                                                    }).ToList()
                                              select new VisitorReportDetailsDTO
                                              {
                                                  VisitorId = visitor.Id,
                                                  VisitorName = visitor.Name,
                                                  Phone = visitor.Phone,
                                                  VisitDate = visitor.VisitDate,
                                                  HostName = visitor.HostName,
                                                  PurposeName = purpose.Name,
                                                  LocationName = location.Name,
                                                  StaffName = user.FirstName + " " + user.LastName,
                                                  StaffPhoneNumber = user.Phone,
                                                  CheckInTime = visitor.CheckInTime,
                                                  CheckOutTime = visitor.CheckOutTime,
                                                  Photo = Convert.ToBase64String(visitor.Photo),
                                                  DeviceCount = devices.Count,
                                                  Devices = devices
                                              }).ToListAsync();

            return visitors;
        }

        public async Task<VisitorDetailsDTO> GetVisitorDetailsAsync(int id)
        {

            var visitorDetails = await (from visitor in _context.Visitors
                                        join purpose in _context.PurposeOfVisits on visitor.PurposeId equals purpose.Id
                                        join location in _context.OfficeLocations on visitor.OfficeLocationId equals location.Id
                                        where visitor.Id == id && visitor.CheckInTime != null && visitor.CheckOutTime != null
                                        select new
                                        {
                                            Visitor = new VisitorDetailsDTO
                                            {
                                                Name = visitor.Name,
                                                Phone = visitor.Phone,
                                                VisitDate = visitor.VisitDate,
                                                HostName = visitor.HostName,
                                                OfficeLocation = location.Name,
                                                CheckInTime = visitor.CheckInTime,
                                                CheckOutTime = visitor.CheckOutTime,
                                                VisitPurpose = purpose.Name,
                                                Photo = Convert.ToBase64String(visitor.Photo),
                                                DeviceCount = _context.VisitorDevices.Count(u => u.VisitorId == id)
                                            },
                                            Devices =(from visitorDevice in _context.VisitorDevices
                                                       join device in _context.Devices on visitorDevice.DeviceId equals device.Id
                                                       where visitorDevice.VisitorId == id
                                                       select new DeviceDetailsDTO
                                                       {
                                                           SerialNumber = visitorDevice.SerialNumber,
                                                           Name = device.Name
                                                       }).ToList()
                                        }).FirstOrDefaultAsync();


            if (visitorDetails == null)
        return null;

    // Assign devices to visitor details
    visitorDetails.Visitor.DevicesCarried = visitorDetails.Devices;

    return visitorDetails.Visitor;
        }
    }
}

 
