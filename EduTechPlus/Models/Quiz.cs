using EduTechApi.Models;
using EduTechAPI.Models;

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
        public int GrupoId { get; internal set; }
        public int MateriaId { get; internal set; }
        
    }
}