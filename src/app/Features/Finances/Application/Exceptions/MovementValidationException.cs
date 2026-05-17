
namespace Backend.src.app.Features.Finances.Application.Exceptions
{
    public class MovementValidationException : Exception
    {
        public MovementValidationException(string message)
            : base(message) { }
    }
}