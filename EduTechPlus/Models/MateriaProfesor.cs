using EduTechApi.Models;

namespace EduTechPlusAPI.Models
{
    public class MateriaProfesor
    {
        public int Id { get; set; }

        public int ProfesorId { get; set; }
        public Profesor Profesor { get; set; } = null!;

        public int MateriaId { get; set; }
        public Materia Materia { get; set; } = null!;
    }
}
