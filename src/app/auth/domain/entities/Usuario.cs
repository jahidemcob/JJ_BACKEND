using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Domain.Entities
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }

        public string Nombre { get; set; } = string.Empty;

        [Column("Usuario")]
        public string NombreUsuario { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public byte[] ClaveHash { get; set; } = Array.Empty<byte>();
        public byte[] ClaveSalt { get; set; } = Array.Empty<byte>();
    }
}