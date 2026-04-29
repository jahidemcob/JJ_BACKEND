using Backend.src.app.Features.Motobikes.application.DTOs;
using Backend.src.app.Features.Motobikes.application.exceptions;
using Backend.src.app.Features.Motobikes.application.mappers;
using Backend.src.app.Features.Motobikes.domain.repository;


namespace Backend.src.app.Features.Motobikes.application.usecases
{
    public class UpdateMotorbikeUsecase
    {

        private readonly IMotorbikesRepository _MotorbikeRepository;

        public UpdateMotorbikeUsecase(IMotorbikesRepository motorbikeRepository)
        {
            _MotorbikeRepository = motorbikeRepository;
        }

        public async Task<MotorbikeResponseDto> Execute(int idMoto, MotorbikeUpdateDto dto)
        {
            // Validar que la moto exista
            var moto = await _MotorbikeRepository.GetMotorbikeByIdAsync(idMoto);
            if (moto == null)
                throw new MotorbikeNotFoundException(idMoto);

            // Validar campo marca
            if (dto.marca != null)
            {
                if (string.IsNullOrWhiteSpace(dto.marca))
                    throw new MotorbikeValidationException("La marca no puede estar vacía.");

                moto.marca = dto.marca;
            }

            // Validar campo modelo
            if (dto.modelo != null)
            {
                if (string.IsNullOrWhiteSpace(dto.modelo))
                    throw new MotorbikeValidationException("El modelo no puede estar vacío.");

                moto.modelo = dto.modelo;
            }

            // Validar campo cilindraje
            if (dto.cilindraje != null)
            {
                if (dto.cilindraje <= 0)
                    throw new MotorbikeValidationException("El cilindraje debe ser mayor a 0.");

                moto.cilindraje = dto.cilindraje.Value;
            }

            //Validar campo año
            if (dto.anio != null)
            {
                int currentYear = DateTime.UtcNow.Year;

                if (dto.anio < 1900 || dto.anio > currentYear + 1)
                    throw new MotorbikeValidationException("El año ingresado no es válido.");

                moto.anio = dto.anio.Value;
            }

            //Guardar cambios
            var updatedMoto = await _MotorbikeRepository.UpdateMotorbikeAsync(moto);

            return MotorbikeMapper.ToDto(updatedMoto);
        }
    }
}
