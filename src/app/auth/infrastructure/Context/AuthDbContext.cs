using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Context
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
    }
}