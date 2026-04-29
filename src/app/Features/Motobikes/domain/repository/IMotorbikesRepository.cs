using Backend.src.app.Features.Motobikes.domain.entities;


namespace Backend.src.app.Features.Motobikes.domain.repository
{
    public interface IMotorbikesRepository
    {
        Task<Motorbike?> GetMotorbikeByIdAsync(int id);
        Task<IEnumerable<Motorbike>> GetAllMotorbikesAsync();
        Task<Motorbike> CreateMotorbikeAsync(Motorbike motorbike);
        Task<Motorbike> UpdateMotorbikeAsync(Motorbike motorbike);
        Task<bool> UpdateMotorbikeStatusAsync(int id, bool Activo);
        Task<bool> ExistsByPlateAsync(string placa);
    }
}
