namespace Backend.src.app.Features.Appointments.Application.Exceptions
{
    public class InvalidEmployeeAssignmentException : Exception
    {
        public InvalidEmployeeAssignmentException()
            : base("El usuario asignado no tiene rol de Empleado.") { }
    }
}