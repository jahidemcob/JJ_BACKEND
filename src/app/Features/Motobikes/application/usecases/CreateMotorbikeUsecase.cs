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

        public async Task<MotorbikeResponseDto> Execute(MotorbikeCreateDto dto)
        {

            // Validar que el idUsuario sea mayor a 0
            if (dto.IdUsuario <= 0)
                throw new MotorbikeValidationException("El IdUsuario no es valido.");

            // Validar que la marca no este vacia
            if (string.IsNullOrWhiteSpace(dto.marca))
                throw new MotorbikeValidationException("La marca no puede estar vacia.");

            // Validar que el modelo no este vacio
            if (string.IsNullOrWhiteSpace(dto.modelo))
                throw new MotorbikeValidationException("El modelo no puede estar vacio.");

            // Validar que la placa no este vacia
            if (string.IsNullOrWhiteSpace(dto.placa))
                throw new MotorbikeValidationException("La placa no puede estar vacia.");

            //Validar que la placa tenga un formato valido 
            var placa = dto.placa.ToUpper().Trim();
            var regex = new Regex(@"^[A-Z]{3}[0-9]{2}[A-Z]$");
            if (!regex.IsMatch(placa))
                throw new MotorbikeValidationException("La placa no tiene un formato valido. El formato debe ser tres letras mayusculas, seguidas de dos numeros y una letra mayuscula al final que hace referencia al año de la moto (Ejemplo: ABC12D).");

            // Validar que la placa sea unica
            if (await _MotorbikeRepository.ExistsByPlateAsync(placa))
                throw new MotorbikeValidationException("La placa ya existe. Por favor ingrese una placa unica.");

            // Validar que el cilindraje sea mayor a 0
            if (dto.cilindraje <=0)
                throw new MotorbikeValidationException("El cilindraje debe ser mayor a 0.");

            // Validar que el año sea mayor a 2010
            if (dto.anio > 2010)
                throw new MotorbikeValidationException("El año debe ser mayor a 2010.");

            var moto = MotorbikeMapper.ToEntity(dto, placa);

            var createdMoto = await _MotorbikeRepository.CreateMotorbikeAsync(moto);

            var response = MotorbikeMapper.ToDto(createdMoto);

            return response;

        }
    }
}
