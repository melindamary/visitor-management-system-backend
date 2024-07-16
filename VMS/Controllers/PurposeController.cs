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

        [HttpGet]
        public IEnumerable<PurposeOfVisit> GetPurposes()
        {
            return _context.PurposeOfVisits.ToList<PurposeOfVisit>();

        }

        [HttpPost]
        public async Task<ActionResult<PurposeOfVisit>> PostPurpose(AddNewPurposeDTO purposeDto)
        {
            if (_context.PurposeOfVisits.Any(p => p.PurposeName == purposeDto.Name))
            {
                return Conflict(new { message = "Purpose already exists" });
            }

            var purpose = new PurposeOfVisit
            {
                PurposeName = purposeDto.Name,
                CreatedBy = purposeDto.CreatedBy,
                UpdatedBy = purposeDto.UpdatedBy,

                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _context.PurposeOfVisits.Add(purpose);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostPurpose), new { id = purpose.PurposeId }, purpose);
        }
    }
}
