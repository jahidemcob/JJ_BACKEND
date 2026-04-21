using Backend.src.app.Features.Services.application.DTOs;
using Backend.src.app.Features.Services.application.exceptions;
using Backend.src.app.Features.Services.domain.repositories;

namespace Backend.src.app.Features.Services.application.usecases
{
    public class GetServiceByIdUseCase
    {
        private readonly IServicesRepository _repository;

        public GetServiceByIdUseCase(IServicesRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceListDto> Execute(int id)
        {
            // VALIDACIÓN
            if (id <= 0)
                throw new ServiceValidationException("El ID proporcionado no es válido.");

            // BUSCAR SERVICIO
            var service = await _repository.GetServiceById(id);

            if (service == null)
                throw new ServiceNotFoundException(id);

            // MAPEO Domain → DTO
            var dto = new ServiceListDto
            {
                IdServicio = service.idServicio,
                NombreServicio = service.nombreServicio,
                Descripcion = service.descripcion,
                PrecioBase = service.precioBase,
                IsActive = service.Activo
            };

            return dto;
        }
    }
}