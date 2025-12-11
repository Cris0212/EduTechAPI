using EduTechPlusApi.Models;

namespace EduTechAPI.Models
{
    public class usuario
    {
        public int id { get; set; }

        public string nombre { get; set; } = null!;
        public string correo { get; set; } = null!;
        public string contrasenahash { get; set; } = null!;

        public int rolid { get; set; }
        public rol rol { get; set; } = null!;

       
    }
}