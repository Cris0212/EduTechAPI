using EduTechPlusAPI.Models;
namespace EduTechPlusAPI.Models
{
    public class materia
    {
        public int id { get; set; }
        public string nombre { get; set; } = null!;

        public ICollection<materiaprofesor> Profesores { get; set; } = new List<materiaprofesor>();
    }
}