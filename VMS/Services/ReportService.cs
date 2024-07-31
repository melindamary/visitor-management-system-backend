using Microsoft.AspNetCore.Mvc;
using VMS.Models.DTO;
using VMS.Repository.IRepository;
using VMS.Services.IServices;

namespace VMS.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _repository;

        public ReportService(IReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<VisitorReportDetailsDTO>> GetAllVisitorReportsAsync()
        {
            return await _repository.GetAllVisitorsAsync();
        }

        public async Task<VisitorDetailsDTO> GetVisitorDetailsAsync(int id)
        {
            return await _repository.GetVisitorDetailsAsync(id);
        }
    }
}
