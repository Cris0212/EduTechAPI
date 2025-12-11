using EduTechApi.Models;
using EduTechAPI.Models;

namespace EduTechPlusAPI.Models
{
    public class materiaprofesor
    {
        public int id { get; set; }

        public int profesorId { get; set; }
        public profesor Profesor { get; set; } = null!;

        public int materiaId { get; set; }
        public materia Materia { get; set; } = null!;
    }
}
