using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneHope.API.Migrations
{
    /// <inheritdoc />
    public partial class Migracion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alquilers_MetodoPago_MetodoPagoID",
                table: "Alquilers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MetodoPago",
                table: "MetodoPago");

            migrationBuilder.RenameTable(
                name: "MetodoPago",
                newName: "MetodoPagos");

            migrationBuilder.RenameColumn(
                name: "TipoMetodoPago",
                table: "MetodoPagos",
                newName: "Discriminator");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetodoPagos",
                table: "MetodoPagos",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Alquilers_MetodoPagos_MetodoPagoID",
                table: "Alquilers",
                column: "MetodoPagoID",
                principalTable: "MetodoPagos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alquilers_MetodoPagos_MetodoPagoID",
                table: "Alquilers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MetodoPagos",
                table: "MetodoPagos");

            migrationBuilder.RenameTable(
                name: "MetodoPagos",
                newName: "MetodoPago");

            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "MetodoPago",
                newName: "TipoMetodoPago");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetodoPago",
                table: "MetodoPago",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Alquilers_MetodoPago_MetodoPagoID",
                table: "Alquilers",
                column: "MetodoPagoID",
                principalTable: "MetodoPago",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
