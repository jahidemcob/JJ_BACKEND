using Auth.Domain.Repositories;
using Backend.src.app.Features.Users.application.DTOs;
using Backend.src.app.Features.Users.domain.Entities;
using Backend.src.app.Features.Users.domain.repositories;

namespace Backend.src.app.Features.Users.application.usecases
{
    public class UpdateUserUsecase
    {
        private readonly IUserManagementRepository _userManagementRepository;
        private readonly IRolRepository _rolRepository;

        public UpdateUserUsecase(
            IUserManagementRepository userManagementRepository,
            IRolRepository rolRepository)
        {
            _userManagementRepository = userManagementRepository;
            _rolRepository = rolRepository;
        }

        public async Task<UserResponseDto> Execute(UserUpdateDto request)
        {
            // 1. Buscar usuario
            var usuario = await _userManagementRepository.GetByIdAsync(request.IdUsuario);
            if (usuario == null)
                throw new InvalidOperationException("Usuario no encontrado.");

            // 2. Validar correo duplicado
            var existingEmail = await _userManagementRepository.GetByEmailAsync(request.Correo);
            if (existingEmail != null && existingEmail.IdUsuario != request.IdUsuario)
                throw new InvalidOperationException("El correo ya está en uso.");

            // 3. Validar nombre de usuario duplicado
            var existingUser = await _userManagementRepository.GetByUsernameAsync(request.NombreUsuario);
            if (existingUser != null && existingUser.IdUsuario != request.IdUsuario)
                throw new InvalidOperationException("El nombre de usuario ya está en uso.");

            // 4. Validar existencia del Rol
            var rol = await _rolRepository.GetByIdAsync(request.IdRol);
            if (rol == null)
                throw new InvalidOperationException("El rol especificado no existe.");

            // 5. Actualizar datos del usuario (la contraseña NO se modifica aquí)
            usuario.Nombre = request.Nombre;
            usuario.Telefono = request.Telefono;
            usuario.Correo = request.Correo;
            usuario.NombreUsuario = request.NombreUsuario;
            usuario.IdRol = request.IdRol;

            // 6. Guardar cambios
            await _userManagementRepository.UpdateAsync(usuario);

            // 7. Retornar DTO actualizado
            return new UserResponseDto
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Telefono = usuario.Telefono,
                Correo = usuario.Correo,
                NombreUsuario = usuario.NombreUsuario,
                IdRol = usuario.IdRol,
                Rol = rol.NombreRol // si tu entidad Rol lo tiene
            };
        }
    }
}