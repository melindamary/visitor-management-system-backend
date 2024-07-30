using Microsoft.AspNetCore.Mvc;
using VMS.Models.DTO;
using VMS.Services.IServices;

namespace VMS.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]

    public class AuthController : ControllerBase
    {
       private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var loginResponse = await _authService.AuthenticateUser(loginRequest);

            return Ok(loginResponse);

        }
    }
}
