using EduTechPlus.Models;
using System.ComponentModel.DataAnnotations;

namespace EduTechApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(200, ErrorMessage = "El nombre no puede superar los 200 caracteres.")]
        [MinLength(3, ErrorMessage = "El nombre debe tener al menos 3 caracteres.")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúñÑ ]+$",
            ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        [MaxLength(200, ErrorMessage = "El correo no puede superar los 200 caracteres.")]
        public string Correo { get; set; } = string.Empty;

        // Aquí se guarda el HASH, no la contraseña en texto plano
        [Required]
        [MaxLength(200)]
        public string ContrasenaHash { get; set; } = string.Empty;

        [Required(ErrorMessage = "El rol es obligatorio.")]
        [RegularExpression("^(Alumno|Profesor)$",
            ErrorMessage = "El rol debe ser Alumno o Profesor.")]
        [MaxLength(50)]
        public string Rol { get; set; } = string.Empty;

        public Alumno? Alumno { get; set; }
        public Profesor? Profesor { get; set; }
    }
}
