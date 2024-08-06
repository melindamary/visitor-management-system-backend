using Microsoft.AspNetCore.Mvc;
using System.Linq;

using VMS.Models;
using VMS.Data;
using VMS.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Data;
using VMS.Repository.IRepository;
namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RoleController : ControllerBase
    {

        private readonly IRoleRepository _roleRepository;
        public RoleController(IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
        }
        
        [HttpGet("get-role-id-name")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IEnumerable<GetRoleIdAndNameDTO>> GetRoleIdAndName()
        {
            return await _roleRepository.GetRoleIdAndNameAsync();
        }    

        

    }
   
}
