using EduTechPlusAPI.Models;

namespace EduTechAPI.Models
{
    public class QuizResultado
    {
        public int id { get; set; }

        public int alumnoid { get; set; }
        public alumno Alumno { get; set; } = null!;

        public int quizId { get; set; }
        public Quiz Quiz { get; set; } = null!;

        public int puntaje { get; set; }
        public DateTime Fecha { get; set; }
    }
}