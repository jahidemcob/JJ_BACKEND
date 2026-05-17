
using Backend.src.app.Features.Finances.Application.DTOs;
using Backend.src.app.Features.Finances.Application.Mappers;
using Backend.src.app.Features.Finances.Domain.Enums;
using Backend.src.app.Features.Finances.Domain.Interfaces;

namespace Backend.src.app.Features.Finances.Application.UseCases
{
    public class GetMovementsByTypeUseCase
    {
        private readonly IFinancesRepository _repository;

        public GetMovementsByTypeUseCase(IFinancesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MovementResponseDto>> Execute(MovementType type)
        {
            var movements = await _repository.GetByTypeAsync(type);
            return movements.Select(FinancesMapper.ToResponseDto);
        }
    }
}