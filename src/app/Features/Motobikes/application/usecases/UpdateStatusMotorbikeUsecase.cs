using Backend.src.app.Features.Motobikes.application.exceptions;
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


        public async Task <bool> Execute(int idMoto)
        {

        if (idMoto <= 0)
            throw new MotorbikeValidationException($"EL ID: {idMoto} no es válido.");


        var ExistingMotorbike = await _MotorbikesRepository.GetMotorbikeByIdAsync(idMoto);

        if (ExistingMotorbike == null)
                throw new MotorbikeNotFoundException(idMoto);

        var newStatus = !ExistingMotorbike.Activo;
        
        var UpdatedStatus  = await _MotorbikesRepository.UpdateMotorbikeStatusAsync(idMoto, newStatus);

        return UpdatedStatus;
        }
    }
}
