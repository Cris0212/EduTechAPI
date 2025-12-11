using EduTechApi.Models;
using EduTechAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace EduTechPlus.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // =====================
        //  Tablas principales
        // =====================

        public DbSet<Rol> Roles { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Profesor> Profesores { get; set; } = null!;
        public DbSet<Alumno> Alumnos { get; set; } = null!;
        public DbSet<Materia> Materias { get; set; } = null!;

        // Relaciones muchos-a-muchos
        public DbSet<ProfesorMateria> ProfesorMaterias { get; set; } = null!;
       
        // Notas
        public DbSet<Nota> Notas { get; set; } = null!;

        // Quizzes
        public DbSet<Quiz> Quizzes { get; set; } = null!;
        public DbSet<QuizPregunta> QuizPreguntas { get; set; } = null!;
       
        // Materiales de clase
        public DbSet<Material> Materiales { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ====== Roles ======
            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Roles");
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Nombre)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.HasIndex(r => r.Nombre)
                      .IsUnique();
            });

            // ====== Usuarios ======
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios");
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Nombre)
                      .HasMaxLength(150)
                      .IsRequired();

                entity.Property(u => u.Correo)
                      .HasMaxLength(150)
                      .IsRequired();

                entity.HasIndex(u => u.Correo)
                      .IsUnique();

                entity.HasOne(u => u.Rol)
                      .WithMany(r => r.Usuarios)
                      .HasForeignKey(u => u.RolId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ====== Profesores ======
            modelBuilder.Entity<Profesor>(entity =>
            {
                entity.ToTable("Profesores");
                entity.HasKey(p => p.Id);

                entity.HasOne(p => p.Usuario)
                      .WithOne()
                      .HasForeignKey<Profesor>(p => p.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(p => p.Colegio)
                      .HasMaxLength(150)
                      .IsRequired();

                entity.Property(p => p.Turno)
                      .HasMaxLength(50)
                      .IsRequired();
            });

            // ====== Alumnos ======
            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.ToTable("Alumnos");
                entity.HasKey(a => a.Id);

                entity.HasOne(a => a.Usuario)
                      .WithOne()
                      .HasForeignKey<Alumno>(a => a.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ====== Materias ======
            modelBuilder.Entity<Materia>(entity =>
            {
                entity.ToTable("Materias");
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Nombre)
                      .HasMaxLength(150)
                      .IsRequired();

                entity.HasIndex(m => m.Nombre)
                      .IsUnique();
            });

            // ====== Grupos ======
            modelBuilder.Entity<Grupo>(entity =>
            {
                entity.ToTable("Grupos");
                entity.HasKey(g => g.Id);

                entity.Property(g => g.Nombre)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.HasIndex(g => g.Nombre)
                      .IsUnique();
            });

            // ====== ProfesorMaterias (muchos a muchos) ======
            modelBuilder.Entity<ProfesorMateria>(entity =>
            {
                entity.ToTable("ProfesorMaterias");
                entity.HasKey(pm => pm.Id);

                entity.HasOne(pm => pm.Profesor)
                      .WithMany(p => p.ProfesorMaterias)
                      .HasForeignKey(pm => pm.ProfesorId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(pm => pm.Materia)
                      .WithMany(m => m.ProfesorMaterias)
                      .HasForeignKey(pm => pm.MateriaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ====== ProfesorGrupos (muchos a muchos) ======
            modelBuilder.Entity<ProfesorGrupo>(entity =>
            {
                entity.ToTable("ProfesorGrupos");
                entity.HasKey(pg => pg.Id);

                entity.HasOne(pg => pg.Profesor)
                      .WithMany(p => p.ProfesorGrupos)
                      .HasForeignKey(pg => pg.ProfesorId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(pg => pg.Grupo)
                      .WithMany(g => g.ProfesorGrupos)
                      .HasForeignKey(pg => pg.GrupoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ====== AlumnoGrupos (muchos a muchos) ======
            modelBuilder.Entity<AlumnoGrupo>(entity =>
            {
                entity.ToTable("AlumnoGrupos");
                entity.HasKey(ag => ag.Id);

                entity.HasOne(ag => ag.Alumno)
                      .WithMany(a => a.AlumnoGrupos)
                      .HasForeignKey(ag => ag.AlumnoId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ag => ag.Grupo)
                      .WithMany(g => g.AlumnoGrupos)
                      .HasForeignKey(ag => ag.GrupoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ====== Notas ======
            modelBuilder.Entity<Nota>(entity =>
            {
                entity.ToTable("Notas");
                entity.HasKey(n => n.Id);

                entity.Property(n => n.Trimestre)
                      .IsRequired();

                entity.HasOne(n => n.Alumno)
                      .WithMany(a => a.Notas)
                      .HasForeignKey(n => n.AlumnoId);

                entity.HasOne(n => n.Profesor)
                      .WithMany(p => p.Notas)
                      .HasForeignKey(n => n.ProfesorId);

                entity.HasOne(n => n.Materia)
                      .WithMany(m => m.Notas)
                      .HasForeignKey(n => n.MateriaId);

                entity.HasOne(n => n.Grupo)
                      .WithMany(g => g.Notas)
                      .HasForeignKey(n => n.GrupoId);
            });

            // ====== Quizzes ======
            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.ToTable("Quizzes");
                entity.HasKey(q => q.Id);

                entity.Property(q => q.Titulo)
                      .HasMaxLength(200)
                      .IsRequired();

                entity.Property(q => q.Descripcion)
                      .HasMaxLength(1000);

                entity.HasOne(q => q.Profesor)
                      .WithMany(p => p.Quizzes)
                      .HasForeignKey(q => q.ProfesorId);

                entity.HasOne(q => q.Materia)
                      .WithMany(m => m.Quizzes)
                      .HasForeignKey(q => q.MateriaId);

                entity.HasOne(q => q.Grupo)
                      .WithMany(g => g.Quizzes)
                      .HasForeignKey(q => q.GrupoId);
            });

            // ====== QuizPreguntas ======
            modelBuilder.Entity<QuizPregunta>(entity =>
            {
                entity.ToTable("QuizPreguntas");
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Texto)
                      .HasMaxLength(1000)
                      .IsRequired();

                entity.Property(p => p.Tipo)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.HasOne(p => p.Quiz)
                      .WithMany(q => q.Preguntas)
                      .HasForeignKey(p => p.QuizId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ====== QuizResultados ======
            modelBuilder.Entity<QuizResultado>(entity =>
            {
                entity.ToTable("QuizResultados");
                entity.HasKey(r => r.Id);

                entity.HasOne(r => r.Quiz)
                      .WithMany(q => q.Resultados)
                      .HasForeignKey(r => r.QuizId);

                entity.HasOne(r => r.Alumno)
                      .WithMany(a => a.QuizResultados)
                      .HasForeignKey(r => r.AlumnoId);
            });

            // ====== Materiales ======
            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("Materiales");
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Titulo)
                      .HasMaxLength(150)
                      .IsRequired();

                entity.HasOne(m => m.Profesor)
                      .WithMany(p => p.Materiales)
                      .HasForeignKey(m => m.ProfesorId);

                entity.HasOne(m => m.Materia)
                      .WithMany(mat => mat.Materiales)
                      .HasForeignKey(m => m.MateriaId);

                entity.HasOne(m => m.Grupo)
                      .WithMany(g => g.Materiales)
                      .HasForeignKey(m => m.GrupoId);
            });
        }
    }
}