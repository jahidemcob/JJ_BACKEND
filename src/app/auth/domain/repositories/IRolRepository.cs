using Auth.Domain.Entities;

namespace Auth.Domain.Repositories
{
    public interface IRolRepository
    {
        Task<Rol?> GetByIdAsync(int idRol);
    }
}