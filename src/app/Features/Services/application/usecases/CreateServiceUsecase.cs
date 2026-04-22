using Backend.src.app.Features.Services.application.DTOs;
using Backend.src.app.Features.Services.application.exceptions;
using Backend.src.app.Features.Services.domain.entities;
using Backend.src.app.Features.Services.domain.repositories;

namespace Backend.src.app.Features.Services.application.usecases
{
    public class CreateServiceUseCase
    {
        private readonly IServicesRepository _repository;

        public CreateServiceUseCase(IServicesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Service> Execute(ServiceCreateDto dto)  
        {
            // VALIDACIONES
            if (string.IsNullOrWhiteSpace(dto.NombreServicio))
                throw new ServiceValidationException("El nombre del servicio es obligatorio.");

            if (string.IsNullOrWhiteSpace(dto.Descripcion))
                throw new ServiceValidationException("La descripción es obligatoria.");

            if (dto.PrecioBase < 0)
                throw new ServiceValidationException("El precio no puede ser negativo.");

            // VALIDAR DUPLICADOS
            var existentes = await _repository.GetAllServicesAsync();
            if (existentes.Any(s => s.nombreServicio.ToLower() == dto.NombreServicio.ToLower()))
                throw new ServiceAlreadyExistsException(dto.NombreServicio);

            // CONSTRUIR ENTIDAD
            var service = new Service
            {
                nombreServicio = dto.NombreServicio,
                descripcion = dto.Descripcion,
                precioBase = dto.PrecioBase,
                Activo = true
            };

            // GUARDAR
            var newId = await _repository.CreateServiceAsync(service);
            service.idServicio = newId;

            return service;
        }
    }
}