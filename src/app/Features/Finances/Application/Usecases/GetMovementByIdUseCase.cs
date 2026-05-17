
using Backend.src.app.Features.Finances.Application.DTOs;
using Backend.src.app.Features.Finances.Application.Exceptions;
using Backend.src.app.Features.Finances.Application.Mappers;
using Backend.src.app.Features.Finances.Domain.Interfaces;

namespace Backend.src.app.Features.Finances.Application.UseCases
{
    public class GetMovementByIdUseCase
    {
        private readonly IFinancesRepository _repository;

        public GetMovementByIdUseCase(IFinancesRepository repository)
        {
            _repository = repository;
        }

        public async Task<MovementResponseDto> Execute(int id)
        {
            var movement = await _repository.GetByIdAsync(id)
                ?? throw new MovementNotFoundException(id);

            return FinancesMapper.ToResponseDto(movement);
        }
    }
}