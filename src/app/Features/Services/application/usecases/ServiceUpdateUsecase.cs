using Backend.src.app.Features.Services.application.DTOs;
using Backend.src.app.Features.Services.application.exceptions;
using Backend.src.app.Features.Services.application.mappers;
using Backend.src.app.Features.Services.domain.entities;
using Backend.src.app.Features.Services.domain.repositories;

namespace Backend.src.app.Features.Services.application.usecases
{
    public class UpdateServiceUseCase
    {
        private readonly IServicesRepository _repository;

        public UpdateServiceUseCase(IServicesRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponseDto> Execute(ServiceUpdateDto dto)
        {
            if (dto.IdServicio <= 0)
                throw new ServiceValidationException("El ID del servicio es inválido.");

            if (string.IsNullOrWhiteSpace(dto.NombreServicio))
                throw new ServiceValidationException("El nombre del servicio es obligatorio.");

            if (string.IsNullOrWhiteSpace(dto.Descripcion))
                throw new ServiceValidationException("La descripción es obligatoria.");

            if (dto.PrecioBase < 0)
                throw new ServiceValidationException("El precio no puede ser negativo.");

            var existing = await _repository.GetServiceById(dto.IdServicio);
            if (existing == null)
                throw new ServiceNotFoundException(dto.IdServicio);

            var all = await _repository.GetAllServicesAsync();
            if (all.Any(s =>
                    s.idServicio != dto.IdServicio &&
                    string.Equals(s.nombreServicio, dto.NombreServicio, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ServiceAlreadyExistsException(dto.NombreServicio);
            }

            // Actualizar entidad existente
            existing.nombreServicio = dto.NombreServicio;
            existing.descripcion = dto.Descripcion;
            existing.precioBase = dto.PrecioBase;

            var success = await _repository.UpdateServiceAsync(existing);

            if (!success)
                throw new ServiceNotUpdatedException(dto.IdServicio);

            return ServiceMapper.ToDto(existing);
        }
    }
}