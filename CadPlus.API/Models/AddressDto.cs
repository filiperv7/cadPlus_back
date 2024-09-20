using System.ComponentModel.DataAnnotations;

namespace CadPlus.API.Models
{
    public class AddressDto
    {
        [Required]
        public string ZipCode { get; set; }

        [Required]
        public string Street { get; set; }
        
        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }
        
    }
}
