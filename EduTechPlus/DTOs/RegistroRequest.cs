using System.ComponentModel.DataAnnotations;

namespace EduTechApi.DTOs
{
    public class RegistroRequest
    {
        [Required]
        [MaxLength(200)]
        [MinLength(3)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Correo { get; set; } = string.Empty;

        [Required]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [MaxLength(100, ErrorMessage = "La contraseña no debe superar 100 caracteres.")]
        public string Contrasena { get; set; } = string.Empty;

        [Required]
        [RegularExpression("^(Alumno|Profesor)$",
            ErrorMessage = "El rol debe ser Alumno o Profesor.")]
        public string Rol { get; set; } = string.Empty;
    }
}
