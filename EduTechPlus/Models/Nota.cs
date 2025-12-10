using System.ComponentModel.DataAnnotations;

namespace EduTechApi.Models
{
    public class Nota
    {
        public int Id { get; set; }

        [Required]
        public int AlumnoId { get; set; }
        public Alumno? Alumno { get; set; }

        [Required]
        public int ProfesorId { get; set; }
        public Profesor? Profesor { get; set; }

        [Required]
        public int MateriaId { get; set; }
        public Materia? Materia { get; set; }

        [Range(1, 3, ErrorMessage = "El trimestre debe ser 1, 2 o 3.")]
        public int Trimestre { get; set; }

        [Required]
        [RegularExpression("^(Diaria|Apreciacion|Examen)$",
            ErrorMessage = "El tipo debe ser Diaria, Apreciacion o Examen.")]
        public string Tipo { get; set; } = string.Empty;

        [Range(0, 100, ErrorMessage = "La nota debe estar entre 0 y 100.")]
        public decimal Valor { get; set; }

        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }
}
