namespace Users.Application.DTOs
{
    public class UserUpdateDto
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string NombreUsuario { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public int IdRol { get; set; }
    }
}