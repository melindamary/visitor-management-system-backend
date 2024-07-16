using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using VMS.Models.DTO;

namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    
    public class AuthController : ControllerBase
    {
        private static readonly List<User> UserList = new List<User>
        {
            new User { Username = "admin", Password = "admin123"},
            new User { Username = "user", Password = "user123"},
            new User { Username = "auditor", Password = "auditor123"}
        };

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = UserList.FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            // Generate and return a JWT token here
            return Ok(new User {Username = user.Username});
        }
    }
}
