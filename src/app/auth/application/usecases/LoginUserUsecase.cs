using Backend.src.app.auth.application.DTOs;
using Backend.src.app.auth.domain.repositories;
using Backend.src.app.auth.application.Services;
using Backend.src.app.Shared.Security;
using Backend.src.app.Features.Users.domain.repositories;
using Backend.src.app.auth.application.Exceptions;

namespace Backend.src.app.auth.application.UseCases
{
    public class LoginUserUseCase
    {
        private readonly IUserManagementRepository _userRepo;
        private readonly IRolRepository _rolRepo;
        
        private readonly TokenService _tokenService;

        public LoginUserUseCase(
            IUserManagementRepository userRepo,
            IRolRepository rolRepo,
            TokenService tokenService)
        {
            _userRepo = userRepo;
            _rolRepo = rolRepo;
     
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            // 1. Buscar usuario
            var usuario = await _userRepo.GetByUsernameAsync(request.Username);
            if (usuario == null)
                throw new InvalidCredentialsException();

            // 2. Validar contraseña
            if (!PasswordService.VerifyPassword(
                request.Clave,
                usuario.ClaveHash,
                usuario.ClaveSalt))
                throw new InvalidCredentialsException();

            //Confirmar si esta activo
            if (!usuario.Activo)
                throw new UserInactiveException();


            // 3. Obtener rol desde AUTH
            var rol = await _rolRepo.GetByIdAsync(usuario.IdRol);
            if (rol == null)
                throw new UserWithNoRolException();

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