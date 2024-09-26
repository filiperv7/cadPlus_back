using CadPlus.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CadPlus.API.Models
{
    public class HealthStatusDto
    {
        [Required]
        public HealthStatus HealthStatus { get; set; }
    }
}
