using EduTechApi.Models;

namespace EduTechPlusAPI.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }

        public int ProfesorId { get; set; }
        public Profesor Profesor { get; set; } = null!;

        public ICollection<QuizPregunta> Preguntas { get; set; } = new List<QuizPregunta>();
    }
}