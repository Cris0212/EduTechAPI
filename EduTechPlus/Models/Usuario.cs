using EduTechPlus.Api.Models;

namespace EduTechPlusAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string ContrasenaHash { get; set; } = null!;

        public int RolId { get; set; }
        public Rol Rol { get; set; } = null!;
    }
}