using Google.Apis.Auth;
using Backend.src.app.auth.application.DTOs;
using Backend.src.app.auth.application.Services;
using Backend.src.app.Features.Users.domain.repositories;
using Backend.src.app.Features.Users.domain.Entities;
using Backend.src.app.Shared.Constants;

namespace Backend.src.app.auth.application.UseCases
{
    public class GoogleLoginUseCase
    {
        private readonly IUserManagementRepository _userRepo;
        private readonly TokenService _tokenService;
        private readonly IConfiguration _configuration;

        public GoogleLoginUseCase(
            IUserManagementRepository userRepo,
            TokenService tokenService,
            IConfiguration configuration)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto> ExecuteAsync(string idToken)
        {
            // 1. Validar token con Google
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { _configuration["Google:ClientId"] }
            };

            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            }
            catch
            {
                throw new UnauthorizedAccessException("Token de Google inválido");
            }

            // 2. Buscar usuario por correo
            var usuario = await _userRepo.GetByEmailAsync(payload.Email);
            bool esNuevo = usuario == null;

            // 3. Si no existe, registrarlo
            if (esNuevo)
            {
                usuario = new User
                {
                    Nombre = payload.Name ?? payload.Email,
                    NombreUsuario = payload.Email,
                    Correo = payload.Email,
                    Telefono = string.Empty,
                    IdRol = Roles.Cliente,
                    ClaveHash = Array.Empty<byte>(),
                    ClaveSalt = Array.Empty<byte>(),
                    Activo = true
                };

                await _userRepo.CreateAsync(usuario);
            }

            // 4. Generar tu JWT
            var rolNombre = usuario!.IdRol == Roles.Cliente ? "cliente"
                          : usuario.IdRol == Roles.Empleado ? "empleado"
                          : "administrador";

            var token = _tokenService.GenerateToken(usuario.IdUsuario, rolNombre);

            // 5. PerfilCompleto = false si es nuevo (le falta usuario, teléfono y clave)
            return new LoginResponseDto
            {
                IdUsuario = usuario.IdUsuario,
                Token = token,
                Nombre = usuario.Nombre,
                Rol = rolNombre,
                PerfilCompleto = !esNuevo
            };
        }
    }
}