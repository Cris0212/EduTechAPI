using EduTechAPI.Models;
using EduTechPlus.Api.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EduTechPlus.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        public class RegistroDto
        {
            public string Nombre { get; set; } = "";
            public string Correo { get; set; } = "";
            public string Contrasena { get; set; } = "";
            public string Rol { get; set; } = "Alumno"; // Alumno / Profesor
            public object?[]? RolId { get; internal set; }
        }

        public class LoginDto
        {
            public string Correo { get; set; } = "";
            public string Contrasena { get; set; } = "";
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] RegistroDto dto)
        {
            // Buscar el rol por nombre
            var rol = await _context.Roles.FirstOrDefaultAsync(r => r.nombre.ToLower() == dto.Rol.ToLower());
            if (rol == null)
                return BadRequest("El rol proporcionado no existe. Debe ser 'Alumno' o 'Profesor'.");

            // Crear usuario
            var usuario = new usuario
            {
                nombre = dto.Nombre,
                correo = dto.Correo,
                contrasenahash = HashPassword(dto.Contrasena), // Tu función de hash
                rolid = rol.id
            };

            _context.usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Usuario registrado correctamente" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var usuario = await _context.usuarios
                .FirstOrDefaultAsync(u => u.correo == dto.Correo);

            if (usuario == null || !VerifyPassword(dto.Contrasena, usuario.contrasenahash))
                return Unauthorized("Credenciales inválidas.");

            return Ok(new
            {
                mensaje = "Login correcto",
                usuario.id,
                usuario.nombre,
                usuario.correo,
                usuario.rol
            });
        }
    }
}