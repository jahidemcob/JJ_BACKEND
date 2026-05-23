using Backend.src.app.auth.domain.repositories;
using Backend.src.app.Features.Users.application.DTOs;
using Backend.src.app.Features.Users.application.Exceptions;
using Backend.src.app.Features.Users.application.mappers;
using Backend.src.app.Features.Users.domain.repositories;
using Backend.src.app.Shared.Security;

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
            // 1. Buscar usuario (PRIMERO SIEMPRE)
            var usuario = await _userManagementRepository.GetByIdAsync(request.IdUsuario);
            if (usuario == null)
                throw new UserNotFoundException();

            // 2. Validar correo duplicado (DESPUÉS DE TENER USUARIO REAL)
            var existingEmail = await _userManagementRepository.GetByEmailAsync(request.Correo);

            if (existingEmail != null && existingEmail.IdUsuario != usuario.IdUsuario)
                throw new EmailUsedException();

            // 3. Validar username duplicado
            var existingUser = await _userManagementRepository.GetByUsernameAsync(request.NombreUsuario);

            if (existingUser != null && existingUser.IdUsuario != usuario.IdUsuario)
                throw new UserAlreadyUsedException();

            // 4. Validar rol
            var rol = await _rolRepository.GetByIdAsync(request.IdRol);
            if (rol == null)
                throw new RolNotExistException();

            // 5. Actualizar datos
            usuario.Nombre = request.Nombre;
            usuario.Telefono = request.Telefono;
            usuario.Correo = request.Correo;
            usuario.NombreUsuario = request.NombreUsuario;
            usuario.IdRol = request.IdRol;

            // 6. Password opcional
            if (!string.IsNullOrEmpty(request.NuevaClave))
            {
                var (hash, salt) = PasswordService.HashPassword(request.NuevaClave);
                usuario.ClaveHash = hash;
                usuario.ClaveSalt = salt;
            }

            // 7. Guardar
            await _userManagementRepository.UpdateAsync(usuario);

            // 8. Retornar DTO
            return UserMapper.ToDto(usuario, rol.NombreRol);


        }
    }
}