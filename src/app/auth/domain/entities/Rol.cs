
using System.ComponentModel.DataAnnotations;

namespace Backend.src.app.auth.domain.entities
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }
        public string NombreRol { get; set; } = string.Empty;
    }
}