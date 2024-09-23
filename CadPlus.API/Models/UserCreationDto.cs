using System.ComponentModel.DataAnnotations;

namespace CadPlus.API.Models
{
    public class UserCreationDto
    {
        [Required]
        public string CPF { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "A Nome deve ter pelo menos 4 caracteres.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "A senha deve ter pelo menos 8 caracteres.")]
        public string Password { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Número de telefone deve conter exatamente 11 dígitos.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Número de telefone deve conter apenas dígitos numéricos.")]
        public string Phone { get; set; }

        [Required]
        public List<AddressDto> Addresses { get; set; }

        [Required]
        [AllowedValues([1,2,3,4], ErrorMessage = "O valores aceitos são 1 para Admin, 2 para Médico(a), 3 para Enfermeiro(a) ou 4 para Paciente")]
        public int IdProfile { get; set; }
    }
}
