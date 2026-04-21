using Microsoft.EntityFrameworkCore;
using Backend.src.app.Features.Services.domain.entities;

namespace Backend.src.app.Features.Services.infrastructure.Context
{
    public class ServicesDbContext : DbContext
    {
        public ServicesDbContext(DbContextOptions<ServicesDbContext> options)
            : base(options) { }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Servicios");

                entity.HasKey(s => s.idServicio);

                entity.Property(s => s.nombreServicio)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(s => s.descripcion)
                      .HasMaxLength(300);

                entity.Property(s => s.precioBase)
                      .HasColumnType("decimal(10,2)")
                      .IsRequired();

                entity.Property(s => s.Activo)
                      .HasDefaultValue(true);
            });
        }
    }
}