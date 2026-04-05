using Backend.src.app.Features.Users.domain.repositories;
using Users.Application.DTOs;

namespace Users.Application.UseCases.Users
{
    public class UpdateUserUseCase
    {
        private readonly IUserManagementRepository _userManagementRepository;

        public UpdateUserUseCase(IUserManagementRepository userManagementRepository)
        {
            _userManagementRepository = userManagementRepository;
        }

        public async Task Execute(UserUpdateDto request)
        {
            // 1. Buscar usuario a actualizar
            var usuario = await _userManagementRepository.GetByIdAsync(request.IdUsuario);
            if (usuario == null)
                throw new Exception("Usuario no encontrado.");

            // 2. Validar correo duplicado 
            var existingEmail = await _userManagementRepository.GetByEmailAsync(request.Correo);
            if (existingEmail != null && existingEmail.IdUsuario != request.IdUsuario)
                throw new Exception("El correo ya está en uso.");

            // 3. Validar username duplicado 
            var existingUser = await _userManagementRepository.GetByUsernameAsync(request.NombreUsuario);
            if (existingUser != null && existingUser.IdUsuario != request.IdUsuario)
                throw new Exception("El nombre de usuario ya está en uso.");

            // 4. Actualizar datos
            usuario.Nombre = request.Nombre;
            usuario.Telefono = request.Telefono;
            usuario.Correo = request.Correo;
            usuario.NombreUsuario = request.NombreUsuario;
            usuario.IdRol = request.IdRol;

            // 5. Guardar cambios
            await _userManagementRepository.UpdateAsync(usuario);
        }
    }
}