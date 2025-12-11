using EduTechAPI.Models;
using EduTechPlusAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace EduTechApi.Models
{
    public class ProfesorMateria
    {
        [Required]
        public int ProfesorId { get; set; }
        public Profesor? Profesor { get; set; }

        [Required]
        public int MateriaId { get; set; }
        public Materia? Materia { get; set; }
    }
}

