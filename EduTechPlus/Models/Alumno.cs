using EduTechPlus.Models;
using System.ComponentModel.DataAnnotations;

namespace EduTechApi.Models
{
    public class Alumno
    {
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }

        public ICollection<AlumnoMateria> AlumnoMaterias { get; set; } = new List<AlumnoMateria>();
        public ICollection<Nota> Notas { get; set; } = new List<Nota>();
    }
}
