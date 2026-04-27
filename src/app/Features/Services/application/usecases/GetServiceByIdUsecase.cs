using Backend.src.app.Features.Services.application.DTOs;
using Backend.src.app.Features.Services.application.exceptions;
using Backend.src.app.Features.Services.domain.repositories;
using Backend.src.app.Features.Services.application.mappers;

namespace Backend.src.app.Features.Services.application.usecases
{
    public class GetServiceByIdUseCase
    {
        private readonly IServicesRepository _repository;

        public GetServiceByIdUseCase(IServicesRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponseDto> Execute(int id)
        {
            // VALIDACIÓN
            if (id <= 0)
                throw new ServiceValidationException("El ID proporcionado no es válido.");

            // BUSCAR SERVICIO
            var service = await _repository.GetServiceById(id);

            if (service == null)
                throw new ServiceNotFoundException(id);

            return ServiceMapper.ToDto(service);
        }
    }
}