namespace EduTechPlusAPI.Models
{
    public class Materia
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public ICollection<MateriaProfesor> Profesores { get; set; } = new List<MateriaProfesor>();
    }
}