using Backend.src.app.Features.Services.application.DTOs;
using Backend.src.app.Features.Services.domain.repositories;
using Backend.src.app.Features.Services.application.mappers;

namespace Backend.src.app.Features.Services.application.usecases
{
    public class GetAllServicesUseCase
    {
        private readonly IServicesRepository _repository;

        public GetAllServicesUseCase(IServicesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ServiceResponseDto>> Execute()
        {
            var services = await _repository.GetAllServicesAsync();

            return ServiceMapper.ToDtoList(services);
        }
    }
}