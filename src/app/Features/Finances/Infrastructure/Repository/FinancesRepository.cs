
using Backend.src.app.Features.Finances.Domain.Entities;
using Backend.src.app.Features.Finances.Domain.Enums;
using Backend.src.app.Features.Finances.Domain.Interfaces;
using Backend.src.app.Features.Finances.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Backend.src.app.Features.Finances.Infrastructure.Repository
{
    public class FinancesRepository : IFinancesRepository
    {
        private readonly FinancesDbContext _context;

        public FinancesRepository(FinancesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AccountingMovement>> GetAllAsync()
        {
            return await _context.Contabilidad
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<AccountingMovement?> GetByIdAsync(int id)
        {
            return await _context.Contabilidad
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdMovimiento == id);
        }

        public async Task<IEnumerable<AccountingMovement>> GetByTypeAsync(MovementType type)
        {
            return await _context.Contabilidad
                .Where(m => m.TipoMovimiento == type)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<AccountingMovement> CreateAsync(AccountingMovement movement)
        {
            _context.Contabilidad.Add(movement);
            await _context.SaveChangesAsync();
            return movement;
        }
    }
}