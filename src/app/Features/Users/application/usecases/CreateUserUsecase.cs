using Backend.src.app.auth.domain.repositories;
using Backend.src.app.Features.Users.application.DTOs;
using Backend.src.app.Features.Users.application.Exceptions;
using Backend.src.app.Features.Users.application.mappers;
using Backend.src.app.Features.Users.domain.Entities;
using Backend.src.app.Features.Users.domain.repositories;
using Backend.src.app.Shared.Security;

namespace Backend.src.app.Features.Users.application.UseCases
{
    public class CreateUserUsecase
    {
        private readonly IUserManagementRepository _repo;
        private readonly PasswordService _passwordService;
        private readonly IRolRepository _rolRepository;

        public CreateUserUsecase(
            IUserManagementRepository repo,
            IRolRepository rolRepository,
            PasswordService passwordService)
        {
            _repo = repo;
            _passwordService = passwordService;
            _rolRepository = rolRepository;
        }

        public async Task<UserResponseDto> Execute(UserCreateDto dto)
        {
            // Validar duplicados
            var existingUser = await _repo.GetByUsernameAsync(dto.NombreUsuario);
            if (existingUser != null)
                throw new UserAlreadyUsedException();

            var existingEmail = await _repo.GetByEmailAsync(dto.Correo);
            if (existingEmail != null)
                throw new EmailUsedException();

            // Hash password
            _passwordService.CreatePasswordHash(
                dto.Clave,
                out var hash,
                out var salt
            );

            // Crear entidad
            var user = new User
            {
                Nombre = dto.Nombre,
                NombreUsuario = dto.NombreUsuario,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                IdRol = dto.IdRol,
                ClaveHash = hash,
                ClaveSalt = salt,
                Activo = true
            };

            await _repo.CreateAsync(user);

            // obtener rol para JSON
            var rol = await _rolRepository.GetByIdAsync(user.IdRol);

            return UserMapper.ToDto(user, rol?.NombreRol ?? "Sin rol");
        }
    }
}