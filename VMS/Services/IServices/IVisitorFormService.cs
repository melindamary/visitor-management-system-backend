using System.Collections.Generic;
using System.Threading.Tasks;
using VMS.Models;
using VMS.Models.DTO;

namespace VMS.Services
{
    public interface IVisitorFormService
    {
        Task<IEnumerable<Visitor>> GetVisitorDetailsAsync();
        Task<IEnumerable<string>> GetPersonInContactAsync();
        Task<Visitor> GetVisitorByIdAsync(int id);
        Task<Visitor> CreateVisitorAsync(VisitorCreationDTO visitorDto);
        Task<VisitorDevice> AddVisitorDeviceAsync(AddVisitorDeviceDTO addDeviceDto);
    }
}