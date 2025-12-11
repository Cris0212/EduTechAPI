using EduTechAPI.Models;

namespace EduTechPlusAPI.Models
{
    public class alumno
    {
        public int id { get; set; }
        public int usuarioId { get; set; }

        public usuario Usuario { get; set; } = null!;

        public ICollection<Nota> Notas { get; set; } = new List<Nota>();
        public ICollection<QuizResultado> ResultadosQuiz { get; set; } = new List<QuizResultado>();
    }
}