using Backend.src.app.Features.Users.domain.Entities;

namespace Backend.src.app.Features.Users.domain.repositories
{
    public interface IUserManagementRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<bool> ActiveUser(int idUsuario);
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string nombreUsuario);
        Task<User?> GetByEmailAsync(string correo);
        Task CreateAsync(User usuario);
        Task<bool> UpdateAsync(User usuario);
        Task DeleteAsync(User usuario);
    }
}