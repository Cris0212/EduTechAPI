using EduTechApi.Context;
using EduTechApi.DTOs;
using EduTechApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduTechApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MateriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MateriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MateriaDto>>> GetMaterias()
        {
            var lista = await _context.Materias
                .AsNoTracking()
                .Select(m => new MateriaDto
                {
                    Id = m.Id,
                    Nombre = m.Nombre
                })
                .ToListAsync();

            return Ok(lista);
        }

        [HttpPost]
        public async Task<IActionResult> CrearMateria([FromBody] MateriaDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest("El nombre de la materia es obligatorio.");

            var materia = new Materia { Nombre = dto.Nombre.Trim() };

            _context.Materias.Add(materia);
            await _context.SaveChangesAsync();

            dto.Id = materia.Id;

            return CreatedAtAction(nameof(GetMaterias), new { id = dto.Id }, dto);
        }
    }
}
