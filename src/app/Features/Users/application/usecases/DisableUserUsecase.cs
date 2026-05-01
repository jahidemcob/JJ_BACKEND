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

        public async Task<bool> Execute(int id)
        {
            var usuario = await _userManagementRepository.GetByIdAsync(id);

            if (usuario == null)
                return false;

            usuario.Activo = !usuario.Activo;

            await _userManagementRepository.UpdateAsync(usuario);

            return usuario.Activo;
        }
    }
}