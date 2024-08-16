using Microsoft.EntityFrameworkCore;
using VMS.Data;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository.IRepository;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;


namespace VMS.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly VisitorManagementDbContext _context;
        private readonly ILogger<UserRepository> _logger;
        private readonly PasswordHasher<object> _passwordHasher = new PasswordHasher<object>();
        public UserRepository(VisitorManagementDbContext context, ILogger<UserRepository> logger) { 
            _context = context;
            _logger = logger;
        }

        public async Task AddUserAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation("User {Username} added successfully", user.Username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding user {Username}", user.Username);
                throw;
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            _logger.LogInformation("Getting all users.");
            try
            {
                var users = await _context.Users.ToListAsync();
                _logger.LogInformation("Retrieved {Count} users.", users.Count);
                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all users.");
                throw;
            }
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            _logger.LogInformation("Getting user by ID: {UserId}.", userId);
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    _logger.LogWarning("User with ID {UserId} not found.", userId);
                }
                else
                {
                    _logger.LogInformation("User with ID {UserId} retrieved.", userId);
                }
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting user by ID: {UserId}.", userId);
                throw;
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            _logger.LogInformation("Getting user by username: {Username}.", username);
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
                if (user == null)
                {
                    _logger.LogWarning("User with username {Username} not found.", username);
                }
                else
                {
                    _logger.LogInformation("User with username {Username} retrieved.", username);
                }
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting user by username: {Username}.", username);
                throw;
            }
        }

        public async Task UpdateLoggedInStatusAsync(string username) {

            await _context.SaveChangesAsync();
            _logger.LogInformation("Updating logged-in status for user: {Username}.", username);
            try
            {
                var user = await GetUserByUsernameAsync(username);
                if (user == null)
                {
                    _logger.LogWarning("User with username {Username} not found.", username);
                    return;
                }

                if (user.IsLoggedIn == 0)
                {
                    user.IsLoggedIn = 1;
                    _logger.LogInformation("User {Username} status set to logged in.", username);
                }
                else if (user.IsLoggedIn == 1)
                {
                    user.IsLoggedIn = 0;
                    _logger.LogInformation("User {Username} status set to logged out.", username);
                }
                await _context.SaveChangesAsync();

                // I need to add the hub
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating logged-in status for user: {Username}.", username);
                throw;
            }

        }
        public async Task UpdateUserAsync(User user)
        {
            _logger.LogInformation("Updating user with ID: {UserId}.", user.Id);
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation("User with ID {UserId} updated successfully.", user.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user with ID: {UserId}.", user.Id);
                throw;
            }
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            _logger.LogInformation("Validating user with username: {Username}.", username);
            try
            {
                var user = await GetUserByUsernameAsync(username);
                if (user == null)
                {
                    _logger.LogWarning("User with username {Username} not found.", username);
                    return false;
                }
                bool isValid = ValidatePassword(user.Password, password);
                _logger.LogInformation("User with username {Username} validation result: {IsValid}.", username, isValid);
                return isValid;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while validating user with username: {Username}.", username);
                throw;
            }


        }
        public bool ValidatePassword(string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }

        public async Task<LocationIdAndNameDTO> GetUserLocationAsync(int id)
        {
            _logger.LogInformation("Getting location for user with ID: {UserId}.", id);
           /* try
            {*/
                var userLocation = await (from user in _context.UserDetails
                                          join location in _context.OfficeLocations on user.OfficeLocationId equals location.Id
                                          where user.UserId == id
                                          select new LocationIdAndNameDTO
                                          {
                                              Id = location.Id,
                                              Name = location.Name
                                          }).FirstOrDefaultAsync();
                if (userLocation == null)
                {
                    _logger.LogWarning("Location for user with ID {UserId} not found.", id);
                }
                else
                {
                    _logger.LogInformation("Location for user with ID {UserId} retrieved successfully.", id);
                }
                return userLocation;
           /* }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting location for user with ID: {UserId}.", id);
                throw;
            }*/

        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            _logger.LogInformation("Checking if username exists: {Username}.", username);
            bool exists = await _context.Users.AnyAsync(u => u.Username == username);
            _logger.LogInformation("Username {Username} exists: {Exists}.", username, exists);
            return exists;
        }
    }
}
