using Auth.Application.DTOs;
using Auth.Application.Services;
using Auth.Domain.Entities;
using Auth.Domain.Repositories;

namespace Auth.Application.UseCases
{
    public class RegisterUserUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly PasswordService _passwordService;

        public RegisterUserUseCase(IUsuarioRepository usuarioRepository, PasswordService passwordService)
        {
            _usuarioRepository = usuarioRepository;
            _passwordService = passwordService;
        }

        public async Task<Usuario?> RegisterAsync(RegisterRequestDto request)
        {
            // 1. Verificar si ya existe un usuario con ese username
            if (await _usuarioRepository.UsuarioExistsAsync(request.NombreUsuario))
                return null;

            // 2. Generar Hash + Salt
            _passwordService.CreatePasswordHash(request.Clave, out byte[] hash, out byte[] salt);

            // 3. Crear el usuario
            var usuario = new Usuario
            {
                Nombre = request.Nombre,
                NombreUsuario = request.NombreUsuario,
                Telefono = request.Telefono,
                Correo = request.Correo,
                IdRol = 3,
                ClaveHash = hash,
                ClaveSalt = salt
            };

            // 4. Guardarlo en la BD
            await _usuarioRepository.CreateAsync(usuario);

            return usuario;
        }
    }
}