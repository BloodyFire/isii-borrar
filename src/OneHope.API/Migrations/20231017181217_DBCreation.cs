using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneHope.API.Migrations
{
    /// <inheritdoc />
    public partial class DBCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alquileres",
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
                    MetodoPago = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alquileres", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    NombreCliente = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetodosPagos = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devoluciones",
                columns: table => new
                {
                    IdDevolucion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<float>(type: "real", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reseña = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devoluciones", x => x.IdDevolucion);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreMarca = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.Id);
                    table.UniqueConstraint("AK_Marcas_NombreMarca", x => x.NombreMarca);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<double>(type: "float", nullable: false),
                    FechaPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CódigoEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoMetodoPago = table.Column<int>(type: "int", nullable: false),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Procesadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModeloProcesador = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procesadores", x => x.Id);
                    table.UniqueConstraint("AK_Procesadores_ModeloProcesador", x => x.ModeloProcesador);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CIF = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                    table.UniqueConstraint("AK_Proveedores_CIF", x => x.CIF);
                });

            migrationBuilder.CreateTable(
                name: "Rams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacidad = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rams", x => x.Id);
                    table.UniqueConstraint("AK_Rams_Capacidad", x => x.Capacidad);
                });

            migrationBuilder.CreateTable(
                name: "Portatiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modelo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrecioCompra = table.Column<double>(type: "float", nullable: false),
                    RamId = table.Column<int>(type: "int", nullable: false),
                    PrecioAlquiler = table.Column<double>(type: "float", nullable: false),
                    MarcaId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PrecioCoste = table.Column<double>(type: "float", nullable: false),
                    ProcesadorId = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    StockAlquilar = table.Column<int>(type: "int", nullable: false),
                    ProveedorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portatiles", x => x.Id);
                    table.UniqueConstraint("AK_Portatiles_Modelo", x => x.Modelo);
                    table.ForeignKey(
                        name: "FK_Portatiles_Marcas_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Portatiles_Procesadores_ProcesadorId",
                        column: x => x.ProcesadorId,
                        principalTable: "Procesadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Portatiles_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Portatiles_Rams_RamId",
                        column: x => x.RamId,
                        principalTable: "Rams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineaAlquiler",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortatilID = table.Column<int>(type: "int", nullable: false),
                    PortatilPrecioAlquiler = table.Column<double>(type: "float", nullable: false),
                    AlquilerID = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineaAlquiler", x => x.ID);
                    table.UniqueConstraint("AK_LineaAlquiler_AlquilerID_PortatilID", x => new { x.AlquilerID, x.PortatilID });
                    table.ForeignKey(
                        name: "FK_LineaAlquiler_Alquileres_AlquilerID",
                        column: x => x.AlquilerID,
                        principalTable: "Alquileres",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineaAlquiler_Portatiles_PortatilID",
                        column: x => x.PortatilID,
                        principalTable: "Portatiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineaCompra",
                columns: table => new
                {
                    IdLinea = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortatilId = table.Column<int>(type: "int", nullable: false),
                    IdProd = table.Column<int>(type: "int", nullable: false),
                    CompraId = table.Column<int>(type: "int", nullable: false),
                    IdCompra = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<double>(type: "float", nullable: false),
                    LineaCompraIdLinea = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineaCompra", x => x.IdLinea);
                    table.UniqueConstraint("AK_LineaCompra_IdProd_IdCompra", x => new { x.IdProd, x.IdCompra });
                    table.ForeignKey(
                        name: "FK_LineaCompra_Compras_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineaCompra_LineaCompra_LineaCompraIdLinea",
                        column: x => x.LineaCompraIdLinea,
                        principalTable: "LineaCompra",
                        principalColumn: "IdLinea");
                    table.ForeignKey(
                        name: "FK_LineaCompra_Portatiles_PortatilId",
                        column: x => x.PortatilId,
                        principalTable: "Portatiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineaPedido",
                columns: table => new
                {
                    PortatilId = table.Column<int>(type: "int", nullable: false),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineaPedido", x => new { x.PortatilId, x.PedidoId });
                    table.ForeignKey(
                        name: "FK_LineaPedido_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineaPedido_Portatiles_PortatilId",
                        column: x => x.PortatilId,
                        principalTable: "Portatiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineaDevolucion",
                columns: table => new
                {
                    IdLinea = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    LineaCompraId = table.Column<int>(type: "int", nullable: false),
                    DevolucionIdDevolucion = table.Column<int>(type: "int", nullable: false),
                    IdDevolucion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineaDevolucion", x => x.IdLinea);
                    table.UniqueConstraint("AK_LineaDevolucion_IdDevolucion_LineaCompraId", x => new { x.IdDevolucion, x.LineaCompraId });
                    table.ForeignKey(
                        name: "FK_LineaDevolucion_Devoluciones_DevolucionIdDevolucion",
                        column: x => x.DevolucionIdDevolucion,
                        principalTable: "Devoluciones",
                        principalColumn: "IdDevolucion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineaDevolucion_LineaCompra_LineaCompraId",
                        column: x => x.LineaCompraId,
                        principalTable: "LineaCompra",
                        principalColumn: "IdLinea",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineaAlquiler_PortatilID",
                table: "LineaAlquiler",
                column: "PortatilID");

            migrationBuilder.CreateIndex(
                name: "IX_LineaCompra_CompraId",
                table: "LineaCompra",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_LineaCompra_LineaCompraIdLinea",
                table: "LineaCompra",
                column: "LineaCompraIdLinea");

            migrationBuilder.CreateIndex(
                name: "IX_LineaCompra_PortatilId",
                table: "LineaCompra",
                column: "PortatilId");

            migrationBuilder.CreateIndex(
                name: "IX_LineaDevolucion_DevolucionIdDevolucion",
                table: "LineaDevolucion",
                column: "DevolucionIdDevolucion");

            migrationBuilder.CreateIndex(
                name: "IX_LineaDevolucion_LineaCompraId",
                table: "LineaDevolucion",
                column: "LineaCompraId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LineaPedido_PedidoId",
                table: "LineaPedido",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Portatiles_MarcaId",
                table: "Portatiles",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Portatiles_ProcesadorId",
                table: "Portatiles",
                column: "ProcesadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Portatiles_ProveedorId",
                table: "Portatiles",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Portatiles_RamId",
                table: "Portatiles",
                column: "RamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineaAlquiler");

            migrationBuilder.DropTable(
                name: "LineaDevolucion");

            migrationBuilder.DropTable(
                name: "LineaPedido");

            migrationBuilder.DropTable(
                name: "Alquileres");

            migrationBuilder.DropTable(
                name: "Devoluciones");

            migrationBuilder.DropTable(
                name: "LineaCompra");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "Portatiles");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "Procesadores");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Rams");
        }
    }
}
