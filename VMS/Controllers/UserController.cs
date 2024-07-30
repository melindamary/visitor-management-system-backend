using Microsoft.AspNetCore.Mvc;
using System.Net;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Services.IServices;

namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;   
        }

        /*[Authorize(Policy = "AdminOnly")]*/
        [HttpGet("{username}")]
        public async Task<ActionResult<APIResponse>> GetUserRoleByUsername(string username) 
        {

            var userRole = await _userService.GetUserRoleByUsernameAsync(username);

            if (userRole == null)
            {
                return NotFound();
            }

            APIResponse response = new APIResponse();
            response.Result = userRole;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewUser([FromBody] AddNewUserDTO addNewUserDto)
        {
            /*try
            {*/
                await _userService.AddUserAsync(addNewUserDto);

                var successResponse = new APIResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    Result = "User created successfully"
                };

                return Ok(successResponse);
           /* }
            catch (Exception ex)
            {
                var errorResponse = new APIResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessages = new List<string> { ex.Message }
                };

                return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
            }*/
        }
        [HttpGet]
        public async Task<IActionResult> CheckUsernameExists([FromQuery] string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                var errorResponse = new APIResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string> { "Username is required" }
                };
                return BadRequest(errorResponse);
            }

            var exists = await _userService.CheckUsernameExistsAsync(username);

            var response = new APIResponse
            {
                Result = exists,
                StatusCode = HttpStatusCode.OK,
            };

            if (!exists)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMessages = new List<string> { "No Username found" };
                return NotFound(response);
            }

            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse>> GetUserById(int id)
        {
           
            var userDetail = await _userService.GetUserByIdAsync(id);
            if (userDetail == null)
            {
                var errorResponse = new APIResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new List<string> { "No User found" }
                };
                return NotFound(errorResponse);
            }
            var response = new APIResponse
            {
                Result = userDetail,
                StatusCode = HttpStatusCode.OK,
            };
            return Ok(userDetail);
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAllUsersOverview()
        {
            var userOverviews = await _userService.GetAllUsersOverviewAsync();
            if (userOverviews == null)
            {
                var errorResponse = new APIResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new List<string> { "No User found" }
                };
                return NotFound(errorResponse);
            }
            var response = new APIResponse
            {
                Result = userOverviews,
                StatusCode = HttpStatusCode.OK,
            };

            return Ok(userOverviews);
        }
       [HttpPut("{id}")]
public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO updateUserDto)
{
    if (id != updateUserDto.UserId)
    {
        var errorResponse = new APIResponse
        {
            StatusCode = HttpStatusCode.BadRequest,
            ErrorMessages = new List<string> { "User ID mismatch" }
        };
        return BadRequest(errorResponse);
    }

    var result = await _userService.UpdateUserAsync(updateUserDto);
    if (!result)
    {
        var errorResponse = new APIResponse
        {
            StatusCode = HttpStatusCode.NotFound,
            ErrorMessages = new List<string> { "User not found" }
        };
        return NotFound(errorResponse);
    }

    var successResponse = new APIResponse
    {
        StatusCode = HttpStatusCode.OK,
        Result = "User updated successfully"
    };

    return Ok(successResponse);
}
 }
}
