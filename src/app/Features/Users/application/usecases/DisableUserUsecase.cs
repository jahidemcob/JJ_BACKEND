using Backend.src.app.Features.Users.application.DTOs;
using Backend.src.app.Features.Users.domain.repositories;

namespace Backend.src.app.Features.Users.application.UseCases
{
    public class DisableUserUsecase
    {
        private readonly IUserManagementRepository _userManagementRepository;

        public DisableUserUsecase(IUserManagementRepository userManagementRepository)
        {
            _userManagementRepository = userManagementRepository;
        }

        public async Task<bool> Execute(UserDisableDto dto)
        {
            // Buscar el usuario
            var usuario = await _userManagementRepository.GetByIdAsync(dto.IdUsuario);

            if (usuario == null)
                return false;

            // USAR EL VALOR QUE VIENE DEL FRONT
            usuario.Activo = dto.Activo;

            await _userManagementRepository.UpdateAsync(usuario);

            return true;
        }
    }
}