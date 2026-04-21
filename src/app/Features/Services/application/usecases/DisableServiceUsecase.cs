using Backend.src.app.Features.Services.application.exceptions;
using Backend.src.app.Features.Services.domain.repositories;

namespace Backend.src.app.Features.Services.application.usecases
{
    public class DisableServiceUseCase
    {
        private readonly IServicesRepository _repository;

        public DisableServiceUseCase(IServicesRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Execute(int id)
        {
            // 1. VALIDAR ID
            if (id <= 0)
                throw new ServiceValidationException("El ID proporcionado no es válido.");

            // 2. VERIFICAR QUE EXISTE
            var existing = await _repository.GetServiceById(id);

            if (existing == null)
                throw new ServiceNotFoundException(id);

            // 3. VALIDAR SI YA ESTÁ DESACTIVADO
            if (!existing.Activo)
                throw new ServiceValidationException("El servicio ya se encuentra desactivado.");

            // 4. DESACTIVAR
            var result = await _repository.DisableServiceAsync(id);

            if (!result)
                throw new Exception("No se pudo desactivar el servicio.");

            return true;
        }
    }
}