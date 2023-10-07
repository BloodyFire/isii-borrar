using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneHope.API.Migrations
{
    /// <inheritdoc />
    public partial class Migracion3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LineaDevolucion",
                columns: table => new
                {
                    IdLinea = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDevolucion = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    IdLineaCompra = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineaDevolucion", x => x.IdLinea);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineaDevolucion");
        }
    }
}
