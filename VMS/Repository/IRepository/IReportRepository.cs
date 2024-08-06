using VMS.Models.DTO;

namespace VMS.Repository.IRepository
{
    public interface IReportRepository
    {
        Task<IEnumerable<VisitorReportDetailsDTO>> GetAllVisitorsAsync();

        Task<VisitorDetailsDTO> GetVisitorDetailsAsync(int id);
    }
}
