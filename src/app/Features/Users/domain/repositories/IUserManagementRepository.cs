using Backend.src.app.Features.Users.domain.Entities;

namespace Backend.src.app.Features.Users.domain.repositories
{
    public interface IUserManagementRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task<Usuario?> GetByUsernameAsync(string nombreUsuario);
        Task<Usuario?> GetByEmailAsync(string correo);
        Task CreateAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(Usuario usuario);
    }
}