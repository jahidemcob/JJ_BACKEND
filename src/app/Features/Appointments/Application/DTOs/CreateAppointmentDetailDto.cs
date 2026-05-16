namespace Backend.src.app.Features.Appointments.Application.DTOs
{
    public class CreateAppointmentDetailDto
    {
        public required int IdServicio { get; set; }
        public required decimal PrecioUnitario { get; set; }
    }
}
