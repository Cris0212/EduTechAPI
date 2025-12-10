using System.ComponentModel.DataAnnotations;

namespace EduTechApi.DTOs
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        [Required]
        public string Contrasena { get; set; } = string.Empty;
    }
}
