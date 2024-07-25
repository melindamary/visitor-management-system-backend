﻿using Microsoft.AspNetCore.Mvc;
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

            var userRole = await _userService.GetUserRoleByUsername(username);

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
        public async Task<IActionResult> CreateNewUser([FromBody] AddNewUserDTO addNewUserDto
            )
        {
             await _userService.AddUserAsync(addNewUserDto);
            return Ok();
        }
    }
}
