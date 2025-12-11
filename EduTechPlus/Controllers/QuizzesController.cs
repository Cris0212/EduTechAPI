using EduTechApi.Context;
using EduTechPlus.Context;
using EduTechPlus.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduTechPlus.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizzesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuizzesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Quizzes?profesorId=1&grupoId=2&materiaId=3
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes(
            [FromQuery] int? profesorId,
            [FromQuery] int? grupoId,
            [FromQuery] int? materiaId)
        {
            var query = _context.Quizzes.AsNoTracking().AsQueryable();

            if (profesorId.HasValue)
                query = query.Where(q => q.ProfesorId == profesorId.Value);

            if (grupoId.HasValue)
                query = query.Where(q => q.GrupoId == grupoId.Value);

            if (materiaId.HasValue)
                query = query.Where(q => q.MateriaId == materiaId.Value);

            var lista = await query
                .OrderByDescending(q => q.FechaCreacion)
                .ToListAsync();

            return Ok(lista);
        }

        // GET: api/Quizzes/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Quiz>> GetQuiz(int id)
        {
            var quiz = await _context.Quizzes
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == id);

            if (quiz == null)
                return NotFound();

            return Ok(quiz);
        }

        // POST: api/Quizzes
        [HttpPost]
        public async Task<ActionResult<Quiz>> CrearQuiz(Quiz quiz)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            quiz.Titulo = quiz.Titulo?.Trim() ?? string.Empty;
            quiz.Descripcion = string.IsNullOrWhiteSpace(quiz.Descripcion)
                ? null
                : quiz.Descripcion.Trim();
            quiz.FechaCreacion = DateTime.UtcNow;

            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQuiz), new { id = quiz.Id }, quiz);
        }

        // PUT: api/Quizzes/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> ActualizarQuiz(int id, Quiz quiz)
        {
            if (id != quiz.Id)
                return BadRequest("El id de la URL no coincide con el del cuerpo.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existe = await _context.Quizzes.AnyAsync(q => q.Id == id);
            if (!existe)
                return NotFound();

            _context.Entry(quiz).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Quizzes/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> EliminarQuiz(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
                return NotFound();

            // Opcional: borrar también las preguntas asociadas
            var preguntas = _context.QuizPreguntas.Where(p => p.QuizId == id);
            _context.QuizPreguntas.RemoveRange(preguntas);

            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}