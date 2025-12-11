using EduTechApi.Models;
using EduTechPlus.Models;

namespace EduTechAPI.Models
{
    public class QuizResultado
    {
        public int Id { get; set; }

        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; } = null!;

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; } = null!;

        public int Puntaje { get; set; }
        public DateTime Fecha { get; set; }
    }
}