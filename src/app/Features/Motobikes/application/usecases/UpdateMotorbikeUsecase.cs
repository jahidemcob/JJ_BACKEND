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

            // 2. Validar campos (solo si vienen cambios)
            if (!string.IsNullOrWhiteSpace(dto.marca))
                moto.marca = dto.marca;

            if (!string.IsNullOrWhiteSpace(dto.modelo))
                moto.modelo = dto.modelo;

            if (dto.cilindraje > 0)
                moto.cilindraje = dto.cilindraje;

            if (dto.anio > 2010)
                moto.anio = dto.anio;

            // 4. Guardar cambios
            var updatedMoto = await _MotorbikeRepository.UpdateMotorbikeAsync(moto);

            if (updatedMoto == null)
                throw new MotorbikeValidationException("No se pudo actualizar la moto.");

            return MotorbikeMapper.ToDto(updatedMoto);
        }
    }
}
