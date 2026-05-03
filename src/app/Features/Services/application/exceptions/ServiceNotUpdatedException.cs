namespace Backend.src.app.Features.Services.application.exceptions
{
    public class ServiceNotUpdatedException : Exception
    {
        public ServiceNotUpdatedException(int id) : base($"No se pudo actualizar el servicio con ID {id}.")
        {
        }
    }
}
