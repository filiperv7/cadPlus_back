using System.ComponentModel.DataAnnotations;

namespace CadPlus.API.Models
{
    public class UserUpdateDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve conter exatamente 11 dígitos.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter apenas dígitos numéricos.")]
        public string CPF { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "O Nome deve ter pelo menos 4 caracteres.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Número de telefone deve conter exatamente 11 dígitos.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Número de telefone deve conter apenas dígitos numéricos.")]
        public string Phone { get; set; }

        [Required]
        public List<AddressDto> Addresses { get; set; }

        public List<Guid> AddressesExcluded { get; set; } = new List<Guid>();
    }
}
