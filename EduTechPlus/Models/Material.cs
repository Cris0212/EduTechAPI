using EduTechApi.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace EduTechAPI.Models
{
    [Table("Materiales")]
    public class Material
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "El título puede tener máximo 150 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "La descripción puede tener máximo 500 caracteres.")]
        public string? Descripcion { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "La URL puede tener máximo 300 caracteres.")]
        public string Url { get; set; } = string.Empty;

        [Required]
        public int ProfesorId { get; set; }

        [Required]
        public int MateriaId { get; set; }

        [Required]
        public int GrupoId { get; set; }

        public DateTime FechaPublicacion { get; set; } = DateTime.UtcNow;

        // Navegación (opcional, solo si tienes estas clases)
        public Profesor? Profesor { get; set; }
        public Materia? Materia { get; set; }
    }
}