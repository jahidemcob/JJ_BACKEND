using Backend.src.app.Features.Motobikes.domain.entities;
using Backend.src.app.Features.Users.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.src.app.Features.Motobikes.infrastructure.Context
{
    public class MotorbikesDbContext : DbContext
    {
        public MotorbikesDbContext(DbContextOptions<MotorbikesDbContext> options)
            : base(options) { }

        public DbSet<Motorbike> Motorbikes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tabla Motos
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

                // Relación con usuario  
                entity.HasOne<User>()
                      .WithMany()
                      .HasForeignKey(m => m.idUsuario);
            });

            // Tabla Usuarios (solo mapping, no la configuras completa aquí)
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Usuarios");
                entity.HasKey(u => u.IdUsuario);
            });
        }
    }
}