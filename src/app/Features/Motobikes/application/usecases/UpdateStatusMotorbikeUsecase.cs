using Backend.src.app.Features.Motobikes.domain.repository;



namespace Backend.src.app.Features.Motobikes.application.usecases
{
    public class UpdateStatusMotorbikeUsecase
    {
        private readonly IMotorbikesRepository _MotorbikesRepository;
        
        public UpdateStatusMotorbikeUsecase(IMotorbikesRepository motorbikesRepository)
        {
            _MotorbikesRepository = motorbikesRepository;
        }


        public async Task <bool> Execute(int id)
        {
           
        }
    }
}
