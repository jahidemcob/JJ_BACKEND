namespace Backend.src.app.Integrations.ExternalAPIs.Replacements.Application.Exceptions
{
    public class ExternalServiceException : Exception
    {
        public ExternalServiceException(string message) : base(message)
        {
        }
        public ExternalServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
