using VMS.Models.DTO;

namespace VMS.Repository.IRepository
{
    public interface IVisitorRepository
    {
        Task<int> GetActiveVisitorsCountToday();
        Task<int> GetTotalVisitorsCountToday();
        Task<int> GetCheckedOutVisitorsCountToday();
        Task<IEnumerable<VisitorLogDTO>> GetUpcomingVisitorsToday();
        Task<IEnumerable<VisitorLogDTO>> GetActiveVisitorsToday();
        Task<VisitorLogDTO> UpdateCheckInTimeAndCardNumber(int id, UpdateVisitorPassCodeDTO updateVisitorPassCode);
        Task<VisitorLogDTO> UpdateCheckOutTime(int id);
    }
}
