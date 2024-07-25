using Microsoft.EntityFrameworkCore;
using VMS.Data;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository.IRepository;

namespace VMS.Repository
{
    public class VisitorFormRepository : IVisitorFormRepository
    {
        private readonly VisitorManagementDbContext _context;

        public VisitorFormRepository(VisitorManagementDbContext context)
        {
            _context = context;
        }
        public async Task<VisitorDevice> AddVisitorDeviceAsync(AddVisitorDeviceDTO addDeviceDto)
        {
            var visitorDevice = new VisitorDevice
            {
                VisitorId = addDeviceDto.VisitorId,
                DeviceId = addDeviceDto.DeviceId,
                SerialNumber = addDeviceDto.SerialNumber,
                CreatedBy = 1,
                UpdatedBy = 1
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
                VisitorName = visitorDto.Name,
                Phone = visitorDto.PhoneNumber,
                PurposeId = visitorDto.PurposeOfVisitId,
                HostName = visitorDto.PersonInContact,
                OfficeLocationId = visitorDto.OfficeLocationId,
                UserId = 1,
                CreatedBy = 1,
                VisitDate = DateTime.Now.Date,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                VisitorDevices = new List<VisitorDevice>()
            };

            /* if (!string.IsNullOrEmpty(visitorDto.ImageData))
             {
                 var imageDataBytes = Convert.FromBase64String(visitorDto.ImageData.Split(',')[1]);
                 visitor.Photo = imageDataBytes;
             }*/
                            if
                 (!
                 string
                 .IsNullOrEmpty(visitorDto.ImageData))
            {
                var
                imageDataParts = visitorDto.ImageData.Split(
                ','
                );
                if
                (imageDataParts.Length > 1)
                {
                    var
                    imageDataBytes = Convert.FromBase64String(imageDataParts[1]); visitor.Photo = imageDataBytes;
                }
            }

            _context.Visitors.Add(visitor);
            await _context.SaveChangesAsync();


            if (visitorDto.SelectedDevice != null && visitorDto.SelectedDevice.Count > 0)
            {
                foreach (var selectedDevice in visitorDto.SelectedDevice)
                {
                    var addDeviceDto = new AddVisitorDeviceDTO
                    {
                        VisitorId = visitor.VisitorId,
                        DeviceId = selectedDevice.DeviceId,
                        SerialNumber = selectedDevice.SerialNumber,
                    };

                    await AddVisitorDeviceAsync(addDeviceDto);
                }
            }
            
            return visitor;
           
        }


        public async Task<IEnumerable<string>> GetPersonInContactAsync()
        {
            return await _context.Visitors.Select(v => v.HostName).Distinct().ToListAsync();
        }

        public async Task<Visitor> GetVisitorByIdAsync(int id)
        {
            return await _context.Visitors.FirstOrDefaultAsync(v => v.VisitorId == id);
        }

        public async Task<IEnumerable<Visitor>> GetVisitorDetailsAsync()
        {
            return await _context.Visitors.ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        // Method to call the AddVisitorDevice API
       /* private VisitorDevice AddVisitorDevice(AddVisitorDeviceDto addDeviceDto)
        {
            // Create VisitorDevice entity from AddVisitorDeviceDto
            var visitorDevice = new VisitorDevice
            {
                VisitorId = addDeviceDto.VisitorId,
                DeviceId = addDeviceDto.DeviceId,
                SerialNumber = addDeviceDto.SerialNumber
            };

            // Add VisitorDevice to context
            *//* _context.VisitorDevices.Add(visitorDevice);*//*

            // Save changes to the VisitorDevices
            *//* _context.SaveChanges();*//*

            return visitorDevice;
        }*/
    }
}
