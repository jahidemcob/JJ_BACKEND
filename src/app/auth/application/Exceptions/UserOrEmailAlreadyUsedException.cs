namespace Backend.src.app.auth.application.Exceptions
{
    public class UserOrEmailAlreadyUsedException : Exception
    {
        public UserOrEmailAlreadyUsedException() 
                : base("El usuario o correo ya existe") { }
    }
}
