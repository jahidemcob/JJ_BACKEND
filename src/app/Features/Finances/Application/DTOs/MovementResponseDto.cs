using Backend.src.app.Features.Finances.Domain.Enums;

namespace Backend.src.app.Features.Finances.Application.DTOs
{
    public class MovementResponseDto
    {
        public int IdMovimiento { get; set; }
        public DateTime Fecha { get; set; }
        public MovementType TipoMovimiento { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal Monto { get; set; }
    }
}
