using EduTechApi.Models;

namespace EduTechPlusAPI.Models
{
    public class Profesor
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }

        public string Colegio { get; set; } = null!;
        public string Turno { get; set; } = null!;
        public int GruposQueDa { get; set; }

        public Usuario Usuario { get; set; } = null!;

        public ICollection<MateriaProfesor> Materias { get; set; } = new List<MateriaProfesor>();
        public ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();
    }
}