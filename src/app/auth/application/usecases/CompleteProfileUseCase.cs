using Backend.src.app.auth.application.DTOs;
using Backend.src.app.Features.Users.domain.repositories;
using Backend.src.app.Shared.Security;

namespace Backend.src.app.auth.application.UseCases
{
    public class CompleteProfileUseCase
    {
        private readonly IUserManagementRepository _userRepo;

        public CompleteProfileUseCase(IUserManagementRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task ExecuteAsync(CompleteProfileRequestDto dto)
        {
            var usuario = await _userRepo.GetByIdAsync(dto.IdUsuario)
                ?? throw new KeyNotFoundException("Usuario no encontrado");

            // Verificar que el username no esté en uso por otro usuario
            var existingUsername = await _userRepo.GetByUsernameAsync(dto.NombreUsuario);
            if (existingUsername != null && existingUsername.IdUsuario != dto.IdUsuario)
                throw new InvalidOperationException("El nombre de usuario ya está en uso");

            // Generar hash y salt de la nueva clave
            PasswordService.CreatePasswordHash(dto.Clave, out byte[] hash, out byte[] salt);

            usuario.NombreUsuario = dto.NombreUsuario;
            usuario.Telefono = dto.Telefono;
            usuario.ClaveHash = hash;
            usuario.ClaveSalt = salt;

            await _userRepo.UpdateAsync(usuario);
        }
    }
}