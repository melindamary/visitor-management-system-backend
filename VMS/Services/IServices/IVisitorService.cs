using VMS.Models.DTO;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace VMS.Services.IServices
{
    public interface IVisitorService
    {
        Task<int> GetActiveVisitorsCountToday(string locationName);
        Task<int> GetTotalVisitorsCountToday(string locationName);
        Task<int> GetCheckedOutVisitorsCountToday(string locationName);
        Task<IEnumerable<VisitorLogDTO>> GetVisitorDetailsToday(string locationName);
        Task<IEnumerable<VisitorLogDTO>> GetUpcomingVisitorsToday(string locationName);
        Task<IEnumerable<VisitorLogDTO>> GetActiveVisitorsToday(string locationName);
        Task<IEnumerable<VisitorLogDTO>> GetCheckedOutVisitorsToday(string locationName);
        Task<IEnumerable<VisitorLogDTO>> GetScheduledVisitors(string locationName);
        Task<VisitorLogDTO> UpdateCheckInTimeAndCardNumber(int id, UpdateVisitorPassCodeDTO updateVisitorPassCode);
        Task<VisitorLogDTO> UpdateCheckOutTime(int id);

    }
}
