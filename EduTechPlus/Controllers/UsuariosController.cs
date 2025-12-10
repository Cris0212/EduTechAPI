using EduTechApi.Context;
using EduTechApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduTechApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioListadoDto>>> GetUsuarios()
        {
            var lista = await _context.Usuarios
                .AsNoTracking()
                .Select(u => new UsuarioListadoDto
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Correo = u.Correo,
                    Rol = u.Rol
                })
                .ToListAsync();

            return Ok(lista);
        }
    }
}
