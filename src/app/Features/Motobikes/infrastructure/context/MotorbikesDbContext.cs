using Microsoft.EntityFrameworkCore;
using Backend.src.app.Features.Motobikes.domain.entities;

namespace Backend.src.app.Features.Motobikes.infrastructure.Context
{
    public class MotobikesDbContext : DbContext
    {
        public MotobikesDbContext(DbContextOptions<MotobikesDbContext> options)
            : base(options) { }

        public DbSet<Motorbike> Motos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Motorbike>(entity =>
            {
                entity.ToTable("Motos");

                entity.HasKey(m => m.idMoto);

                entity.Property(m => m.marca)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(m => m.modelo)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(m => m.placa)
                      .IsRequired()
                      .HasMaxLength(10);

                entity.Property(m => m.cilindraje)
                      .IsRequired();

                entity.Property(m => m.anio)
                      .IsRequired();

                entity.Property(m => m.Activo)
                      .HasDefaultValue(true);

                // Mantener FK sin dependencias
                entity.Property(m => m.idUsuario)
                      .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}