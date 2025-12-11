using EduTechAPI.Models;
using EduTechPlusApi.Models;
using EduTechPlusAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EduTechPlus.Api.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // ========== TABLAS PRINCIPALES ==========

        public DbSet<usuario> usuarios { get; set; } = null!;
        public DbSet<rol> roles { get; set; } = null!;

        public DbSet<profesor> Profesores { get; set; } = null!;
        public DbSet<alumno> Alumnos { get; set; } = null!;

        public DbSet<materia> Materias { get; set; } = null!;
        public DbSet<grupo> Grupos { get; set; } = null!;

        public DbSet<Quiz> Quizzes { get; set; } = null!;
        public DbSet<QuizPregunta> QuizPreguntas { get; set; } = null!;
        public DbSet<QuizResultado> QuizResultados { get; set; } = null!;

        public DbSet<material> Materiales { get; set; } = null!;
        public DbSet<Nota> Notas { get; set; } = null!;

        // ========== CONFIGURACIÓN SENCILLA ==========
        // Sin Fluent API complicada. EF usa convenciones.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Si mañana el profe pregunta:
            // "¿Cómo se configuran relaciones?"
            // puedes decir que se usan convenciones,
            // y que se puede extender aquí con Fluent API.
        }
    }
}