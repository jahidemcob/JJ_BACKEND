using Backend.src.app.Features.Appointments.Domain.Enums;

namespace Backend.src.app.Features.Appointments.Application.DTOs
{
    public class UpdateAppointmentStateDto
    {
        public required AppointmentState NuevoEstado { get; set; }
    }
}
