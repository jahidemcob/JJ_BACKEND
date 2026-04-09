using Backend.src.app.auth.domain.entities;

namespace Backend.src.app.auth.domain.repositories
{
    public interface IRolRepository
    {
        Task<Rol?> GetByIdAsync(int idRol);
    }
}