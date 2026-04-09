using Backend.src.app.auth.domain.repositories;
using Backend.src.app.Features.Users.application.DTOs;
using Backend.src.app.Features.Users.domain.repositories;
using Backend.src.app.Features.Users.application.Exceptions;
using Backend.src.app.Shared.Security;

namespace Backend.src.app.Features.Users.application.usecases
{
    public class UpdateUserUsecase
    {
        private readonly IUserManagementRepository _userManagementRepository;
        private readonly IRolRepository _rolRepository;
        private readonly PasswordService _passwordService;

        public UpdateUserUsecase(
            IUserManagementRepository userManagementRepository,
            IRolRepository rolRepository,
            PasswordService passwordService)
        {
            _userManagementRepository = userManagementRepository;
            _rolRepository = rolRepository;
            _passwordService = passwordService;
        }

        public async Task<UserResponseDto> Execute(UserUpdateDto request)
        {
            // 1. Buscar usuario
            var usuario = await _userManagementRepository.GetByIdAsync(request.IdUsuario);
            if (usuario == null)
                throw new UserNotFoundException();

            // 2. Validar correo duplicado
            var existingEmail = await _userManagementRepository.GetByEmailAsync(request.Correo);
            if (existingEmail != null && existingEmail.IdUsuario != request.IdUsuario)
                throw new EmailUsedException();

            // 3. Validar nombre de usuario duplicado
            var existingUser = await _userManagementRepository.GetByUsernameAsync(request.NombreUsuario);
            if (existingUser != null && existingUser.IdUsuario != request.IdUsuario)
                throw new UserAlreadyUsedException();

            // 4. Validar rol
            var rol = await _rolRepository.GetByIdAsync(request.IdRol);
            if (rol == null)
                throw new RolNotExistException();

            // 5. Actualizar datos generales
            usuario.Nombre = request.Nombre;
            usuario.Telefono = request.Telefono;
            usuario.Correo = request.Correo;
            usuario.NombreUsuario = request.NombreUsuario;
            usuario.IdRol = request.IdRol;

            // 6. Si envió nueva clave, actualizarla
            if (!string.IsNullOrEmpty(request.NuevaClave))
            {
                var (hash, salt) = _passwordService.HashPassword(request.NuevaClave);

                usuario.ClaveHash = hash;
                usuario.ClaveSalt = salt;
            }

            // 7. Guardar cambios
            await _userManagementRepository.UpdateAsync(usuario);

            // 8. Retornar DTO
            return new UserResponseDto
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Telefono = usuario.Telefono,
                Correo = usuario.Correo,
                NombreUsuario = usuario.NombreUsuario,
                IdRol = usuario.IdRol,
                Rol = rol.NombreRol
            };
        }
    }
}