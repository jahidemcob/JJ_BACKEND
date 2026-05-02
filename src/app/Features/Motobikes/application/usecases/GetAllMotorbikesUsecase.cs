using Backend.src.app.Features.Motobikes.application.DTOs;
using Backend.src.app.Features.Motobikes.application.mappers;
using Backend.src.app.Features.Motobikes.domain.entities;
using Backend.src.app.Features.Motobikes.domain.repository;

namespace Backend.src.app.Features.Motobikes.application.usecases
{
    public class GetAllMotorbikesUsecase
    {
        private readonly IMotorbikesRepository _MotorbikesRepository;

        public GetAllMotorbikesUsecase(IMotorbikesRepository motorbikesRepository)
        {
            _MotorbikesRepository = motorbikesRepository;
        }

        public async Task<IEnumerable<MotorbikeWithUserResponseDto>> Execute(int userId)
        {
            var result = await _MotorbikesRepository.GetAllMotorbikesWithUserAsync();

            var filtered = result
                .Where(x => x.moto.idUsuario == userId);

            return MotorbikeMapper.ToDtoWithUser(filtered);
        }
    }
}