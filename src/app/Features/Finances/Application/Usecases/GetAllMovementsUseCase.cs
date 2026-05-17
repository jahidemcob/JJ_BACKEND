
using Backend.src.app.Features.Finances.Application.DTOs;
using Backend.src.app.Features.Finances.Application.Mappers;
using Backend.src.app.Features.Finances.Domain.Interfaces;

namespace Backend.src.app.Features.Finances.Application.UseCases
{
    public class GetAllMovementsUseCase
    {
        private readonly IFinancesRepository _repository;

        public GetAllMovementsUseCase(IFinancesRepository repository)
        {
            _repository = repository;
        }

        public async Task<FinancesSummaryDto> Execute()
        {
            var movements = await _repository.GetAllAsync();
            var dtos = movements.Select(FinancesMapper.ToResponseDto).ToList();

            var total = dtos.Sum(m => m.TipoMovimiento == Domain.Enums.MovementType.Ingreso
                ? m.Monto
                : -m.Monto);

            return new FinancesSummaryDto
            {
                Movimientos = dtos,
                Total = total
            };
        }
    }
}