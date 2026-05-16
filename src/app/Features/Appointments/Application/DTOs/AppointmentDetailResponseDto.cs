namespace Backend.src.app.Features.Appointments.Application.DTOs
{
    public class AppointmentDetailResponseDto
    {
        public int IdDetalle { get; set; }
        public int IdServicio { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }
    }
}
