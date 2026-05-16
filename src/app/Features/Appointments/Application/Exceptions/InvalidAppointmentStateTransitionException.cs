using Backend.src.app.Features.Appointments.Domain.Enums;

namespace Backend.src.app.Features.Appointments.Application.Exceptions
{
    public class InvalidAppointmentStateTransitionException : Exception
    {
        public InvalidAppointmentStateTransitionException(AppointmentState current, AppointmentState next)
            : base($"No se puede cambiar el estado de '{current}' a '{next}'.") { }
    }
}