using Backend.src.app.Features.Finances.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Backend.src.app.Features.Finances.Domain.Entities
{
    public class AccountingMovement
    {
        [Key]
        public int IdMovimiento { get; set; }
        public required DateTime Fecha { get; set; }
        public required MovementType TipoMovimiento { get; set; }
        public required string Descripcion { get; set; }
        public required decimal Monto { get; set; }
    }
}
