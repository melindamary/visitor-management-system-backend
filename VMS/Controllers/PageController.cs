using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VMS.Models;
using VMS.Data;
using VMS.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PageController : ControllerBase
    {
        private readonly VisitorManagementDbContext _context;

        public PageController(VisitorManagementDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Page>> GetPages()
        {
            return _context.Pages.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Page> GetPage(int id)
        {
            var page = _context.Pages.Find(id);

            if (page == null)
            {
                return NotFound();
            }

            return page;
        }

        [HttpPost]
        public ActionResult<Page> CreatePage(PageDTO pageDto)
        {
            var page = new Page
            {
                PageName = pageDto.PageName,
                PageUrl = pageDto.PageUrl,
                CreatedBy = pageDto.CreatedBy,
                UpdatedBy = pageDto.UpdatedBy,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _context.Pages.Add(page);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetPage), new { id = page.PageId }, page);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePage(int id, PageDTO pageDto)
        {
            var page = _context.Pages.Find(id);

            if (page == null)
            {
                return NotFound();
            }

            page.PageName = pageDto.PageName;
            page.PageUrl = pageDto.PageUrl;
            page.UpdatedBy = pageDto.UpdatedBy;
            page.UpdatedDate = DateTime.Now;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePage(int id)
        {
            var page = _context.Pages.Find(id);

            if (page == null)
            {
                return NotFound();
            }

            _context.Pages.Remove(page);
            _context.SaveChanges();

            return NoContent();
        }
    }
}