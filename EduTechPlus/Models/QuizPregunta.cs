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
        public string texto { get; internal set; }
        public string tipo { get; internal set; }
        public string? opcionesjson { get; internal set; }
        public int orden { get; internal set; }
    }
}