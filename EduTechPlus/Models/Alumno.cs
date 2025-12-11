using EduTechAPI.Models;

namespace EduTechPlusAPI.Models
{
    public class Alumno
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; } = null!;

        public ICollection<Nota> Notas { get; set; } = new List<Nota>();
        public ICollection<QuizResultado> ResultadosQuiz { get; set; } = new List<QuizResultado>();
    }
}