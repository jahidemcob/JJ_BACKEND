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

        public async Task<IEnumerable<MotorbikeWithUserResponseDto>> Execute()
        { 
            var Result = await _MotorbikesRepository.GetAllMotorbikesWithUserAsync();

            return MotorbikeMapper.ToDtoWithUser(Result);
        }
         
    }
}
