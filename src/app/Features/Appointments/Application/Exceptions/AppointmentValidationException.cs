namespace Backend.src.app.Features.Appointments.Application.Exceptions
{
    public class AppointmentValidationException : Exception
    {
        public AppointmentValidationException(string message) : base(message) { }
    }
}
