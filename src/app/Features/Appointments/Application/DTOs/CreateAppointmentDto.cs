namespace Backend.src.app.Features.Appointments.Application.DTOs
{
    public class CreateAppointmentDto
    {
        public required int IdUsuario { get; set; }
        public required int IdMoto { get; set; }
        public required DateOnly FechaCita { get; set; }
        public required TimeOnly HoraCita { get; set; }
        public required List<CreateAppointmentDetailDto> Detalles { get; set; }
    }
}
