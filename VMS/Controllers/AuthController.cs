using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using VMS.Models;

namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    
    public class AuthController : ControllerBase
    {
        private static readonly List<User> UserList = new List<User>
        {
            new User { Username = "admin", Password = "admin123", RoleId =1 },
            new User { Username = "user", Password = "user123", RoleId =2 },
            new User { Username = "auditor", Password = "auditor123", RoleId =3 }
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
            return Ok(new User {Username = user.Username, RoleId = user.RoleId });
        }
    }
}
