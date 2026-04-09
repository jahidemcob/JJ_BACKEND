namespace Backend.src.app.auth.application.Exceptions
{
    public class UserWithNoRolException : Exception
    {
        public UserWithNoRolException() 
            : base("Error interno: Su usuario no tiene asignado un rol.") { }
    }
}
