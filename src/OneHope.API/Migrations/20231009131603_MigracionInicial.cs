using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneHope.API.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MetodoPago",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoMetodoPago = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<int>(type: "int", nullable: true),
                    NumTC = table.Column<int>(type: "int", nullable: true),
                    CCV = table.Column<int>(type: "int", nullable: true),
                    FechaCaducidad = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NumeroCuenta = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetodoPago", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Procesadores",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procesadores", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RAMs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAMs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Alquilers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaAlquiler = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaInAlquiler = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinAlquiler = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<float>(type: "real", nullable: false),
                    NombreCliente = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApellidosCliente = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DireccionEnvio = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    EmailCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelefonoCliente = table.Column<int>(type: "int", nullable: true),
                    MetodoPagoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alquilers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Alquilers_MetodoPago_MetodoPagoID",
                        column: x => x.MetodoPagoID,
                        principalTable: "MetodoPago",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Portatiles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modelo = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    PrecioCompra = table.Column<float>(type: "real", nullable: false),
                    PrecioAlq = table.Column<float>(type: "real", nullable: false),
                    PrecioCoste = table.Column<float>(type: "real", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    StockAlq = table.Column<int>(type: "int", nullable: false),
                    ProcesadorID = table.Column<int>(type: "int", nullable: false),
                    MarcaID = table.Column<int>(type: "int", nullable: false),
                    RAMID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portatiles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Portatiles_Marcas_MarcaID",
                        column: x => x.MarcaID,
                        principalTable: "Marcas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Portatiles_Procesadores_ProcesadorID",
                        column: x => x.ProcesadorID,
                        principalTable: "Procesadores",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Portatiles_RAMs_RAMID",
                        column: x => x.RAMID,
                        principalTable: "RAMs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineasAlquiler",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortatilID = table.Column<int>(type: "int", nullable: false),
                    PortatilPrecioAlq = table.Column<float>(type: "real", nullable: false),
                    AlquilerID = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineasAlquiler", x => x.ID);
                    table.UniqueConstraint("AK_LineasAlquiler_AlquilerID_PortatilID", x => new { x.AlquilerID, x.PortatilID });
                    table.ForeignKey(
                        name: "FK_LineasAlquiler_Alquilers_AlquilerID",
                        column: x => x.AlquilerID,
                        principalTable: "Alquilers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineasAlquiler_Portatiles_PortatilID",
                        column: x => x.PortatilID,
                        principalTable: "Portatiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alquilers_MetodoPagoID",
                table: "Alquilers",
                column: "MetodoPagoID");

            migrationBuilder.CreateIndex(
                name: "IX_LineasAlquiler_PortatilID",
                table: "LineasAlquiler",
                column: "PortatilID");

            migrationBuilder.CreateIndex(
                name: "IX_Portatiles_MarcaID",
                table: "Portatiles",
                column: "MarcaID");

            migrationBuilder.CreateIndex(
                name: "IX_Portatiles_ProcesadorID",
                table: "Portatiles",
                column: "ProcesadorID");

            migrationBuilder.CreateIndex(
                name: "IX_Portatiles_RAMID",
                table: "Portatiles",
                column: "RAMID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineasAlquiler");

            migrationBuilder.DropTable(
                name: "Alquilers");

            migrationBuilder.DropTable(
                name: "Portatiles");

            migrationBuilder.DropTable(
                name: "MetodoPago");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "Procesadores");

            migrationBuilder.DropTable(
                name: "RAMs");
        }
    }
}
