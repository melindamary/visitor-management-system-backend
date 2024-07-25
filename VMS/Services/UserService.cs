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

        public const int _activeStatus = 1;
        public const int _isLoggeedIn = 0;



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
                CreatedDate = DateTime.Now,
                IsActive = _activeStatus,
                IsLoggedIn = _isLoggeedIn,
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
                CreatedDate = DateTime.Now
            };
            Console.WriteLine(userDetail.UserId);
            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = addNewUserDto.RoleId,
                CreatedDate = DateTime.Now
            };

            var userLocation = new UserLocation
            {
                UserId = user.Id,
                OfficeLocationId = addNewUserDto.OfficeLocationId,
                CreatedDate = DateTime.Now
            };

            await _userDetailRepository.AddUserDetailAsync(userDetail);
            await _userRoleRepository.AddUserRoleAsync(userRole);
            await _userLocationRepository.AddUserLocationAsync(userLocation);
        }

        public async Task<UserDetailDTO> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                return null; // User not found
            }

            var userDetail = await _userDetailRepository.GetUserDetailByUserIdAsync(userId);
            var userRole = await _userRoleRepository.GetUserRoleByUserIdAsync(userId);
            var role = await _roleRepository.GetRoleByIdAsync(userRole.RoleId);
            var userLocation = await _userLocationRepository.GetUserLocationByUserIdAsync(userId);

            return new UserDetailDTO
            {
                UserId = user.Id,
                Username = user.Username,
                FirstName = userDetail.FirstName,
                LastName = userDetail.LastName,
                Phone = userDetail.Phone,
                Address = userDetail.Address,
                RoleName = role?.Name ?? "Unknown",
                OfficeLocationId = userLocation.OfficeLocationId,               
                IsActive = user.IsActive,
                ValidFrom = user.ValidFrom
            };
        }

        public async Task<List<UserOverviewDTO>> GetAllUsersOverviewAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            var userDetails = await _userDetailRepository.GetAllUserDetailsAsync();
            var userRoles = await _userRoleRepository.GetAllUserRolesAsync();
            var roles = await _roleRepository.GetAllRolesAsync();
            var userLocations = await _userLocationRepository.GetAllUserLocationsAsync();

            var userOverviews = from user in users
                                join detail in userDetails on user.Id equals detail.UserId
                                join userRole in userRoles on user.Id equals userRole.UserId
                                join role in roles on userRole.RoleId equals role.Id
                                join location in userLocations on user.Id equals location.UserId
                                select new UserOverviewDTO
                                {
                                    Username = user.Username,
                                    RoleName = role.Name,
                                    Location = location.OfficeLocationId.ToString(), // Assuming OfficeLocationId represents location
                                    FullName = $"{detail.FirstName} {detail.LastName}",
                                    IsActive = user.IsActive == 1
                                };

            return userOverviews.ToList();
        }
        public async Task<bool> UpdateUserAsync(UpdateUserDTO updateUserDto)
        {
            var user = await _userRepository.GetUserByIdAsync(updateUserDto.UserId);
            if (user == null) return false;

            user.Username = updateUserDto.Username;
            user.Password = updateUserDto.Password;
            user.ValidFrom = updateUserDto.ValidFrom;

            await _userRepository.UpdateUserAsync(user);

            var userDetail = await _userDetailRepository.GetUserDetailByUserIdAsync(user.Id);
            if (userDetail != null)
            {
                userDetail.FirstName = updateUserDto.FirstName;
                userDetail.LastName = updateUserDto.LastName;
                userDetail.Phone = updateUserDto.Phone;
                userDetail.Address = updateUserDto.Address;
                userDetail.OfficeLocationId = updateUserDto.OfficeLocationId;

                await _userDetailRepository.UpdateUserDetailAsync(userDetail);
            }

            var userRole = await _userRoleRepository.GetUserRoleByUserIdAsync(user.Id);
            if (userRole != null)
            {
                userRole.RoleId = updateUserDto.RoleId;
                await _userRoleRepository.UpdateUserRoleAsync(userRole);
            }

            var userLocation = await _userLocationRepository.GetUserLocationByUserIdAsync(user.Id);
            if (userLocation != null)
            {
                userLocation.OfficeLocationId = updateUserDto.OfficeLocationId;
                await _userLocationRepository.UpdateUserLocationAsync(userLocation);
            }

            return true;
        }
    }
}