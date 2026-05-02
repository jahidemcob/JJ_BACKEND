using Backend.src.app.Features.Motobikes.application.DTOs;
using Backend.src.app.Features.Motobikes.application.mappers;
using Backend.src.app.Features.Motobikes.domain.repository;
using Backend.src.app.Features.Motobikes.domain.entities;
using Backend.src.app.Features.Motobikes.application.exceptions;
using System.Text.RegularExpressions;

namespace Backend.src.app.Features.Motobikes.application.usecases
{
    public class CreateMotorbikeUsecase
    {
        private readonly IMotorbikesRepository _MotorbikeRepository;
        
        public CreateMotorbikeUsecase (IMotorbikesRepository motorbikesRepository)
        {
            _MotorbikeRepository = motorbikesRepository;
        }

        public async Task<MotorbikeResponseDto> Execute(MotorbikeCreateDto dto, int userId)
        {

            // Validar marca
            if (string.IsNullOrWhiteSpace(dto.marca))
                throw new MotorbikeValidationException("La marca no puede estar vacia.");

            // Validar modelo
            if (string.IsNullOrWhiteSpace(dto.modelo))
                throw new MotorbikeValidationException("El modelo no puede estar vacio.");

            // Validar placa
            if (string.IsNullOrWhiteSpace(dto.placa))
                throw new MotorbikeValidationException("La placa no puede estar vacia.");

            // Normalizar placa
            var placa = dto.placa.ToUpper().Trim();

            // Validar formato de placa
            var regex = new Regex(@"^[A-Z]{3}[0-9]{2}[A-Z]$");
            if (!regex.IsMatch(placa))
                throw new MotorbikeValidationException("La placa no tiene un formato valido. El formato debe ser tres letras mayusculas, seguidas de dos numeros y una letra mayuscula al final (Ejemplo: ABC12D).");

            // Validar placa única
            if (await _MotorbikeRepository.ExistsByPlateAsync(placa))
                throw new MotorbikeValidationException("La placa ya existe. Por favor ingrese una placa unica.");

            // Validar cilindraje
            if (dto.cilindraje <= 0)
                throw new MotorbikeValidationException("El cilindraje debe ser mayor a 0.");

            // Validar año
            int currentYear = DateTime.UtcNow.Year;
            if (dto.anio < 2010 || dto.anio > currentYear + 1)
                throw new MotorbikeValidationException("El año de la moto no es válido.");

            var moto = MotorbikeMapper.ToEntity(dto, placa, userId);
            var createdMoto = await _MotorbikeRepository.CreateMotorbikeAsync(moto);

            return MotorbikeMapper.ToDto(createdMoto);
        }
    }
}
