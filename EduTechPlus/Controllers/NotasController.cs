using EduTechApi.Context;
using EduTechApi.DTOs;
using EduTechApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduTechApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NotasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarNota([FromBody] NotaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nota = new Nota
            {
                AlumnoId = dto.AlumnoId,
                ProfesorId = dto.ProfesorId,
                MateriaId = dto.MateriaId,
                Trimestre = dto.Trimestre,
                Tipo = dto.Tipo,
                Valor = dto.Valor,
                Fecha = DateTime.UtcNow
            };

            _context.Notas.Add(nota);
            await _context.SaveChangesAsync();

            return Ok("Nota registrada correctamente.");
        }

        [HttpGet("alumno/{alumnoId:int}")]
        public async Task<IActionResult> GetNotasAlumno(int alumnoId, [FromQuery] int? trimestre = null)
        {
            var query = _context.Notas
                .Include(n => n.Materia)
                .Include(n => n.Profesor).ThenInclude(p => p.Usuario)
                .Where(n => n.AlumnoId == alumnoId)
                .AsNoTracking();

            if (trimestre.HasValue)
                query = query.Where(n => n.Trimestre == trimestre.Value);

            var lista = await query
                .Select(n => new
                {
                    n.Id,
                    n.Trimestre,
                    n.Tipo,
                    n.Valor,
                    Materia = n.Materia!.Nombre,
                    Profesor = n.Profesor!.Usuario!.Nombre,
                    n.Fecha
                })
                .ToListAsync();

            return Ok(lista);
        }
    }
}
