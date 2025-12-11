using System;
using System.ComponentModel.DataAnnotations;

namespace EduTechAPI.DTOs
{
    public class MaterialCrearDto
    {
        [Required]
        [StringLength(150)]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descripcion { get; set; }

        [Required]
        [StringLength(300)]
        public string Url { get; set; } = string.Empty;

        [Required]
        public int ProfesorId { get; set; }

        [Required]
        public int MateriaId { get; set; }

        [Required]
        public int GrupoId { get; set; }
    }

    public class MaterialListadoDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string Url { get; set; } = string.Empty;
        public int ProfesorId { get; set; }
        public int MateriaId { get; set; }
        public int GrupoId { get; set; }
        public DateTime FechaPublicacion { get; set; }
    }
}