
using EduTechPlus.Api.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduTechApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlumnosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlumnosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAlumnos()
        {
            var lista = await _context.Alumnos
                .Include(a => a.Usuario)
                .AsNoTracking()
                .Select(a => new
                {
                    a.Id,
                    Nombre = a.Usuario!.Nombre,
                    Correo = a.Usuario.Correo
                })
                .ToListAsync();

            return Ok(lista);
        }
    }
}
