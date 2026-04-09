namespace Backend.src.app.Features.Users.application.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
            : base("Usuario no encontrado.")
        {
        }
    }
}
