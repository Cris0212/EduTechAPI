
using EduTechAPI.DTOs;
using EduTechAPI.Models;
using EduTechPlus.Api.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduTechAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MaterialesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Materiales
        // Opcional: ?profesorId=1&materiaId=2&grupoId=3
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialListadoDto>>> GetMateriales(
            [FromQuery] int? profesorId,
            [FromQuery] int? materiaId,
            [FromQuery] int? grupoId)
        {
            var query = _context.Materiales.AsNoTracking().AsQueryable();

            if (profesorId.HasValue)
                query = query.Where(m => m.ProfesorId == profesorId.Value);

            if (materiaId.HasValue)
                query = query.Where(m => m.MateriaId == materiaId.Value);

            if (grupoId.HasValue)
                query = query.Where(m => m.GrupoId == grupoId.Value);

            var lista = await query
                .OrderByDescending(m => m.FechaPublicacion)
                .Select(m => new MaterialListadoDto
                {
                    Id = m.Id,
                    Titulo = m.Titulo,
                    Descripcion = m.Descripcion,
                    Url = m.Url,
                    ProfesorId = m.ProfesorId,
                    MateriaId = m.MateriaId,
                    GrupoId = m.GrupoId,
                    FechaPublicacion = m.FechaPublicacion
                })
                .ToListAsync();

            return Ok(lista);
        }

        // GET: api/Materiales/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MaterialListadoDto>> GetMaterial(int id)
        {
            var material = await _context.Materiales
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (material == null)
                return NotFound();

            var dto = new MaterialListadoDto
            {
                Id = material.Id,
                Titulo = material.Titulo,
                Descripcion = material.Descripcion,
                Url = material.Url,
                ProfesorId = material.ProfesorId,
                MateriaId = material.MateriaId,
                GrupoId = material.GrupoId,
                FechaPublicacion = material.FechaPublicacion
            };

            return Ok(dto);
        }

        // POST: api/Materiales
        [HttpPost]
        public async Task<ActionResult<MaterialListadoDto>> CrearMaterial(MaterialCrearDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var material = new Material
            {
                Titulo = dto.Titulo.Trim(),
                Descripcion = string.IsNullOrWhiteSpace(dto.Descripcion)
                    ? null
                    : dto.Descripcion.Trim(),
                Url = dto.Url.Trim(),
                ProfesorId = dto.ProfesorId,
                MateriaId = dto.MateriaId,
                GrupoId = dto.GrupoId,
                FechaPublicacion = System.DateTime.UtcNow
            };

            _context.Materiales.Add(material);
            await _context.SaveChangesAsync();

            var resultado = new MaterialListadoDto
            {
                Id = material.Id,
                Titulo = material.Titulo,
                Descripcion = material.Descripcion,
                Url = material.Url,
                ProfesorId = material.ProfesorId,
                MateriaId = material.MateriaId,
                GrupoId = material.GrupoId,
                FechaPublicacion = material.FechaPublicacion
            };

            // Para que en Swagger muestre la URL del recurso: api/Materiales/{id}
            return CreatedAtAction(nameof(GetMaterial), new { id = material.Id }, resultado);
        }

        // DELETE: api/Materiales/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> EliminarMaterial(int id)
        {
            var material = await _context.Materiales.FindAsync(id);

            if (material == null)
                return NotFound();

            _context.Materiales.Remove(material);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}