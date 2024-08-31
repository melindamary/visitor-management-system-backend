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
            .ForMember(dest => dest.PurposeName, opt => opt.MapFrom(src => src.Purpose.Name));

            // Reverse mapping from VisitorLogDTO to Visitor if needed
            CreateMap<VisitorLogDTO, Visitor>()
                .ForMember(dest => dest.Purpose, opt => opt.Ignore());

            CreateMap<OfficeLocation, LocationDetailsDTO>();

            CreateMap<AddOfficeLocationDTO, OfficeLocation>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());

            CreateMap<UpdateLocationDTO, OfficeLocation>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore());
        }
    }
}
