using Backend.src.app.Features.Services.application.DTOs;
using Backend.src.app.Features.Services.domain.repositories;

namespace Backend.src.app.Features.Services.application.usecases
{
    public class GetAllServicesUseCase
    {
        private readonly IServicesRepository _repository;

        public GetAllServicesUseCase(IServicesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ServiceListDto>> Execute()
        {
            var services = await _repository.GetAllServicesAsync();

            // MAPEO: Domain → DTO
            var result = services.Select(s => new ServiceListDto
            {
                IdServicio = s.idServicio,
                NombreServicio = s.nombreServicio,
                Descripcion = s.descripcion,
                PrecioBase = s.precioBase,
                IsActive = s.Activo
            });

            return result;
        }
    }
}