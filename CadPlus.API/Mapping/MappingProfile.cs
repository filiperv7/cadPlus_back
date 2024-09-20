using CadPlus.API.Models;
using CadPlus.Domain.Entities;

namespace CadPlus.API.Mapping
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCreationDto, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<AddressDto, Address>()
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
                .ReverseMap();

            CreateMap<User, UserResponseDto>()
               .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses))
               .ForMember(dest => dest.Profiles, opt => opt.MapFrom(src => src.Profiles));

            CreateMap<Profile, ProfileDto>();
        }
    }
}
