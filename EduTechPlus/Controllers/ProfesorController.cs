using EduTechApi.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduTechApi.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetProfesores()
        {
            var lista = await _context.Profesores
                .Include(p => p.Usuario)
                .AsNoTracking()
                .Select(p => new
                {
                    p.Id,
                    Nombre = p.Usuario!.Nombre,
                    Correo = p.Usuario.Correo
                })
                .ToListAsync();

            return Ok(lista);
        }
    }
}
