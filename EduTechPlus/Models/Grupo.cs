using EduTechApi.Models;
using EduTechAPI.Models;

namespace EduTechPlusAPI.Models
{
    public class grupo
    {
        public int id { get; set; }
        public string nombre { get; set; } = null!;

        public int profesorId { get; set; }
        public profesor Profesor { get; set; } = null!;
    }
}