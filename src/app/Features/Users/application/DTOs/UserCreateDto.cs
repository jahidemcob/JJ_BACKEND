namespace Users.Application.DTOs
{
    public class UserCreateDto
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
    }
}