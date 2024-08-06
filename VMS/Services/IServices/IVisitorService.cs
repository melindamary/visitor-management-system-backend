using VMS.Models.DTO;

namespace VMS.Services.IServices
{
    public interface IVisitorService
    {
        Task<int> GetActiveVisitorsCountToday();
        Task<int> GetTotalVisitorsCountToday();
        Task<int> GetCheckedOutVisitorsCountToday();
        Task<IEnumerable<VisitorLogDTO>> GetVisitorDetailsToday();
        Task<IEnumerable<VisitorLogDTO>> GetUpcomingVisitorsToday();
        Task<IEnumerable<VisitorLogDTO>> GetActiveVisitorsToday();
        Task<IEnumerable<VisitorLogDTO>> GetCheckedOutVisitorsToday();
        Task<VisitorLogDTO> UpdateCheckInTimeAndCardNumber(int id, UpdateVisitorPassCodeDTO updateVisitorPassCode);
        Task<VisitorLogDTO> UpdateCheckOutTime(int id);

    }
}
