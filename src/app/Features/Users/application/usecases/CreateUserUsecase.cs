using Auth.Application.Services;
using Auth.Domain.Entities;
using Auth.Domain.Repositories;
using Users.Application.DTOs;

public class CreateUserUseCase
{
    private readonly IUserManagementRepository _repo;
    private readonly PasswordService _passwordService;

    public CreateUserUseCase(IUserManagementRepository repo, PasswordService passwordService)
    {
        _repo = repo;
        _passwordService = passwordService;
    }

    public async Task<Usuario> CreateAsync(UserCreateDto dto)
    {
        _passwordService.CreatePasswordHash(dto.Clave, out var hash, out var salt);

        var user = new Usuario
        {
            Nombre = dto.Nombre,
            NombreUsuario = dto.NombreUsuario,
            Telefono = dto.Telefono,
            Correo = dto.Correo,
            IdRol = dto.IdRol,   
            ClaveHash = hash,
            ClaveSalt = salt
        };

        await _repo.CreateAsync(user);
        return user;
    }
}