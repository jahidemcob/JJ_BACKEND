using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations.MotobikesDb
{
    /// <inheritdoc />
    public partial class InitialMotobikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Motos",
                columns: table => new
                {
                    idMoto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    marca = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    modelo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    placa = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    cilindraje = table.Column<int>(type: "int", nullable: false),
                    anio = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motos", x => x.idMoto);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Motos");
        }
    }
}
