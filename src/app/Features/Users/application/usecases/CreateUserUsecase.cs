using Backend.src.app.Features.Users.domain.Entities;
using Backend.src.app.Features.Users.application.DTOs;
using Backend.src.app.Features.Users.domain.repositories;
using Backend.src.app.Shared.Security;

namespace Backend.src.app.Features.Users.application.UseCases
{
    public class CreateUserUsecase
    {
        private readonly IUserManagementRepository _repo;
        private readonly PasswordService _passwordService;

        public CreateUserUsecase(
            IUserManagementRepository repo,
            PasswordService passwordService)
        {
            _repo = repo;
            _passwordService = passwordService;
        }

        public async Task<User> Execute(UserCreateDto dto)
        {
            // Validar nombre de usuario duplicado
            var existingUser = await _repo.GetByUsernameAsync(dto.NombreUsuario);
            if (existingUser != null)
                throw new Exception("El nombre de usuario ya está en uso.");

            // Validar correo duplicado
            var existingEmail = await _repo.GetByEmailAsync(dto.Correo);
            if (existingEmail != null)
                throw new Exception("El correo ya está en uso.");

            // Generar hash y salt
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
            return user;
        }
    }
}