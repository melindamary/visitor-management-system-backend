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
            /*if (_context.PurposeOfVisits.Any(p => p.Name == purposeDto.purposeName))
            {
                throw new InvalidOperationException("Purpose already exists");
            }*/

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

        public async Task<IEnumerable<PurposeOfVisitNameadnIdDTO>> GetPurposesAsync()
        {
            return await _context.PurposeOfVisits
       .Where(p => p.Status == 1) // Filter by status
       .Select(p => new PurposeOfVisitNameadnIdDTO
       {
           PurposeId = p.Id,
           PurposeName = p.Name
       })
       .ToListAsync();
        }

        public async Task<IEnumerable<PurposeOfVisitDTO>> GetPurposeListAsync()
        {

            var purposeList = await (from purpose in _context.PurposeOfVisits
                                     join user in _context.UserDetails
                                     on purpose.UpdatedBy equals user.UserId into userGroup
                                     from user in userGroup.DefaultIfEmpty()
                                     where purpose.Status == 0 || purpose.Status == 1
                                     select new PurposeOfVisitDTO
                                     {
                                         Id = purpose.Id,
                                         Name = purpose.Name,
                                         Status = purpose.Status,
                                         CreatedBy = purpose.CreatedBy,
                                         UpdatedBy = user != null ? user.FirstName + " " + user.LastName : null,
                                         CreatedDate = purpose.CreatedDate,
                                         UpdatedDate = purpose.UpdatedDate

                                     }).ToListAsync();

            return purposeList;
        }

        public async Task<bool> UpdatePurposeAsync(PurposeUpdateRequestDTO updatePurposeRequestDTO)
        {
            var purpose = await _context.PurposeOfVisits.FindAsync(updatePurposeRequestDTO.Id);
            if (purpose == null)
            {
                return false;
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == updatePurposeRequestDTO.Username);
            Console.WriteLine(updatePurposeRequestDTO.Username);
            purpose.Name = updatePurposeRequestDTO.Purpose;
            purpose.UpdatedBy = user.Id;
            purpose.UpdatedDate = DateTime.Now;
            purpose.Status = 1;

            _context.PurposeOfVisits.Update(purpose);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdatePurposeStatusAsync(PurposeStatusUpdateRequestDTO updatePurposeStatusRequestDTO)
        {
            var purpose = await _context.PurposeOfVisits.FindAsync(updatePurposeStatusRequestDTO.Id);
            if (purpose == null)
            {
                return false;
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == updatePurposeStatusRequestDTO.Username);
            Console.WriteLine(updatePurposeStatusRequestDTO.Username);
            purpose.UpdatedBy = user.Id;
            purpose.UpdatedDate = DateTime.Now;
            purpose.Status = updatePurposeStatusRequestDTO.Status;

            _context.PurposeOfVisits.Update(purpose);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletePurposeAsync(int id)
        {
            var purpose = await _context.PurposeOfVisits.FirstOrDefaultAsync(u => u.Id == id);
            if (purpose == null)
            {
                return false;
            }

            purpose.Status = 2; //status = 2 implies soft delete
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
