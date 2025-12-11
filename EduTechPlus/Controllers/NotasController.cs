using EduTechPlus.Api.Context;
using EduTechPlusAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduTechPlus.Api.Controllers
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

        // ============================
        // 1. CREAR NOTA
        // ============================
        [HttpPost("crear")]
        public async Task<IActionResult> CrearNota([FromBody] Nota nota)
        {
            if (nota == null)
                return BadRequest("Datos inválidos");

            _context.Notas.Add(nota);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Nota registrada correctamente",
                nota
            });
        }

        // ============================
        // 2. LISTAR NOTAS POR ALUMNO
        // ============================
        [HttpGet("alumno/{alumnoId}")]
        public async Task<IActionResult> ObtenerPorAlumno(int alumnoId)
        {
            var notas = await _context.Notas
                                      .Where(n => n.AlumnoId == alumnoId)
                                      .ToListAsync();

            return Ok(notas);
        }

        // ============================
        // 3. LISTAR NOTAS POR MATERIA
        // ============================
        [HttpGet("materia/{materiaId}")]
        public async Task<IActionResult> ObtenerPorMateria(int materiaId)
        {
            var notas = await _context.Notas
                                      .Where(n => n.MateriaId == materiaId)
                                      .ToListAsync();

            return Ok(notas);
        }

        // ============================
        // 4. BORRAR NOTA
        // ============================
        [HttpDelete("borrar/{id}")]
        public async Task<IActionResult> BorrarNota(int id)
        {
            var nota = await _context.Notas.FindAsync(id);

            if (nota == null)
                return NotFound("Nota no encontrada");

            _context.Notas.Remove(nota);
            await _context.SaveChangesAsync();

            return Ok("Nota eliminada correctamente");
        }
    }
}
