using VMS.Models;
using VMS.Models.DTO;

namespace VMS.Repository.IRepository
{
    public interface IVisitorFormRepository
    {
        Task<IEnumerable<Visitor>> GetVisitorDetailsAsync();
        Task<IEnumerable<string>> GetPersonInContactAsync();
        Task<Visitor> GetVisitorByIdAsync(int id);
        Task<Visitor> CreateVisitorAsync(VisitorCreationDTO visitorDto);
        Task<VisitorDevice> AddVisitorDeviceAsync(AddVisitorDeviceDto addDeviceDto);
        Task SaveAsync();
    }
}
