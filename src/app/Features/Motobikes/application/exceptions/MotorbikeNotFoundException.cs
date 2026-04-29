namespace Backend.src.app.Features.Motobikes.application.exceptions
{
    public class MotorbikeNotFoundException : Exception
    {
        public MotorbikeNotFoundException(int id) : base($"La motocicleta con el id: {id} no existe")
        {
        }
    }
}
