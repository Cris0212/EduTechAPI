using System.ComponentModel.DataAnnotations;
using EduTechAPI.Models;
using EduTechPlusAPI.Models;

namespace EduTechApi.Models
{
    public class AlumnoMateria
    {
        [Required]
        public int AlumnoId { get; set; }
        public Alumno? Alumno { get; set; }

        [Required]
        public int ProfesorId { get; set; }
        public Profesor? Profesor { get; set; }

        [Required]
        public int MateriaId { get; set; }
        public Materia? Materia { get; set; }
    }
}
