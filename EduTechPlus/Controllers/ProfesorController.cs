using System.Threading.Tasks;
using EduTechPlus.Api.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduTechPlus.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProfesoresController(AppDbContext context)
        {
            _context = context;
        }

        public class ProfesorDetalleDto
        {
            public int UsuarioId { get; set; }
            public string Colegio { get; set; } = "";
            public string Turno { get; set; } = "";
            public int GruposQueDa { get; set; }
        }

        [HttpPost("detalle")]
        public async Task<IActionResult> RegistrarDetalle([FromBody] ProfesorDetalleDto dto)
        {
            var usuario = await _context.Usuarios.FindAsync(dto.UsuarioId);
            if (usuario == null)
                return BadRequest("Usuario no encontrado.");

            var yaTiene = await _context.Profesores
                .AnyAsync(p => p.UsuarioId == dto.UsuarioId);

            if (yaTiene)
                return BadRequest("Este profesor ya tiene datos registrados.");

            var profesor = new Profesor
            {
                UsuarioId = dto.UsuarioId,
                Colegio = dto.Colegio,
                Turno = dto.Turno,
                GruposQueDa = dto.GruposQueDa
            };

            _context.Profesores.Add(profesor);
            await _context.SaveChangesAsync();

            return Ok(profesor);
        }

        [HttpGet("usuario/{usuarioId:int}")]
        public async Task<IActionResult> ObtenerPorUsuario(int usuarioId)
        {
            var profesor = await _context.Profesores
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.UsuarioId == usuarioId);

            if (profesor == null)
                return NotFound();

            return Ok(profesor);
        }
    }
}