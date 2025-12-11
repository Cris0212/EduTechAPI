using System.ComponentModel.DataAnnotations;

namespace EduTechPlus.DTOs
{
    public class QuizPreguntaCrearDto
    {
        [Required]
        public int QuizId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Texto { get; set; } = string.Empty;

        /// <summary>
        /// "opcion_multiple", "verdadero_falso", etc.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Tipo { get; set; } = "opcion_multiple";

        /// <summary>
        /// Opciones en JSON, por ejemplo: ["A","B","C","D"]
        /// </summary>
        public string? OpcionesJson { get; set; }

        public string? RespuestaCorrecta { get; set; }

        public int Orden { get; set; } = 1;
    }

    public class QuizPreguntaListaDto
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string Texto { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string? OpcionesJson { get; set; }
        public int Orden { get; set; }
    }
}