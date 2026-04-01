using Auth.Application.DTOs;
using Auth.Application.Services;
using Auth.Domain.Repositories;
using Auth.Domain.Entities;

namespace Auth.Application.UseCases
{
    public class LoginUserUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRolRepository _rolRepository;
        private readonly PasswordService _passwordService;
        private readonly TokenService _tokenService;

        public LoginUserUseCase(
            IUsuarioRepository usuarioRepository,
            IRolRepository rolRepository,
            PasswordService passwordService,
            TokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _rolRepository = rolRepository;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            // 1. Buscar usuario
            var usuario = await _usuarioRepository.GetByUsernameAsync(request.Username);
            if (usuario == null)
                return null;

            // 2. Validar contraseña
            if (!_passwordService.VerifyPassword(request.Clave, usuario.ClaveHash, usuario.ClaveSalt))
                return null;

            // 3. Obtener el rol del usuario
            var rol = await _rolRepository.GetByIdAsync(usuario.IdRol);
            if (rol == null)
                return null;

            // 4. Generar token
            var token = _tokenService.GenerateToken(usuario.IdUsuario, rol.NombreRol);

            // 5. Construir la respuesta
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