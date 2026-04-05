using Backend.src.app.Features.Users.domain.Entities;
using Backend.src.app.Features.Users.domain.repositories;

namespace Users.Application.UseCases.Users
{
    public class UserListUseCase
    {
        private readonly IUserManagementRepository _repo;

        public UserListUseCase(IUserManagementRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Usuario>> Execute()
        {
            return await _repo.GetAllAsync();
        }
    }
}