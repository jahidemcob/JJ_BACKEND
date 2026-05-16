namespace Backend.src.app.Features.Appointments.Application.Exceptions
{
    public class AppointmentNotFoundException : Exception
    {
        public AppointmentNotFoundException(int IdPedido)
            : base($"La cita con ID {IdPedido} no fue encontrada.")
        {
        }
    }
}
