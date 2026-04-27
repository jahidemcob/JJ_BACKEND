using Backend.src.app.Features.Users.application.DTOs;
using Backend.src.app.Features.Users.domain.Entities;

namespace Backend.src.app.Features.Users.application.mappers
{
    public static class UserMapper
    {
        public static UserResponseDto ToDto(User u, string NombreRol)
        {
            return new UserResponseDto
            {
                IdUsuario = u.IdUsuario,
                IdRol = u.IdRol,
                Nombre = u.Nombre,
                NombreUsuario = u.NombreUsuario,
                Telefono = u.Telefono,
                Correo = u.Correo,
                Activo = u.Activo,
                Rol = NombreRol
            };
        }
    }
}