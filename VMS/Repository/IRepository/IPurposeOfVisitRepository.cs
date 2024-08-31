using VMS.Models;
using VMS.Models.DTO;

namespace VMS.Repository.IRepository
{
    public interface IPurposeOfVisitRepository
    {
        Task<IEnumerable<PurposeOfVisitNameadnIdDTO>> GetPurposesAsync();
        Task<PurposeOfVisit> AddPurposeAsync(AddNewPurposeDTO purposeDto);

        Task<IEnumerable<PurposeOfVisitDTO>> GetPurposeListAsync();

        Task<bool> UpdatePurposeAsync(PurposeUpdateRequestDTO updatePurposeRequestDTO);
        Task<bool> UpdatePurposeStatusAsync(PurposeStatusUpdateRequestDTO updatePurposeStatusRequestDTO);

        Task<bool> DeletePurposeAsync(int id);
        Task SaveAsync();
    }
}
