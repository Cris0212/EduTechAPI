using EduTechApi.Models;
using EduTechAPI.Models;

namespace EduTechPlusAPI.Models
{
    public class Quiz
    {
        public int id { get; set; }
        public string titulo { get; set; } = null!;
        public DateTime fechacreacion { get; set; }

        public int profesorid { get; set; }
        public profesor Profesor { get; set; } = null!;

        public ICollection<QuizPregunta> preguntas { get; set; } = new List<QuizPregunta>();
        public int grupoid { get; internal set; }
        public int materiaid { get; internal set; }
        
    }
}