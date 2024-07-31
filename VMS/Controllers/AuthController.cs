using Microsoft.AspNetCore.Mvc;
using VMS.Models;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var loginResponse = await _authService.AuthenticateUser(loginRequest);

            return Ok(loginResponse);

        }
    }
}
