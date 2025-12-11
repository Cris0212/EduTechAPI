using EduTechPlus.Api.Context;
using EduTechPlus.DTOs;
using EduTechPlusAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduTechPlus.Api.Controllers
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

        // POST: api/QuizPreguntas
        // Crea una nueva pregunta para un quiz
        [HttpPost]
        public async Task<ActionResult<QuizPreguntaListaDto>> CrearPregunta([FromBody] QuizPreguntaCrearDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validar que exista el quiz
            var quizExiste = await _context.Quizzes.AnyAsync(q => q.Id == dto.QuizId);
            if (!quizExiste)
                return BadRequest($"No existe un quiz con Id {dto.QuizId}");

            var pregunta = new QuizPregunta
            {
                QuizId = dto.QuizId,
                Texto = dto.Texto,
                Tipo = dto.Tipo,
                OpcionesJson = dto.OpcionesJson,
                RespuestaCorrecta = dto.RespuestaCorrecta,
                Orden = dto.Orden
            };

            _context.QuizPreguntas.Add(pregunta);
            await _context.SaveChangesAsync();

            var result = new QuizPreguntaListaDto
            {
                Id = pregunta.Id,
                QuizId = pregunta.QuizId,
                Texto = pregunta.Texto,
                Tipo = pregunta.Tipo,
                OpcionesJson = pregunta.OpcionesJson,
                Orden = pregunta.Orden
            };

            return CreatedAtAction(nameof(ObtenerPreguntasPorQuiz), new { quizId = pregunta.QuizId }, result);
        }

        // GET: api/QuizPreguntas?quizId=5
        // Lista las preguntas de un quiz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizPreguntaListaDto>>> ObtenerPreguntasPorQuiz([FromQuery] int quizId)
        {
            var quizExiste = await _context.Quizzes.AnyAsync(q => q.Id == quizId);
            if (!quizExiste)
                return NotFound($"No existe un quiz con Id {quizId}");

            var preguntas = await _context.QuizPreguntas
                .AsNoTracking()
                .Where(p => p.QuizId == quizId)
                .OrderBy(p => p.Orden)
                .Select(p => new QuizPreguntaListaDto
                {
                    Id = p.Id,
                    QuizId = p.QuizId,
                    Texto = p.Texto,
                    Tipo = p.Tipo,
                    OpcionesJson = p.OpcionesJson,
                    Orden = p.Orden
                })
                .ToListAsync();

            return Ok(preguntas);
        }

        // DELETE: api/QuizPreguntas/10
        // Elimina una pregunta específica
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> EliminarPregunta(int id)
        {
            var pregunta = await _context.QuizPreguntas.FirstOrDefaultAsync(p => p.Id == id);

            if (pregunta == null)
                return NotFound();

            _context.QuizPreguntas.Remove(pregunta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}