using Backend.src.app.Features.Finances.Application.DTOs;
using Backend.src.app.Features.Finances.Domain.Entities;

namespace Backend.src.app.Features.Finances.Application.Mappers
{
    public static class FinancesMapper
    {
        // ─── Entidad → Response ───
        public static MovementResponseDto ToResponseDto(AccountingMovement movement)
        {
            return new MovementResponseDto
            {
                IdMovimiento = movement.IdMovimiento,
                Fecha = movement.Fecha,
                TipoMovimiento = movement.TipoMovimiento,
                Descripcion = movement.Descripcion,
                Monto = movement.Monto
            };
        }

        // ─── CreateDto → Entidad ───
        public static AccountingMovement ToEntity(CreateMovementDto dto)
        {
            return new AccountingMovement
            {
                Fecha = DateTime.Now,
                TipoMovimiento = dto.TipoMovimiento,
                Descripcion = dto.Descripcion,
                Monto = dto.Monto
            };
        }
    }
}