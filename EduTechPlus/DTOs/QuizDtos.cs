using System;
using System.ComponentModel.DataAnnotations;

namespace EduTechPlus.DTOs
{
    public class QuizCrearDto
    {
        [Required]
        [MaxLength(200)]
        public string Titulo { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Descripcion { get; set; }

        [Required]
        public int ProfesorId { get; set; }

        [Required]
        public int MateriaId { get; set; }

        [Required]
        public int GrupoId { get; set; }
    }

    public class QuizListaDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int ProfesorId { get; set; }
        public int MateriaId { get; set; }
        public int GrupoId { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public class QuizDetalleDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int ProfesorId { get; set; }
        public int MateriaId { get; set; }
        public int GrupoId { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Opcional: cantidad de preguntas
        public int TotalPreguntas { get; set; }
    }
}