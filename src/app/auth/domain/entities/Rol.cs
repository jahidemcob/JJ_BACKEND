
using System.ComponentModel.DataAnnotations;

namespace Auth.Domain.Entities
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }
        public string NombreRol { get; set; } = string.Empty;
    }
}