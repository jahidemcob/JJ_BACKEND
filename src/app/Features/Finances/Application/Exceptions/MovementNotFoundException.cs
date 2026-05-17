namespace Backend.src.app.Features.Finances.Application.Exceptions
{
    public class MovementNotFoundException : Exception
    {
        public MovementNotFoundException(int id)
            : base($"El movimiento con ID {id} no fue encontrado.") { }
    }
}