// to map POCO classes and DTO
using AutoMapper;
using VMS.Models;
using VMS.Models.DTO;

namespace VMS
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            //syntax : CreateMap<POCO class, DTO class>().ReverseMap();
            CreateMap<Roles, AddNewRoleDTO>().ReverseMap();
        }
    }
}
