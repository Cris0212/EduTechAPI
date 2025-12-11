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
        public int ProfesorId { get; internal set; }
        public int MateriaId { get; internal set; }
        public int Trimestre { get; internal set; }
        public decimal Valor { get; internal set; }
        public DateTime Fecha { get; internal set; }
    }
}