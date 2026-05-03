using Backend.src.app.auth.domain.entities;
using Backend.src.app.auth.domain.repositories;
using Microsoft.EntityFrameworkCore;
using Backend.src.app.auth.infrastructure.Context;

namespace Backend.src.app.auth.infrastructure.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly AuthDbContext _context;

        public RolRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<Rol?> GetByIdAsync(int idRol)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.IdRol == idRol);
        }

        public async Task<IEnumerable<Rol>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}