using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VMS.Data;
using VMS.Models;
using VMS.Models.DTO;

namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PurposeController : ControllerBase
    {
        private VisitorManagementDbContext _context;
        public PurposeController(VisitorManagementDbContext _context)
        {
            this._context = _context;

        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("get-purposes-idAndName")]
        public IEnumerable<PurposeOfVisitNameadnIdDto> GetPurposes()
        {
            return _context.PurposeOfVisits
                   .Select(p => new PurposeOfVisitNameadnIdDto
                   {
                       PurposeId = p.PurposeId,
                       PurposeName = p.PurposeName
                   })
                   .ToList();

        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult<PurposeOfVisit>> PostPurpose(AddNewPurposeDTO purposeDto)
        {
            if (_context.PurposeOfVisits.Any(p => p.PurposeName == purposeDto.purposeName))
            {
                return Conflict(new { message = "Purpose already exists" });
            }

            var purpose = new PurposeOfVisit
            {
                PurposeName = purposeDto.purposeName,
                CreatedBy = 1,
                UpdatedBy = 1,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _context.PurposeOfVisits.Add(purpose);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostPurpose), new { id = purpose.PurposeId }, purpose);
        }
    }
}
