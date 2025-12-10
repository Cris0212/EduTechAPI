using System.Text.RegularExpressions;
using EduTechApi.Context;
using EduTechApi.DTOs;
using EduTechApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace EduTechApi.Controllers
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

        private bool EsContrasenaSegura(string password, out string mensaje)
        {
            mensaje = string.Empty;

            if (password.Length < 8)
            {
                mensaje = "La contraseña debe tener al menos 8 caracteres.";
                return false;
            }

            if (password.Length > 100)
            {
                mensaje = "La contraseña no debe superar los 100 caracteres.";
                return false;
            }

            if (!Regex.IsMatch(password, "[A-Z]"))
            {
                mensaje = "La contraseña debe contener al menos una letra mayúscula.";
                return false;
            }

            if (!Regex.IsMatch(password, "[a-z]"))
            {
                mensaje = "La contraseña debe contener al menos una letra minúscula.";
                return false;
            }

            if (!Regex.IsMatch(password, "[0-9]"))
            {
                mensaje = "La contraseña debe contener al menos un número.";
                return false;
            }

            if (!Regex.IsMatch(password, "[^a-zA-Z0-9]"))
            {
                mensaje = "La contraseña debe contener al menos un símbolo (ej: !, @, #, $, %).";
                return false;
            }

            string[] comunes =
            {
                "12345678",
                "password",
                "qwerty",
                "123456",
                "abc123",
                "panama123"
            };

            if (comunes.Contains(password.ToLower()))
            {
                mensaje = "La contraseña es demasiado común, por favor usa otra más segura.";
                return false;
            }

            return true;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] RegistroRequest dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!EsContrasenaSegura(dto.Contrasena, out var errorPass))
                return BadRequest(errorPass);

            bool existeCorreo = await _context.Usuarios.AnyAsync(u => u.Correo == dto.Correo);
            if (existeCorreo)
                return BadRequest("El correo ya está registrado.");

            string hash = BC.HashPassword(dto.Contrasena);

            var usuario = new Usuario
            {
                Nombre = dto.Nombre.Trim(),
                Correo = dto.Correo.Trim(),
                ContrasenaHash = hash,
                Rol = dto.Rol
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            if (dto.Rol == "Alumno")
            {
                var alumno = new Alumno { UsuarioId = usuario.Id };
                _context.Alumnos.Add(alumno);
            }
            else if (dto.Rol == "Profesor")
            {
                var profesor = new Profesor { UsuarioId = usuario.Id };
                _context.Profesores.Add(profesor);
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Usuario registrado correctamente.",
                usuario.Id,
                usuario.Nombre,
                usuario.Correo,
                usuario.Rol
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == dto.Correo);

            if (usuario == null)
                return Unauthorized("Correo o contraseña incorrectos.");

            bool ok = BC.Verify(dto.Contrasena, usuario.ContrasenaHash);
            if (!ok)
                return Unauthorized("Correo o contraseña incorrectos.");

            return Ok(new
            {
                mensaje = "Login exitoso.",
                usuario.Id,
                usuario.Nombre,
                usuario.Correo,
                usuario.Rol
            });
        }
    }
}
