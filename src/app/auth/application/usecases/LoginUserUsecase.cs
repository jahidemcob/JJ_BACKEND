using Auth.Application.DTOs;
using Auth.Domain.Repositories;
using Backend.src.app.auth.application.Services;
using Backend.src.app.Shared.Security;
using Backend.src.app.Features.Users.domain.repositories;

namespace Auth.Application.UseCases
{
    public class LoginUserUseCase
    {
        private readonly IUserManagementRepository _userRepo;
        private readonly IRolRepository _rolRepo;
        private readonly PasswordService _passwordService;
        private readonly TokenService _tokenService;

        public LoginUserUseCase(
            IUserManagementRepository userRepo,
            IRolRepository rolRepo,
            PasswordService passwordService,
            TokenService tokenService)
        {
            _userRepo = userRepo;
            _rolRepo = rolRepo;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            // 1. Buscar usuario
            var usuario = await _userRepo.GetByUsernameAsync(request.Username);
            if (usuario == null)
                return null;

            // 2. Validar contraseña
            if (!_passwordService.VerifyPassword(
                request.Clave,
                usuario.ClaveHash,
                usuario.ClaveSalt))
                return null;

            // 3. Obtener rol desde AUTH
            var rol = await _rolRepo.GetByIdAsync(usuario.IdRol);
            if (rol == null)
                return null;

            // 4. Generar token
            var token = _tokenService.GenerateToken(usuario.IdUsuario, rol.NombreRol);

            // 5. Respuesta
            return new LoginResponseDto
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Rol = rol.NombreRol,
                Token = token
            };
        }
    }
}