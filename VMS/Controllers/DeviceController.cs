using Microsoft.AspNetCore.Mvc;
using System.Linq;

using VMS.Models;
using VMS.Data;

namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DeviceController : ControllerBase
    {
        private VisitorManagementDbContext _context;
        public DeviceController(VisitorManagementDbContext _context)
        {
            this._context = _context;

        }

        [HttpGet]
        public IEnumerable<Device> GetItems()
        {
            return _context.Devices.ToList<Device>();

        }
    }
}
