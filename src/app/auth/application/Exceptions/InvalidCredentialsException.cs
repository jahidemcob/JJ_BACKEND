namespace Backend.src.app.auth.application.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() 
                : base("Credenciales inválidas. Verifique su usuario y contraseña.") { }
    }
}
