using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository.IRepository;

namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;

        public AuthController(IUserRepository repository, IConfiguration configuration)
        {
            _repository = repository;
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

            var user = await _repository.GetUserByUsernameAsync(loginRequest.Username);
            var token = GenerateJwtToken(user);

            var loginResponse = new LoginResponseDTO
            {
                Username = user.Username,
                Token = token
            };

            APIResponse response = new APIResponse();
            response.Result = loginResponse;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Ok(response);

        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["ApiSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        
    }
}
