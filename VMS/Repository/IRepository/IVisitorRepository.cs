using VMS.Models;
using VMS.Models.DTO;

namespace VMS.Repository.IRepository
{
    public interface IVisitorRepository
    {
        Task<Visitor> GetVisitorByIdAsync(int id);
        Task<int> GetVisitorCount(Func<IQueryable<Visitor>, IQueryable<Visitor>> filter);
        Task<IEnumerable<VisitorLogDTO>> GetVisitorLogs(Func<IQueryable<Visitor>, IQueryable<Visitor>> filter,string locationName);
        Task<VisitorLogDTO> UpdateCheckInTimeAndCardNumber(int id, UpdateVisitorPassCodeDTO updateVisitorPassCode);
        Task<VisitorLogDTO> UpdateCheckOutTime(int id);
        Task SaveAsync();


    }
}
