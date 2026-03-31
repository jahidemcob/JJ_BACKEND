using Auth.Domain.Entities;

namespace Auth.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByUsernameAsync(string username);
        Task<Usuario?> GetByIdAsync(int idUsuario);
        Task<bool> UsuarioExistsAsync(string usuario);
        Task<Usuario> CreateAsync(Usuario usuario);
    }
}