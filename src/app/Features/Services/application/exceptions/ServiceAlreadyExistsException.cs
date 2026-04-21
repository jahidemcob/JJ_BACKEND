namespace Backend.src.app.Features.Services.application.exceptions
{
    public class ServiceAlreadyExistsException: Exception
    {
        public ServiceAlreadyExistsException(string name) : base($"El servicio con el nombre: {name} ya existe")
        {
        }
    }
}
