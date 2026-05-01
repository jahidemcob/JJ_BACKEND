using Backend.src.app.auth.domain.repositories;
using Backend.src.app.Features.Users.application.DTOs;
using Backend.src.app.Features.Users.application.mappers;
using Backend.src.app.Features.Users.domain.repositories;

public class GetUserByIdUsecase
{
    private readonly IUserManagementRepository _userManagementRepository;
    private readonly IRolRepository _rolRepository;

    public GetUserByIdUsecase(
        IUserManagementRepository userManagementRepository,
        IRolRepository rolRepository)
    {
        _userManagementRepository = userManagementRepository;
        _rolRepository = rolRepository;
    }

    public async Task<UserResponseDto?> Execute(int id)
    {
        var usuario = await _userManagementRepository.GetByIdAsync(id);

        if (usuario == null)
            return null;

        var rol = await _rolRepository.GetByIdAsync(usuario.IdRol);

        return UserMapper.ToDto(usuario, rol?.NombreRol ?? "Sin rol");
    }
}