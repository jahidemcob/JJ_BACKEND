using Backend.src.app.auth.application.UseCases;
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

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Usuarios
                .ToListAsync(); 
        }
        public async Task<IEnumerable<User>> GetActiveUsersAsync()
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

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.IdUsuario == id);
        }

        public async Task<User?> GetByUsernameAsync(string nombreUsuario)
        {
            return await _context.Usuarios
                .Where(u => EF.Functions.Collate(u.NombreUsuario!, "Latin1_General_CS_AS")
                            == nombreUsuario)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetByEmailAsync(string correo)
        {
            correo = correo.Trim().ToLower();

            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo);
        }

        public async Task CreateAsync(User usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(User usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(User usuario)
        {
            usuario.Activo = false; 
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }
    }
}