using Backend.src.app.Features.Services.domain.entities;

namespace Backend.src.app.Features.Services.domain.repositories
{
    public interface IServicesRepository
    {
        Task <Service?> GetServiceById (int id);
        Task <IEnumerable<Service>> GetAllServicesAsync();
        Task <int> CreateServiceAsync (Service service);
        Task <bool> UpdateServiceAsync (Service service);
        Task <bool> UpdateServiceStatusAsync (int id, bool Activo);  

    }
}
