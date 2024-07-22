using Microsoft.AspNetCore.Mvc;
using VMS.Models.DTO;

namespace VMS.Services.IServices
{
    public interface IUserService
    {
     Task<ActionResult<UserRoleDTO>> GetUserRoleByUsername(string username);
    }
}
