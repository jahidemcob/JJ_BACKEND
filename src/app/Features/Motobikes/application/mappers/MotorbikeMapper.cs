using Backend.src.app.Features.Motobikes.domain.entities;
using Backend.src.app.Features.Motobikes.application.DTOs;

namespace Backend.src.app.Features.Motobikes.application.mappers
{
    public class MotorbikeMapper
    {
        // Entity -> DTO
        public static MotorbikeResponseDto ToDto(Motorbike moto)
        {
            return new MotorbikeResponseDto
            {
                idMoto = moto.idMoto,
                idUsuario = moto.idUsuario,
                marca = moto.marca,
                modelo = moto.modelo,
                placa = moto.placa,
                cilindraje = moto.cilindraje,
                anio = moto.anio,
                Activo = moto.Activo,
            };
        }

        // DTO -> Entity
        public static Motorbike ToEntity(MotorbikeCreateDto dto, string placa)
        {
            return new Motorbike
            {
                idUsuario = dto.IdUsuario,
                marca = dto.marca,
                modelo = dto.modelo,
                placa = placa,
                cilindraje = dto.cilindraje,
                anio = dto.anio,
            };
        }


        public static MotorbikeWithUserResponseDto ToDtoWithUser(Motorbike moto, string nombreUsuario)
        {
            return new MotorbikeWithUserResponseDto
            {
                idMoto = moto.idMoto,
                idUsuario = moto.idUsuario,
                marca = moto.marca,
                modelo = moto.modelo,
                placa = moto.placa,
                cilindraje = moto.cilindraje,
                anio = moto.anio,
                Activo = moto.Activo,
                nombreUsuario = nombreUsuario
            };
        }
    }
}