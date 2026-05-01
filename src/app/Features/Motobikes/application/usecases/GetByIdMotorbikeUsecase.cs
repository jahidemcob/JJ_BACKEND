using Backend.src.app.Features.Motobikes.application.DTOs;
using Backend.src.app.Features.Motobikes.application.exceptions;
using Backend.src.app.Features.Motobikes.domain.repository;
using Backend.src.app.Features.Motobikes.application.mappers;


namespace Backend.src.app.Features.Motobikes.application.usecases
{
    public class GetByIdMotorbikeUsecase
    {
        private readonly IMotorbikesRepository _MotorbikesRepository;

        public GetByIdMotorbikeUsecase(IMotorbikesRepository motorbikesRepository)
        {
            _MotorbikesRepository = motorbikesRepository;
        }

        public async Task<MotorbikeWithUserResponseDto?> Execute (int idMoto)
        {
            var result = await _MotorbikesRepository.GetMotorbikeWithUserByIdAsync(idMoto);

            if (!result.HasValue)
                throw new MotorbikeValidationException($"La moto con ID: {idMoto} no existe.");

            return MotorbikeMapper.ToDtoWithUser(result.Value.moto, result.Value.nombreUsuario);

        }
    }
}
