using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneHope.API.Migrations
{
    /// <inheritdoc />
    public partial class OneHope : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Portatiles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modelo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PrecioCompra = table.Column<float>(type: "real", nullable: false),
                    PrecioAlquiler = table.Column<float>(type: "real", nullable: false),
                    PrecioCoste = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    StockAlquiler = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portatiles", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Portatiles");
        }
    }
}
