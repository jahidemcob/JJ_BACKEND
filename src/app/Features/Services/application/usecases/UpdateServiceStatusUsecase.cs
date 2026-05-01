using Backend.src.app.Features.Services.application.exceptions;
using Backend.src.app.Features.Services.domain.repositories;

namespace Backend.src.app.Features.Services.application.usecases
{
    public class UpdateServiceStatusUsecase
    {
        private readonly IServicesRepository _repository;

        public UpdateServiceStatusUsecase(IServicesRepository repository)
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

            // 3. cambiar estado
            var newStatus = !existing.Activo;

            var result = await _repository.UpdateServiceStatusAsync(id, newStatus);

            if (!result)
                throw new Exception("No se pudo cambiar el estado del servicio.");

            return newStatus;
        }
    }
}