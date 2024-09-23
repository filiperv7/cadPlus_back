using System.ComponentModel.DataAnnotations;

namespace CadPlus.API.Models
{
    public class AddressResponseDto
    {
        public Guid Id { get; set; }

        public string ZipCode { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }
    }
}
