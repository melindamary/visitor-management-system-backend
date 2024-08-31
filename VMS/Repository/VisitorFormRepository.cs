using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using VMS.AVHubs;
using VMS.Data;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository.IRepository;
using VMS.Services;

namespace VMS.Repository
{
    public class VisitorFormRepository : IVisitorFormRepository
    {
        private readonly DashboardService _dashboardService;

        private readonly IHubContext<VisitorHub> _hubContext;
        private readonly VisitorManagementDbContext _context;
        public const int _systemUserId = 1;
        public const int _deafaultPassCode = 0;
       /* public const string _formSubmissionMode = "Kiosk";*/

        public VisitorFormRepository(VisitorManagementDbContext context, IHubContext<VisitorHub> hubContext, DashboardService dashboardService)
        {
            _context = context;
            _hubContext = hubContext;
            _dashboardService = dashboardService;
        }
        public async Task<VisitorDevice> AddVisitorDeviceAsync(AddVisitorDeviceDTO addDeviceDto)
        {
            var visitorDevice = new VisitorDevice
            {
                VisitorId = addDeviceDto.VisitorId,
                DeviceId = addDeviceDto.DeviceId,
                SerialNumber = addDeviceDto.SerialNumber,
                CreatedBy = _systemUserId,
                UpdatedBy = _systemUserId
            };

            _context.VisitorDevices.Add(visitorDevice);
            await _context.SaveChangesAsync();

            return visitorDevice;
        }


        public async Task<Visitor> CreateVisitorAsync(VisitorCreationDTO visitorDto)
        {
            if (visitorDto == null)
            {
                throw new ArgumentNullException(nameof(visitorDto));
            }

            var visitor = new Visitor
            {
                Name = visitorDto.Name,
                Phone = visitorDto.PhoneNumber,
                PurposeId = visitorDto.PurposeOfVisitId,
                HostName = visitorDto.PersonInContact,
                OfficeLocationId = visitorDto.OfficeLocationId,
                FormSubmissionMode = visitorDto.formSubmissionMode,
                VisitDate = visitorDto.VisitDate,
                CheckedInBy = _systemUserId,
                CreatedBy = _systemUserId,
                VisitorPassCode= _deafaultPassCode,                
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                VisitorDevices = new List<VisitorDevice>()
            };

         
           if(!string.IsNullOrEmpty(visitorDto.ImageData))
            {
                var imageDataParts = visitorDto.ImageData.Split(',');
                if
                (imageDataParts.Length > 1)
                {
                    var
                    imageDataBytes = Convert.FromBase64String(imageDataParts[1]); visitor.Photo = imageDataBytes;
                }
            }

            _context.Visitors.Add(visitor);
            await _context.SaveChangesAsync();


/*            var hubContext = _serviceProvider.GetRequiredService<IHubContext<VisitorHub>>();
*/            await _hubContext.Clients.All.SendAsync("ReceiveVisitorCount", await _dashboardService.GetVisitorCountAsync());
            await _hubContext.Clients.All.SendAsync("ReceiveScheduledVisitorsCount", await _dashboardService.GetScheduledVisitorsCountAsync());
            await _hubContext.Clients.All.SendAsync("ReceiveTotalVisitorsCount", await _dashboardService.GetTotalVisitorsCountAsync());

            // Update Location Statistics
            await _hubContext.Clients.All.SendAsync("ReceiveLocationStatistics", await _dashboardService.GetLocationStatistics(1));
            await _hubContext.Clients.All.SendAsync("ReceiveLocationStatistics", await _dashboardService.GetSecurityStatistics(1));



            
            
            return visitor;
           
        }


        public async Task<IEnumerable<string>> GetPersonInContactAsync()
        {
            return await _context.Visitors.Select(v => v.HostName).Distinct().ToListAsync();
        }

        public async Task<Visitor> GetVisitorByIdAsync(int id)
        {
            return await _context.Visitors.FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Visitor>> GetVisitorDetailsAsync()
        {
            return await _context.Visitors.ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

       
    }
}
