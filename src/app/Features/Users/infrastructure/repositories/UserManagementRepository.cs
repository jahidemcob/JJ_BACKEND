using Backend.src.app.Features.Users.domain.Entities;
using Backend.src.app.Features.Users.domain.repositories;
using Backend.src.app.Features.Users.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Backend.src.app.Features.Users.infrastructure.Repositories
{
    public class UserManagementRepository : IUserManagementRepository
    {
        private readonly UsersDbContext _context;

        public UserManagementRepository(UsersDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios
                .ToListAsync(); 
        }
        public async Task<IEnumerable<Usuario>> GetActiveUsersAsync()
        {
            return await _context.Usuarios
                .Where(u => u.Activo)
                .ToListAsync();
        }

        public async Task<bool> ActiveUser(int idUsuario)
        {
            return await _context.Usuarios
                .Where(u => u.IdUsuario == idUsuario)
                .Select(u => u.Activo)
                .FirstOrDefaultAsync();
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.IdUsuario == id);
        }

        public async Task<Usuario?> GetByUsernameAsync(string nombreUsuario)
        {
            return await _context.Usuarios
                .Where(u => EF.Functions.Collate(u.NombreUsuario!, "Latin1_General_CS_AS")
                            == nombreUsuario)
                .FirstOrDefaultAsync();
        }

        public async Task<Usuario?> GetByEmailAsync(string correo)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo);
        }

        public async Task CreateAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Usuario usuario)
        {
            usuario.Activo = false; // Soft delete
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }
    }
}