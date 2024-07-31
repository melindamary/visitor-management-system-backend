using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using VMS.Models;
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
        private readonly IUserDetailsRepository _userDetailRepository;
        private readonly IUserLocationRepository _userLocationRepository;
        private readonly ILocationRepository _locationRepository;

        public const int _activeStatus = 1;
        public const int _isLoggedIn = 0;



        public UserService(IUserRepository userRepository, IUserLocationRepository userLocationRepository,
            IUserDetailsRepository userDetailRepository,ILocationRepository locationRepository,
            IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _userDetailRepository = userDetailRepository;  
            _userLocationRepository = userLocationRepository;
            _locationRepository = locationRepository;
        }

      

        public async Task<ActionResult<UserRoleDTO>> GetUserRoleByUsernameAsync(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username); //this is working

            if (user == null)
            {
                return null; // User not found
            }

            var userRole = await _userRoleRepository.GetUserRoleByUserIdAsync(user.Id); //returns UserRole

            if (userRole == null)
            {
                return null; // Role not found for the user
            }

            var role = await _roleRepository.GetRoleByIdAsync(userRole.RoleId);

            return new UserRoleDTO
            {
                UserId = user.Id,
                Username = user.Username,
                RoleId = userRole.RoleId,
                RoleName = role?.Name ?? "Role does not exist for user" // Handle cases where role might not be found
            };
        }

        public async Task AddUserAsync(AddNewUserDTO addNewUserDto)
        {
            var passwordHasher = new PasswordHasher<User>();

            // Create the user object
            var user = new User
            {
                Username = addNewUserDto.UserName,
                CreatedDate = DateTime.Now,
                IsActive = _activeStatus,
                IsLoggedIn = _isLoggedIn,
                ValidFrom = addNewUserDto.ValidFrom
            };

            // Hash the password and set it
            /*user.Password = passwordHasher.HashPassword(user, addNewUserDto.Password);*/
            user.Password = HashPassword(addNewUserDto.Password);


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

        public static string HashPassword(string password)
        {
            // Generate a random salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            // Create the Rfc2898DeriveBytes and get the hash value
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Combine the salt and password bytes for later use
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Turn the combined salt+hash into a string for storage
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
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
            var locations = await _locationRepository.GetLocationByIdAsync(userLocation.OfficeLocationId);

            return new UserDetailDTO
            {
                UserId = user.Id,
                Username = user.Username,
                FirstName = userDetail.FirstName,
                LastName = userDetail.LastName,
                Phone = userDetail.Phone,
                Address = userDetail.Address,
                RoleName = role?.Name ?? "Unknown",
                RoleId = role.Id,
                OfficeLocation = locations.Name,     
                OfficeLocationId = locations.Id,
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
            var locations = await _locationRepository.GetAllLocationAsync();
            var userLocations = await _userLocationRepository.GetAllUserLocationsAsync();


            if (users == null || userDetails == null || userRoles == null || roles == null || locations == null || userLocations == null)
            {
                throw new InvalidOperationException("One or more data sources returned null.");
            }

            var userOverviews = from user in users
                                join detail in userDetails on user.Id equals detail.UserId
                                join userRole in userRoles on user.Id equals userRole.UserId
                                join role in roles on userRole.RoleId equals role.Id
                                join userLocation in userLocations on user.Id equals userLocation.UserId
                                join location in locations on userLocation.OfficeLocationId equals location.Id
                                where userLocation.OfficeLocationId != null
                                select new UserOverviewDTO
                                {
                                    userId = user.Id,
                                    Username = user.Username,
                                    RoleName = role.Name,
                                    Location = location.Name ?? "Unknown", // Assuming 'Name' is the property that contains the location name
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
            // Only update the password if a new password is provided
            if (!string.IsNullOrEmpty(updateUserDto.Password))
            {
                var passwordHasher = new PasswordHasher<User>();
                user.Password = passwordHasher.HashPassword(user, updateUserDto.Password);
            }
            user.ValidFrom = updateUserDto.ValidFrom;
            user.IsActive = updateUserDto.IsActive;

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

        public async Task<bool> CheckUsernameExistsAsync(string username)
        {

            return await _userRepository.UsernameExistsAsync(username);

        }
    }
}