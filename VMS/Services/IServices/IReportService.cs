using Microsoft.AspNetCore.Mvc;
using VMS.Models.DTO;
using VMS.Repository.IRepository;

namespace VMS.Services.IServices
{
    public interface IReportService
    {
        Task<IEnumerable<VisitorReportDetailsDTO>> GetAllVisitorReportsAsync();
        Task<VisitorDetailsDTO> GetVisitorDetailsAsync(int id);
    }
}
