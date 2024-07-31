using Microsoft.AspNetCore.Mvc;
using VMS.Models.DTO;

namespace VMS.Services.IServices
{
    public interface IUserService
    {
     Task<ActionResult<UserRoleDTO>> GetUserRoleByUsernameAsync(string username);
     Task AddUserAsync(AddNewUserDTO addNewUserDto);
     Task<UserDetailDTO> GetUserByIdAsync(int userId);
     Task<List<UserOverviewDTO>> GetAllUsersOverviewAsync();
     Task<bool> UpdateUserAsync(UpdateUserDTO updateUserDto);

    }
}
