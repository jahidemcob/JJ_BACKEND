namespace Backend.src.app.Features.Services.application.exceptions
{
    public class ServiceValidationException: Exception
    {
        public ServiceValidationException(string message) : base(message)
        {
        }
    }
}
