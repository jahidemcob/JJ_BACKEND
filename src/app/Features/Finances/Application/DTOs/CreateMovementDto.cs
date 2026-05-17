using Backend.src.app.Features.Finances.Domain.Enums;

namespace Backend.src.app.Features.Finances.Application.DTOs
{
    public class CreateMovementDto
    {
        public required MovementType TipoMovimiento { get; set; }
        public required string Descripcion { get; set; }
        public required decimal Monto { get; set; }
    }
}
