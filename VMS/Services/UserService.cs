using Microsoft.AspNetCore.Mvc;
using VMS.Models.DTO;
using VMS.Repository.IRepository;
using VMS.Services.IServices;

namespace VMS.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }
        public async Task<ActionResult<UserRoleDTO>> GetUserRoleByUsername(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user == null)
            {
                return null; // User not found
            }

            var userRole = await _userRoleRepository.GetUserRoleByUserIdAsync(user.Id);

            if (userRole == null)
            {
                return null; // Role not found for the user
            }

            var role = await _roleRepository.GetRoleByIdAsync(userRole.RoleId);

            return new UserRoleDTO
            {
                UserId = user.Id,
                Username = user.Username,
                RoleName = role?.Name ?? "Unknown" // Handle cases where role might not be found
            };


        }
    }
}
