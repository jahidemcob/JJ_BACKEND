namespace Backend.src.app.Features.Finances.Application.DTOs
{
    public class FinancesSummaryDto
    {
        public IEnumerable<MovementResponseDto> Movimientos { get; set; } = new List<MovementResponseDto>();
        public decimal Total { get; set; }
    }
}
