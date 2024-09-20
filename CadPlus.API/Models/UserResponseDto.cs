using CadPlus.Domain.Enums;

namespace CadPlus.API.Models
{
    public class UserResponseDto
    {
        public string CPF { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public HealthStatus HealthStatus { get; set; }
        public List<AddressDto> Addresses { get; set; }
        public List<ProfileDto> Profiles { get; set; }
    }
}
