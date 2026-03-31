namespace Auth.Domain.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }

        public string Nombre { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;  
        public string Clave { get; set; } = string.Empty;   
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
    }
}