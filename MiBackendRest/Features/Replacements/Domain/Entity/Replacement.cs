namespace MiBackendRest.Features.Replacements.Domain.Entity
{
    public class Replacement
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public decimal Precio { get; set; }
    }
}
