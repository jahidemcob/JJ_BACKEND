namespace Backend.src.app.Features.Services.application.exceptions
{
    public class ServiceNotFoundException : Exception
    {
        public ServiceNotFoundException(int id) : base($"El servicio con id: {id} no existe")
        {
        }
    }
}
