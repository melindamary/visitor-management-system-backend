using Microsoft.EntityFrameworkCore;
using VMS.Data;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository.IRepository;

namespace VMS.Repository
{
    public class PurposeOfVisitRepository : IPurposeOfVisitRepository
    {
        private readonly VisitorManagementDbContext _context;

        public PurposeOfVisitRepository(VisitorManagementDbContext context)
        {
            _context = context;
        }


        public async Task<PurposeOfVisit> AddPurposeAsync(AddNewPurposeDTO purposeDto)
        {
            if (_context.PurposeOfVisits.Any(p => p.Name == purposeDto.purposeName))
            {
                throw new InvalidOperationException("Purpose already exists");
            }

            var purpose = new PurposeOfVisit
            {
                Name = purposeDto.purposeName,
                CreatedBy = 1,
                UpdatedBy = 1,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _context.PurposeOfVisits.Add(purpose);
            await _context.SaveChangesAsync();

            return purpose;
        }

        public async Task<IEnumerable<PurposeOfVisitNameadnIdDto>> GetPurposesAsync()
        {
            return await _context.PurposeOfVisits
                .Select(p => new PurposeOfVisitNameadnIdDto
                {
                    PurposeId = p.Id,
                    PurposeName = p.Name
                })
                .ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
