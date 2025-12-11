using System.ComponentModel.DataAnnotations;
using EduTechAPI.Models;
using EduTechPlusAPI.Models;

namespace EduTechApi.Models
{
    public class alumnomateria
    {
        [Required]
        public int alumnoId { get; set; }
        public alumno? Alumno { get; set; }

        [Required]
        public int profesorId { get; set; }
        public profesor? Profesor { get; set; }

        [Required]
        public int materiaId { get; set; }
        public materia? Materia { get; set; }
    }
}
