using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using VMS.Models.DTO;
using VMS.Models;
using VMS.Repository.IRepository;
using VMS.Services.IServices;

namespace VMS.Services
{

    public class AuthService: IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IUserService _service;

        public AuthService(IUserRepository repository, IUserService service, IConfiguration configuration)
        {
            _repository = repository;
            _service = service;
            _configuration = configuration;
        }

        public async Task<APIResponse> AuthenticateUser(LoginRequestDTO loginRequest)
        {
            var result = new LoginResponseDTO();
            var response = new APIResponse();

            if (!await _repository.ValidateUserAsync(loginRequest.Username, loginRequest.Password))
            {
                response.ErrorMessages = new List<string> { "Invalid login credentials" };
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }

            var user = await _repository.GetUserByUsernameAsync(loginRequest.Username);
            var location = await _repository.GetUserLocationAsync(user.Id);
            if (location == null)
            {
                response.ErrorMessages = new List<string> { "Location not found for user" };
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            var userRole = await _service.GetUserRoleByUsernameAsync(user.Username);
            if (userRole == null)
            {
                response.ErrorMessages = new List<string> { "Role not found for user" };
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            await _repository.UpdateLoggedInStatusAsync(user.Username);

            var token = GenerateJwtToken(user, userRole.Value.RoleName);

            result.Username = user.Username;
            result.Token = token;
            result.Location = location.Name;
            result.Role = userRole.Value.RoleName;
            
            response.Result = result;
            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            return response;
        }

        public string GenerateJwtToken(User user, string userRole)
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





  