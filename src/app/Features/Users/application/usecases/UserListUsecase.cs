using Auth.Domain.Repositories;
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

            // Convertimos la entidad Usuario → UserResponseDto
            var lista = new List<UserResponseDto>();

            foreach (var u in usuarios)
            {
                var rol = await _rolRepository.GetByIdAsync(u.IdRol);

                lista.Add(new UserResponseDto
                {
                    IdUsuario = u.IdUsuario,
                    Nombre = u.Nombre,
                    NombreUsuario = u.NombreUsuario,
                    Telefono = u.Telefono,
                    Correo = u.Correo,
                    IdRol = u.IdRol,
                    Rol = rol?.NombreRol ?? "Sin rol"
                });
            }

            return lista;
        }
    }
}