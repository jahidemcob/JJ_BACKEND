namespace Backend.src.app.Features.Users.application.Exceptions
{
    public class RolNotExistException : Exception
    {
        public RolNotExistException() : base("El rol especificado no existe.") { }
   
    }
}
