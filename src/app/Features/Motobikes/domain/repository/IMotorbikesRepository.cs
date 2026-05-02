using Backend.src.app.Features.Motobikes.domain.entities;

namespace Backend.src.app.Features.Motobikes.domain.repository
{
    public interface IMotorbikesRepository
    {
        Task<(Motorbike moto, string nombreUsuario)?> GetMotorbikeWithUserByIdAsync(int id);

        Task<IEnumerable<(Motorbike moto, string nombreUsuario)>> GetAllMotorbikesWithUserAsync();

        Task<IEnumerable<(Motorbike moto, string nombreUsuario)>>
            GetMotorbikesWithUserByUserIdAsync(int userId);

        Task<Motorbike?> GetMotorbikeByIdAsync(int id);

        Task<Motorbike> CreateMotorbikeAsync(Motorbike motorbike);

        Task<Motorbike> UpdateMotorbikeAsync(Motorbike motorbike);

        Task<bool> UpdateMotorbikeStatusAsync(int idMoto, bool Activo);

        Task<bool> ExistsByPlateAsync(string placa);
    }
}