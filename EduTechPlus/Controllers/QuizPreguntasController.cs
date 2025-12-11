using EduTechApi.Context;
using EduTechPlus.Context;
using EduTechPlus.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduTechPlus.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizPreguntasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuizPreguntasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/QuizPreguntas?quizId=1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizPregunta>>> GetPreguntas([FromQuery] int quizId)
        {
            if (quizId <= 0)
                return BadRequest("Debe indicar un quizId válido.");

            var lista = await _context.QuizPreguntas
                .AsNoTracking()
                .Where(p => p.QuizId == quizId)
                .OrderBy(p => p.Orden)
                .ToListAsync();

            return Ok(lista);
        }

        // GET: api/QuizPreguntas/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<QuizPregunta>> GetPregunta(int id)
        {
            var pregunta = await _context.QuizPreguntas
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pregunta == null)
                return NotFound();

            return Ok(pregunta);
        }

        // POST: api/QuizPreguntas
        public class CrearPreguntaDto
        {
            public int QuizId { get; set; }
            public string Texto { get; set; } = string.Empty;
            public string Tipo { get; set; } = "opcion_multiple"; // opcional
            public string? OpcionesJson { get; set; }             // JSON con opciones si aplica
            public string? RespuestaCorrecta { get; set; }
            public int Orden { get; set; }
        }

        [HttpPost]
        public async Task<ActionResult<QuizPregunta>> CrearPregunta(CrearPreguntaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existeQuiz = await _context.Quizzes.AnyAsync(q => q.Id == dto.QuizId);
            if (!existeQuiz)
                return BadRequest("El quiz indicado no existe.");

            var pregunta = new QuizPregunta
            {
                QuizId = dto.QuizId,
                Texto = dto.Texto.Trim(),
                Tipo = dto.Tipo.Trim(),
                OpcionesJson = dto.OpcionesJson,
                RespuestaCorrecta = dto.RespuestaCorrecta,
                Orden = dto.Orden
            };

            _context.QuizPreguntas.Add(pregunta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPregunta), new { id = pregunta.Id }, pregunta);
        }

        // PUT: api/QuizPreguntas/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> ActualizarPregunta(int id, QuizPregunta pregunta)
        {
            if (id != pregunta.Id)
                return BadRequest("El id de la URL no coincide con el del cuerpo.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existe = await _context.QuizPreguntas.AnyAsync(p => p.Id == id);
            if (!existe)
                return NotFound();

            _context.Entry(pregunta).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/QuizPreguntas/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> EliminarPregunta(int id)
        {
            var pregunta = await _context.QuizPreguntas.FindAsync(id);
            if (pregunta == null)
                return NotFound();

            _context.QuizPreguntas.Remove(pregunta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}