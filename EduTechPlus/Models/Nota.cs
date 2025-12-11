using EduTechApi.Models;

namespace EduTechPlusAPI.Models
{
    public class Nota
    {
        public int id { get; set; }

        public int alumnoid { get; set; }
        public alumno Alumno { get; set; } = null!;

        public string tipo { get; set; } = null!; // Diaria, Apreciación, Examen Final
        public decimal calificacion { get; set; }
        public int profesorid { get; internal set; }
        public int materiaid { get; internal set; }
        public int trimestre { get; internal set; }
        public decimal valor { get; internal set; }
        public DateTime fecha { get; internal set; }
    }
}