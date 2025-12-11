using EduTechAPI.Models;
using EduTechPlusAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace EduTechApi.Models
{
    public class ProfesorMateria
    {
        [Required]
        public int profesorid { get; set; }
        public profesor? Profesor { get; set; }

        [Required]
        public int materiaid { get; set; }
        public materia? Materia { get; set; }
    }
}

