using System.ComponentModel.DataAnnotations;

namespace Backend.src.app.Features.Services.domain.entities
{
    public class Service
    {

        [Key]
        public int idServicio { get; set; }
        public string nombreServicio { get; set; }

        public string descripcion { get; set; }

        public decimal precioBase { get; set; }

        public bool Activo { get; set; }
    }
}
