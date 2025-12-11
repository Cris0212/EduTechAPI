using EduTechAPI.Models;

namespace EduTechPlusApi.Models
{
    public class rol
    {
        public int id { get; set; }
        public string nombre { get; set; } = null!;

        // Relación -> Usuarios
        public ICollection<usuario> Usuarios { get; set; } = new List<usuario>();
    }
}