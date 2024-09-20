using System.ComponentModel.DataAnnotations;

namespace CadPlus.API.Models
{
    public class LoginDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "A senha deve ter pelo menos 8 caracteres.")]
        public string Password { get; set; }
    }
}
