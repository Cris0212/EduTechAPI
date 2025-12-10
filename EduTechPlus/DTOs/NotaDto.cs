using System.ComponentModel.DataAnnotations;

namespace EduTechApi.DTOs
{
    public class NotaDto
    {
        [Required]
        public int AlumnoId { get; set; }

        [Required]
        public int ProfesorId { get; set; }

        [Required]
        public int MateriaId { get; set; }

        [Range(1, 3)]
        public int Trimestre { get; set; }

        [Required]
        [RegularExpression("^(Diaria|Apreciacion|Examen)$")]
        public string Tipo { get; set; } = string.Empty;

        [Range(0, 100)]
        public decimal Valor { get; set; }
    }
}
