using Backend.src.app.auth.domain.entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.src.app.auth.infrastructure.Context
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options) { }
        public DbSet<Rol> Roles { get; set; }
    }
}