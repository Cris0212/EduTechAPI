using EduTechApi.Models;

namespace EduTechPlusAPI.Models
{
    public class Nota
    {
        public int Id { get; set; }

        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; } = null!;

        public string Tipo { get; set; } = null!; // Diaria, Apreciación, Examen Final
        public decimal Calificacion { get; set; }
    }
}