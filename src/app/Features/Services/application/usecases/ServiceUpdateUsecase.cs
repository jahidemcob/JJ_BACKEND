using Backend.src.app.Features.Services.application.DTOs;
using Backend.src.app.Features.Services.application.exceptions;
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

        public async Task<bool> Execute(ServiceUpdateDto dto)
        {
            // VALIDACIONES BÁSICAS
            if (dto.IdServicio <= 0)
                throw new ServiceValidationException("El ID del servicio es inválido.");

            if (string.IsNullOrWhiteSpace(dto.NombreServicio))
                throw new ServiceValidationException("El nombre del servicio es obligatorio.");

            if (string.IsNullOrWhiteSpace(dto.Descripcion))
                throw new ServiceValidationException("La descripción es obligatoria.");

            if (dto.PrecioBase < 0)
                throw new ServiceValidationException("El precio no puede ser negativo.");

            // VERIFICAR QUE EXISTE
            var existing = await _repository.GetServiceById(dto.IdServicio);
            if (existing == null)
                throw new ServiceNotFoundException(dto.IdServicio);

            // VALIDAR DUPLICADO 
            var all = await _repository.GetAllServicesAsync();
            if (all.Any(s =>
                    s.idServicio != dto.IdServicio &&
                    s.nombreServicio.ToLower() == dto.NombreServicio.ToLower()))
            {
                throw new ServiceAlreadyExistsException(dto.NombreServicio);
            }

            // MAPEO A ENTIDAD DOMAIN
            var updatedService = new Service
            {
                idServicio = dto.IdServicio,
                nombreServicio = dto.NombreServicio,
                descripcion = dto.Descripcion,
                precioBase = dto.PrecioBase,
                Activo = existing.Activo // mantener estado
            };

            // ACTUALIZAR 
            var success = await _repository.UpdateServiceAsync(updatedService);

            if (!success)
                throw new Exception("No se pudo actualizar el servicio.");

            return true;
        }
    }
}