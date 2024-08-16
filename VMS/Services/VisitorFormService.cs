using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository.IRepository;

namespace VMS.Services
{
    public class VisitorFormService : IVisitorFormService
    {
        private readonly IVisitorFormRepository _repository;
        private const int _systemUserId = 1;
        private const int _defaultPassCode = 0;
        private const string _submissionType = "Kiosk";

        public VisitorFormService(IVisitorFormRepository repository)
        {
            _repository = repository;
        }

        public async Task<Visitor> CreateVisitorAsync(VisitorCreationDTO visitorDto)
        {
            if (visitorDto == null)
            {
                throw new ArgumentNullException(nameof(visitorDto));
            }

            // Delegate visitor creation to the repository
            var createdVisitor = await _repository.CreateVisitorAsync(visitorDto);

            // If there are devices associated, add them
            if (visitorDto.SelectedDevice != null && visitorDto.SelectedDevice.Count > 0)
            {
                foreach (var selectedDevice in visitorDto.SelectedDevice)
                {
                    var addDeviceDto = new AddVisitorDeviceDTO
                    {
                        VisitorId = createdVisitor.Id,
                        DeviceId = selectedDevice.DeviceId,
                        SerialNumber = selectedDevice.SerialNumber,
                    };

                    await _repository.AddVisitorDeviceAsync(addDeviceDto);
                }
            }

            return createdVisitor;
        }

        public async Task<VisitorDevice> AddVisitorDeviceAsync(AddVisitorDeviceDTO addDeviceDto)
        {
            return await _repository.AddVisitorDeviceAsync(addDeviceDto);
        }

        public async Task<IEnumerable<string>> GetPersonInContactAsync()
        {
            return await _repository.GetPersonInContactAsync();
        }

        public async Task<Visitor> GetVisitorByIdAsync(int id)
        {
            return await _repository.GetVisitorByIdAsync(id);
        }

        public async Task<IEnumerable<Visitor>> GetVisitorDetailsAsync()
        {
            return await _repository.GetVisitorDetailsAsync();
        }
    }
}
