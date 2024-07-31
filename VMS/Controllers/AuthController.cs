using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository.IRepository;
using VMS.Services.IServices;

namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IUserService _service;

        public AuthController(IUserRepository repository, IUserService service, IConfiguration configuration)
        {
            _repository = repository;
            _service = service;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            if (!await _repository.ValidateUserAsync(loginRequest.Username, loginRequest.Password))
            {
                var errorResponse = new APIResponse
                {
                    Result = null,
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.Unauthorized,
                    ErrorMessages = new List<string> { "Invalid login credentials" }
                };
                return Unauthorized(errorResponse);
            }

            APIResponse response = new APIResponse();

            var user = await _repository.GetUserByUsernameAsync(loginRequest.Username);
            var location = await _repository.GetUserLocationAsync(user.Id);
            if (location == null) {
                response.ErrorMessages.Add("Location not found for user");
            }

            var userRole = await _service.GetUserRoleByUsernameAsync(user.Username);
            Console.WriteLine(userRole.Value.RoleName);

            if (userRole == null) {
                response.ErrorMessages.Add("Role not found for user");
            }

            await _repository.UpdateLoggedInStatusAsync(user.Username);

            var token = GenerateJwtToken(user, userRole.Value.RoleName);

            var loginResponse = new LoginResponseDTO
            {
                Username = user.Username,
                Token = token,
                Location = location.Name,
                Role = userRole.Value.RoleName
            };

            
            response.Result = loginResponse;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.ErrorMessages = null;
            return Ok(response);

        }

        private string GenerateJwtToken(User user, string userRole)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, userRole)
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["ApiSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        
    }
}
