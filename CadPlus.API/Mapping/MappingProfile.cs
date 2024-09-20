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

            CreateMap<AddressDto, Address>();
        }
    }
}
