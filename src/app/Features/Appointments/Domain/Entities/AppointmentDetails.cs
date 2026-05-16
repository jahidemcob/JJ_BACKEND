namespace Backend.src.app.Features.Appointments.Domain.Entities
{
    public class AppointmentDetails
    {

        public int IdDetalle { get; set; }
        public int IdPedido { get; set; }
        public int IdServicio { get; set; }
        public required decimal PrecioUnitario { get; set; }
        public required decimal SubTotal { get; set; }
    }
}
