using EduTechPlusAPI.Models;

namespace EduTechPlusAPI.Models
{
    public class QuizPregunta
    {
        public int Id { get; set; }
        public int QuizId { get; set; }

        public string Pregunta { get; set; } = null!;
        public string OpcionA { get; set; } = null!;
        public string OpcionB { get; set; } = null!;
        public string OpcionC { get; set; } = null!;
        public string OpcionD { get; set; } = null!;
        public string RespuestaCorrecta { get; set; } = null!;

        public Quiz Quiz { get; set; } = null!;
        public string Texto { get; internal set; }
        public string Tipo { get; internal set; }
        public string? OpcionesJson { get; internal set; }
        public int Orden { get; internal set; }
    }
}