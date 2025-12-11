
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
                query = query.Where(m => m.profesorid == profesorId.Value);

            if (materiaId.HasValue)
                query = query.Where(m => m.materiaid == materiaId.Value);

            if (grupoId.HasValue)
                query = query.Where(m => m.grupoid == grupoId.Value);

            var lista = await query
                .OrderByDescending(m => m.FechaPublicacion)
                .Select(m => new MaterialListadoDto
                {
                    Id = m.Id,
                    Titulo = m.titulo,
                    Descripcion = m.descripcion,
                    Url = m.url,
                    ProfesorId = m.profesorid,
                    MateriaId = m.materiaid,
                    GrupoId = m.grupoid,
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
                Titulo = material.titulo,
                Descripcion = material.descripcion,
                Url = material.url,
                ProfesorId = material.profesorid,
                MateriaId = material.materiaid,
                GrupoId = material.grupoid,
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

            var material = new material
            {
              titulo = dto.Titulo.Trim(),
              descripcion = string.IsNullOrWhiteSpace(dto.Descripcion)
                    ? null
                    : dto.Descripcion.Trim(),
                url = dto.Url.Trim(),
                profesorid = dto.ProfesorId,
                materiaid = dto.MateriaId,
                grupoid = dto.GrupoId,
                FechaPublicacion = System.DateTime.UtcNow
            };

            _context.Materiales.Add(material);
            await _context.SaveChangesAsync();

            var resultado = new MaterialListadoDto
            {
                Id = material.Id,
                Titulo = material.titulo,
                Descripcion = material.descripcion,
                Url = material.url,
                ProfesorId = material.profesorid,
                MateriaId = material.materiaid,
                GrupoId = material.grupoid,
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