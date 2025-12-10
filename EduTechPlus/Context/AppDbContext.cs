using EduTechApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EduTechApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Alumno> Alumnos => Set<Alumno>();
        public DbSet<Profesor> Profesores => Set<Profesor>();
        public DbSet<Materia> Materias => Set<Materia>();
        public DbSet<ProfesorMateria> ProfesoresMaterias => Set<ProfesorMateria>();
        public DbSet<AlumnoMateria> AlumnosMaterias => Set<AlumnoMateria>();
        public DbSet<Nota> Notas => Set<Nota>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Correo)
                .IsUnique();

            modelBuilder.Entity<Alumno>()
                .HasOne(a => a.Usuario)
                .WithOne(u => u.Alumno)
                .HasForeignKey<Alumno>(a => a.UsuarioId);

            modelBuilder.Entity<Profesor>()
                .HasOne(p => p.Usuario)
                .WithOne(u => u.Profesor)
                .HasForeignKey<Profesor>(p => p.UsuarioId);

            modelBuilder.Entity<ProfesorMateria>()
                .HasKey(pm => new { pm.ProfesorId, pm.MateriaId });

            modelBuilder.Entity<ProfesorMateria>()
                .HasOne(pm => pm.Profesor)
                .WithMany(p => p.ProfesorMaterias)
                .HasForeignKey(pm => pm.ProfesorId);

            modelBuilder.Entity<ProfesorMateria>()
                .HasOne(pm => pm.Materia)
                .WithMany(m => m.ProfesorMaterias)
                .HasForeignKey(pm => pm.MateriaId);

            modelBuilder.Entity<AlumnoMateria>()
                .HasKey(am => new { am.AlumnoId, am.ProfesorId, am.MateriaId });

            modelBuilder.Entity<AlumnoMateria>()
                .HasOne(am => am.Alumno)
                .WithMany(a => a.AlumnoMaterias)
                .HasForeignKey(am => am.AlumnoId);

            modelBuilder.Entity<AlumnoMateria>()
                .HasOne(am => am.Profesor)
                .WithMany(p => p.AlumnoMaterias)
                .HasForeignKey(am => am.ProfesorId);

            modelBuilder.Entity<AlumnoMateria>()
                .HasOne(am => am.Materia)
                .WithMany(m => m.AlumnoMaterias)
                .HasForeignKey(am => am.MateriaId);

            modelBuilder.Entity<Nota>()
                .HasOne(n => n.Alumno)
                .WithMany(a => a.Notas)
                .HasForeignKey(n => n.AlumnoId);

            modelBuilder.Entity<Nota>()
                .HasOne(n => n.Profesor)
                .WithMany(p => p.NotasPuestas)
                .HasForeignKey(n => n.ProfesorId);

            modelBuilder.Entity<Nota>()
                .HasOne(n => n.Materia)
                .WithMany(m => m.Notas)
                .HasForeignKey(n => n.MateriaId);
        }
    }
}
