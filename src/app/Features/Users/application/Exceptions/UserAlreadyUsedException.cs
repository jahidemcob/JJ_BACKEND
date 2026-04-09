namespace Backend.src.app.Features.Users.application.Exceptions
{
    public class UserAlreadyUsedException : Exception
    {
        public UserAlreadyUsedException() : base("El nombre de usuario ya está en uso.") { }
     
    }
}
