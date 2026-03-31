using Auth.Domain.Entities;
using Auth.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Auth.Infrastructure.Context;

namespace Auth.Infrastructure.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly AuthDbContext _context;

        public RolRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<Rol?> GetByIdAsync(int id)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.IdRol == id);
        }
    }
}