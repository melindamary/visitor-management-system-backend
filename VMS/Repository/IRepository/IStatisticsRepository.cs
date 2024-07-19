// File: Repository/IRepository/IStatisticsRepository.cs
using VMS.Models.DTO;

namespace VMS.Repository.IRepository
{
    public interface IStatisticsRepository
    {
        Task<IEnumerable<LocationStatisticsDTO>> GetLocationStatistics();
        Task<IEnumerable<SecurityStatisticsDTO>> GetSecurityStatistics();

    }
}