using EduTechApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace EduTechPlus.Models
{
    [Table("QuizPreguntas")]
    public class QuizPregunta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int QuizId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Texto { get; set; } = string.Empty;

        /// <summary>
        /// Ej: "opcion_multiple", "verdadero_falso", etc.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Tipo { get; set; } = "opcion_multiple";

        /// <summary>
        /// Opciones en JSON (si aplica). Ej: ["A","B","C","D"]
        /// </summary>
        public string? OpcionesJson { get; set; }

        /// <summary>
        /// Respuesta correcta (puede ser letra, texto, etc.)
        /// </summary>
        public string? RespuestaCorrecta { get; set; }

        public int Orden { get; set; } = 1;

        // Navegación
        public Quiz? Quiz { get; set; }
    }
}