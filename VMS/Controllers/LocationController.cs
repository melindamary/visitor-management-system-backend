using Microsoft.AspNetCore.Mvc;
using VMS.Models.DTO;
using VMS.Repository.IRepository;

namespace VMS.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LocationController : ControllerBase
    {
        private readonly IlocationRepository _repository;
        public LocationController(IlocationRepository roleRepository)
        {
            this._repository = roleRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<LocationIdAndNameDTO>> GetLocationIdAndName()
        {
            return await _repository.GetLocationIdAndNameAsync();
        }



    }
}

