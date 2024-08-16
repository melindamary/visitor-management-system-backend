using VMS.Models.DTO;
using VMS.Models;

namespace VMS.Services.IServices
{
    public interface IAuthService
    {
        Task<APIResponse> AuthenticateUser(LoginRequestDTO loginRequest);
        string GenerateJwtToken(User user, string userRole);

        Task<APIResponse> LogoutUser(LogoutRequestDTO loginRequest);
    }
}
