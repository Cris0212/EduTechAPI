using EduTechApi.Models;

namespace EduTechPlusAPI.Models
{
    public class Grupo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public int ProfesorId { get; set; }
        public Profesor Profesor { get; set; } = null!;
    }
}