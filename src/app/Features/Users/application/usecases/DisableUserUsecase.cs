
using Backend.src.app.Features.Users.domain.repositories;

namespace Users.Application.UseCases.Users
{
    public class DisableUserUseCase
    {
        private readonly IUserManagementRepository _userManagementRepository;

        public DisableUserUseCase(IUserManagementRepository userManagementRepository)
        {
            _userManagementRepository = userManagementRepository;
        }

        public async Task Execute(int idUsuario)
        {
            // Busacar el usuario por su ID
            var usuario = await _userManagementRepository.GetByIdAsync(idUsuario);

            if (usuario == null)
                throw new KeyNotFoundException("Usuario no encontrado.");

            // Si existe entonces se desactiva, false = 0
            usuario.Activo = false;

            await _userManagementRepository.UpdateAsync(usuario);
        }
    }
}