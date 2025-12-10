using EduTechPlus.Models;
using System.ComponentModel.DataAnnotations;

namespace EduTechApi.Models
{
    public class Materia
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la materia es obligatorio.")]
        [MaxLength(200, ErrorMessage = "El nombre de la materia no puede superar 200 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        public ICollection<ProfesorMateria> ProfesorMaterias { get; set; } = new List<ProfesorMateria>();
        public ICollection<AlumnoMateria> AlumnoMaterias { get; set; } = new List<AlumnoMateria>();
        public ICollection<Nota> Notas { get; set; } = new List<Nota>();
    }
}
