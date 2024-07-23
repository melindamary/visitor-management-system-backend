using VMS.Models;
using VMS.Models.DTO;

namespace VMS.Services.IServices
{
    public interface IReportService
    {
        Task<IEnumerable<VisitorReportDetailsDTO>> GetAllVisitorsAsync();
    }
}
