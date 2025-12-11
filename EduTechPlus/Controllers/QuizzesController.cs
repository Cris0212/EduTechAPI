using EduTechPlus.Api.Context;
using EduTechPlusAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduTechPlus.Api.Controllers
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

        // ============================
        // 1. CREAR QUIZ
        // ============================
        [HttpPost("crear")]
        public async Task<IActionResult> CrearQuiz([FromBody] Quiz quiz)
        {
            if (quiz == null)
                return BadRequest("Datos inválidos");

            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Quiz creado correctamente",
                quiz
            });
        }

        // ============================
        // 2. LISTAR TODOS LOS QUIZZES
        // ============================
        [HttpGet("todos")]
        public async Task<IActionResult> ObtenerTodos()
        {
            var quizzes = await _context.Quizzes
                                        .OrderByDescending(q => q.FechaCreacion)
                                        .ToListAsync();

            return Ok(quizzes);
        }

        // ============================
        // 3. QUIZZES POR PROFESOR
        // ============================
        [HttpGet("profesor/{profesorId}")]
        public async Task<IActionResult> ObtenerPorProfesor(int profesorId)
        {
            var quizzes = await _context.Quizzes
                                        .Where(q => q.ProfesorId == profesorId)
                                        .OrderByDescending(q => q.FechaCreacion)
                                        .ToListAsync();

            return Ok(quizzes);
        }

        // ============================
        // 4. ELIMINAR QUIZ
        // ============================
        [HttpDelete("borrar/{id}")]
        public async Task<IActionResult> BorrarQuiz(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);

            if (quiz == null)
                return NotFound("Quiz no encontrado");

            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();

            return Ok("Quiz eliminado correctamente");
        }
    }
}