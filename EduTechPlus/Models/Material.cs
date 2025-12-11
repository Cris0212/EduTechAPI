using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTechAPI.Models
{
    [Table("Materiales")]
    public class material
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "El título puede tener máximo 150 caracteres.")]
        public string titulo { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "La descripción puede tener máximo 500 caracteres.")]
        public string? descripcion { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "La URL puede tener máximo 300 caracteres.")]
        public string url { get; set; } = string.Empty;

        [Required]
        public int profesorid { get; set; }

        [Required]
        public int materiaid { get; set; }

        [Required]
        public int grupoid { get; set; }

        public DateTime FechaPublicacion { get; set; } = DateTime.UtcNow;
    }
}