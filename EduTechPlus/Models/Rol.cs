using EduTechApi.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduTechPlus.Api.Models
{
    public class Rol
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        // Relación con usuarios
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}