using Auth.Domain.Entities;
using Auth.Domain.Repositories;

public class UserListUseCase
{
    private readonly IUserManagementRepository _repo;

    public UserListUseCase(IUserManagementRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<Usuario>> GetAllAsync()
    {
        return _repo.GetAllAsync();
    }
}