using Backend.src.app.auth.application.DTOs;
using Backend.src.app.Shared.Constants;
using Backend.src.app.Shared.Security;  
using Backend.src.app.Features.Users.domain.repositories;
using Backend.src.app.Features.Users.domain.Entities;
using Backend.src.app.auth.application.Exceptions;

namespace Backend.src.app.auth.application.UseCases
{
    public class RegisterUserUseCase
    {
        private readonly IUserManagementRepository _userRepo;   

        public RegisterUserUseCase(
            IUserManagementRepository userRepo)
        {
            _userRepo = userRepo;
            
        }

        public async Task<User> RegisterAsync(RegisterRequestDto request)
        {
            // 1. Verificar si existe alguien con ese username
            var existing = await _userRepo.GetByUsernameAsync(request.NombreUsuario);
            if (existing != null)
                throw new UserOrEmailAlreadyUsedException();

            // (Opcional) Validar correo
            var existingEmail = await _userRepo.GetByEmailAsync(request.Correo);
            if (existingEmail != null)
                throw new UserOrEmailAlreadyUsedException();

            // 2. Crear Hash + Salt
            PasswordService.CreatePasswordHash(
                request.Clave, out byte[] hash, out byte[] salt);

            // 3. Crear la entidad usuario
            var usuario = new User
            {
                Nombre = request.Nombre,
                NombreUsuario = request.NombreUsuario,
                Telefono = request.Telefono,
                Correo = request.Correo,
                IdRol = Roles.Cliente, 
                ClaveHash = hash,
                ClaveSalt = salt,
                Activo = true
            };

            // 4. Guardar en módulo Users
            await _userRepo.CreateAsync(usuario);

            return usuario;
        }
    }
}