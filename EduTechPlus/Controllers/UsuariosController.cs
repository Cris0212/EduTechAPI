using System.Linq;
using System.Threading.Tasks;
using EduTechPlus.Api.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduTechPlus.Api.Controllers
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
        public async Task<IActionResult> Get()
        {
            var lista = await _context.usuarios
                .AsNoTracking()
                .Select(u => new
                {
                    u.id,
                    u.nombre,
                    u.correo,
                    u.rol,
                    u.rolid
                })
                .ToListAsync();

            return Ok(lista);
        }
    }
}