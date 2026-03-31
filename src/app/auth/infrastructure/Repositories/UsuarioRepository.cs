using Auth.Domain.Entities;
using Auth.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Auth.Infrastructure.Context;

namespace Auth.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AuthDbContext _context;

        public UsuarioRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> GetByUsernameAsync(string username)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == username);
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.IdUsuario == id);
        }

        public async Task<bool> UsuarioExistsAsync(string username)
        {
            return await _context.Usuarios
                .AnyAsync(u => u.NombreUsuario == username);
        }

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
    }
}