using Backend.src.app.Features.Finances.Domain.Entities;
using Backend.src.app.Features.Finances.Domain.Enums;

namespace Backend.src.app.Features.Finances.Domain.Interfaces
{
    public interface IFinancesRepository
    {
        Task<IEnumerable<AccountingMovement>> GetAllAsync();
        Task<AccountingMovement?> GetByIdAsync(int id);
        Task<IEnumerable<AccountingMovement>> GetByTypeAsync(MovementType type);
        Task<AccountingMovement> CreateAsync(AccountingMovement movement);
    }
}
