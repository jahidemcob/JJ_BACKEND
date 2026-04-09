namespace Backend.src.app.auth.application.Exceptions
{
    public class UserInactiveException : Exception
    {
        public UserInactiveException() 
            : base("Usuario inactivo. Contacte al administrador.") { }

    }
}
