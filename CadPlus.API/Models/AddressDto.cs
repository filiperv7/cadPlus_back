using System.ComponentModel.DataAnnotations;

namespace CadPlus.API.Models
{
    public class AddressDto
    {
        [Required]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "CEP deve conter exatamente 8 dígitos.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "CEP deve conter apenas dígitos.")]
        public string ZipCode { get; set; }

        [Required]
        public string Street { get; set; }
        
        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }
        
    }
}
