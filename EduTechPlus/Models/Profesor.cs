using EduTechPlus.Models;
using System.ComponentModel.DataAnnotations;

namespace EduTechApi.Models
{
    public class Profesor
    {
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }

        public ICollection<ProfesorMateria> ProfesorMaterias { get; set; } = new List<ProfesorMateria>();
        public ICollection<AlumnoMateria> AlumnoMaterias { get; set; } = new List<AlumnoMateria>();
        public ICollection<Nota> NotasPuestas { get; set; } = new List<Nota>();
    }
}
