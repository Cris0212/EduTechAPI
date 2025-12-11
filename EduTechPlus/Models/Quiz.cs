using EduTechApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace EduTechPlus.Models
{
    [Table("Quizzes")]
    public class Quiz
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Titulo { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Descripcion { get; set; }

        // Relación con Profesor
        [Required]
        public int ProfesorId { get; set; }

        // Relación con Materia
        [Required]
        public int MateriaId { get; set; }

        // Relación con Grupo
        [Required]
        public int GrupoId { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        // Navegación (opcional, si ya tienes estos modelos)
        public Profesor? Profesor { get; set; }
        public Materia? Materia { get; set; }
      

        public ICollection<QuizPregunta> Preguntas { get; set; } = new List<QuizPregunta>();
    }
}