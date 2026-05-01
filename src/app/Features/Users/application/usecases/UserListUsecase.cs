using Backend.src.app.auth.domain.repositories;
using Backend.src.app.Features.Users.application.DTOs;
using Backend.src.app.Features.Users.domain.repositories;

namespace Backend.src.app.Features.Users.application.usecases
{
    public class UserListUsecase
    {
        private readonly IUserManagementRepository _userManagementRepository;
        private readonly IRolRepository _rolRepository;

        public UserListUsecase(
            IUserManagementRepository userManagementRepository,
            IRolRepository rolRepository)
        {
            _userManagementRepository = userManagementRepository;
            _rolRepository = rolRepository;
        }

        public async Task<IEnumerable<UserResponseDto>> Execute()
        {
            var usuarios = await _userManagementRepository.GetAllAsync();
            var roles = await _rolRepository.GetAllAsync();

            var rolesDict = roles.ToDictionary(r => r.IdRol, r => r.NombreRol);

            return usuarios.Select(u => new UserResponseDto
            {
                IdUsuario = u.IdUsuario,
                Nombre = u.Nombre,
                NombreUsuario = u.NombreUsuario,
                Telefono = u.Telefono,
                Correo = u.Correo,
                IdRol = u.IdRol,
                Rol = rolesDict.GetValueOrDefault(u.IdRol, "Sin rol"),
                Activo = u.Activo
            });
        }
    }
}