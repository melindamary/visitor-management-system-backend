using Microsoft.AspNetCore.Mvc;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository;
using VMS.Repository.IRepository;
using VMS.Services.IServices;

namespace VMS.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserDetailsRepository _userDetailRepository;
        private readonly IUserLocationRepository _userLocationRepository;

        public UserService(IUserRepository userRepository, IUserLocationRepository userLocationRepository,
            IUserDetailsRepository userDetailRepository,
            IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _userDetailRepository = userDetailRepository;  
            _userLocationRepository = userLocationRepository;
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

        public async Task AddUserAsync(AddNewUserDTO addNewUserDto)
        {
            var user = new User
            {
                Username = addNewUserDto.UserName,
                Password = addNewUserDto.Password,
                CreatedDate = DateTime.UtcNow,
                IsActive = 1,
                ValidFrom = addNewUserDto.ValidFrom
            };

            await _userRepository.AddUserAsync(user);
           

            var userDetail = new UserDetail
            {
                UserId = user.Id,
                FirstName = addNewUserDto.FirstName,
                LastName = addNewUserDto.LastName,
                Phone = addNewUserDto.Phone,
                Address = addNewUserDto.Address,
                OfficeLocationId = addNewUserDto.OfficeLocationId,
                CreatedDate = DateTime.UtcNow
            };
            Console.WriteLine(userDetail.UserId);
            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = addNewUserDto.RoleId,
                CreatedDate = DateTime.UtcNow
            };

            var userLocation = new UserLocation
            {
                UserId = user.Id,
                OfficeLocationId = addNewUserDto.OfficeLocationId,
                CreatedDate = DateTime.UtcNow
            };

            await _userDetailRepository.AddUserDetailAsync(userDetail);
            await _userRoleRepository.AddUserRoleAsync(userRole);
            await _userLocationRepository.AddUserLocationAsync(userLocation);
        }
    }
}