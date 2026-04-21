using Microsoft.EntityFrameworkCore;
using Backend.src.app.Features.Users.domain.Entities;

namespace Backend.src.app.Features.Users.infrastructure.Context
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.IdUsuario);

                entity.Property(u => u.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.NombreUsuario)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.Correo)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(u => u.Activo)
                    .HasDefaultValue(true);

            });
        }
    }
}