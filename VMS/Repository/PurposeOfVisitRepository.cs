﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<PurposeOfVisitNameadnIdDTO>> GetPurposesAsync()
        {
            return await _context.PurposeOfVisits
                .Select(p => new PurposeOfVisitNameadnIdDTO
                {
                    PurposeId = p.Id,
                    PurposeName = p.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<PurposeOfVisit>> GetPurposeListAsync() {

            var purposeList = await (from purpose in _context.PurposeOfVisits
                                     select new PurposeOfVisit { 
                                      Id = purpose.Id,
                                      Name = purpose.Name,
                                      Status = purpose.Status,
                                      CreatedBy = purpose.CreatedBy,
                                      UpdatedBy = purpose.UpdatedBy,
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
            purpose.Name = updatePurposeRequestDTO.Purpose;
            purpose.UpdatedBy = updatePurposeRequestDTO.UserId;
            purpose.UpdatedDate = DateTime.Now;
            purpose.Status = 1;

            _context.PurposeOfVisits.Update(purpose);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
