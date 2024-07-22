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
            CreateMap<Role, AddNewRoleDTO>().ReverseMap();
            CreateMap<Visitor, VisitorLogDTO>()
            .ForMember(dest => dest.PurposeName, opt => opt.MapFrom(src => src.Purpose.PurposeName))
            .ForMember(dest => dest.DeviceName, opt => opt.MapFrom(src => src.VisitorDevices.FirstOrDefault().Device.DeviceName));

            // Reverse mapping from VisitorLogDTO to Visitor if needed
            CreateMap<VisitorLogDTO, Visitor>()
                .ForMember(dest => dest.Purpose, opt => opt.Ignore());
        }
    }
}
