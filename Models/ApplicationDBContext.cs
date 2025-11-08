using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColegioSanJose.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Expediente> Expedientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar Alumno
            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.HasKey(e => e.AlumnoId);
                entity.Property(e => e.AlumnoId)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.FechaNacimiento)
                    .IsRequired()
                    .HasColumnType("date");

                entity.Property(e => e.Grado)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar(50)");
            });

            // Configurar Materia
            modelBuilder.Entity<Materia>(entity =>
            {
                entity.HasKey(e => e.MateriaId);
                entity.Property(e => e.MateriaId)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.NombreMateria)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Docente)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("varchar(100)");
            });

            // Configurar Expediente
            modelBuilder.Entity<Expediente>(entity =>
            {
                entity.HasKey(e => e.ExpedienteId);
                entity.Property(e => e.ExpedienteId)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.NotaFinal)
                    .IsRequired()
                    .HasColumnType("decimal(3,1)");

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(500)
                    .HasColumnType("varchar(500)");

                // Configurar relaciones
                entity.HasOne(e => e.Alumno)
                    .WithMany(a => a.Expedientes)
                    .HasForeignKey(e => e.AlumnoId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Materia)
                    .WithMany(m => m.Expedientes)
                    .HasForeignKey(e => e.MateriaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}