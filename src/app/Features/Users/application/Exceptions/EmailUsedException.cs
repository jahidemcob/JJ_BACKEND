namespace Backend.src.app.Features.Users.application.Exceptions
{
    public class EmailUsedException : Exception
    {
        public EmailUsedException()
            : base($"El correo ya está en uso.") { }
    }
}
