
using Backend.src.app.Features.Finances.Application.DTOs;
using Backend.src.app.Features.Finances.Application.Exceptions;
using Backend.src.app.Features.Finances.Application.Mappers;
using Backend.src.app.Features.Finances.Domain.Interfaces;

namespace Backend.src.app.Features.Finances.Application.UseCases
{
    public class CreateMovementUseCase
    {
        private readonly IFinancesRepository _repository;

        public CreateMovementUseCase(IFinancesRepository repository)
        {
            _repository = repository;
        }

        public async Task<MovementResponseDto> Execute(CreateMovementDto dto)
        {
            if (dto.Monto <= 0)
                throw new MovementValidationException("El monto debe ser mayor a cero.");

            if (string.IsNullOrWhiteSpace(dto.Descripcion))
                throw new MovementValidationException("La descripción es obligatoria.");

            var movement = FinancesMapper.ToEntity(dto);
            var created = await _repository.CreateAsync(movement);
            return FinancesMapper.ToResponseDto(created);
        }
    }
}