using VMS.Models.DTO;

namespace VMS.Repository.IRepository
{
    public interface IPurposeOfVisitRepository
    {
        Task<IEnumerable<PurposeOfVisitNameadnIdDto>> GetPurposesAsync();
        Task<PurposeOfVisit> AddPurposeAsync(AddNewPurposeDTO purposeDto);
        Task SaveAsync();
    }
}
