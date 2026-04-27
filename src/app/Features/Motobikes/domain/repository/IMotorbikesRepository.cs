using Backend.src.app.Features.Motobikes.domain.entities;


namespace Backend.src.app.Features.Motobikes.domain.repository
{
    public interface IMotorbikesRepository
    {
        Task<Motorbike?> GetMotorbikeById(int id);
        Task<IEnumerable<Motorbike>> GetAllMotorbikesAsync();
        Task<int> CreateMotorbikeAsync(Motorbike motorbike);
        Task<bool> UpdateMotorbikeAsync(Motorbike motorbike);
        Task<bool> UpdateMotorbikeStatusAsync(int id, bool Activo);
    }
}
