using Humanizer;
using OneHope.Shared.CompraDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHope.UT.ComprasController_test
{
    public class PostCompras_test : OneHope4SqliteUT
    {
        public PostCompras_test() 
        {
            var rams = new List<Ram>()
            {
                new Ram("12Gb"),
                new Ram("8Gb"),
            };

            var procesadores = new List<Procesador>()
            {
                new Procesador("Snapdragon 888+"),
                new Procesador("Intel-Core i7"),
            };

            var marcas = new List<Marca>()
            {
                new Marca("Acer"),
                new Marca("Samsung"),
            };

            var portatiles = new List<Portatil>()
            {
                new Portatil(1, "5", procesadores[0], rams[1], marcas[0], "Aspire", 248.36, 24.99, 100, 24, 10, new Proveedor(1, "ChinaSA", "45", "C/Maravillas", "c@", "125")),
                new Portatil(2, "3", procesadores[1], rams[0], marcas[1], "Galaxy", 850.99, 49.99, 499.85, 17, 2, new Proveedor(2, "USA", "46", "C/Patria", "eeuu@", "258")),
            };

            var compra = new Compra(1, 1, DateTime.Today, "C/Expedición", TipoMetodoPago.TarjetaCredito, 248.36, "Jose", "García");

            var lineasCompra = new LineaCompra(portatiles[1], 1, compra);
            compra.LineasCompra.Add(lineasCompra);

            _context.AddRange(rams);
            _context.AddRange(procesadores);
            _context.AddRange(marcas);
            _context.AddRange(portatiles);
            _context.Add(compra);
            _context.SaveChanges();
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetCompra_NotFound_test()
        {
            //Arrange
            var mock = new Mock<ILogger<ComprasController>>();
            ILogger<ComprasController> logger = mock.Object;

            var controller = new ComprasController(_context, logger);

            //Act
            var result = await controller.GetCompra(0);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("LevelTesting","Unit Testing")]
        public async Task GetCompras_Found_test()
        {
            //Arrange
            var mock = new Mock<ILogger<ComprasController>>();
            ILogger<ComprasController> logger = mock.Object;
            var controller = new ComprasController(_context, logger);

            var expectedPurchase = new DetallesCompraDTO(1, "Jose", "García", "C/Expedición", new List<LineaCompraDTO>(),
                Shared.TipoMetodoPago.TarjetaCredito, DateTime.Today);

            expectedPurchase.LineasCompra.Add(new LineaCompraDTO(2, "Galaxy", 850.99, "Samsung", "Intel-Core i7", "12Gb", 1));

            //Act
            var result = await controller.GetCompra(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var purchaseDTOActual = Assert.IsType<DetallesCompraDTO>(okResult.Value);

            Assert.Equal(expectedPurchase, purchaseDTOActual);

        }

        public static IEnumerable<object[]> TestCasesFor_CreatePurchase()
        {
            CompraPorCrearDTO compraNoCompraPortatiles = new CompraPorCrearDTO("C/Expedición", new List<LineaCompraDTO>(),
                "Jose", "García", Shared.TipoMetodoPago.TarjetaCredito);

            CompraPorCrearDTO compraPortatilNoExiste = new CompraPorCrearDTO("C/Expedición", new List<LineaCompraDTO>(),
               "Jose", "García", Shared.TipoMetodoPago.TarjetaCredito);
            compraPortatilNoExiste.LineasCompra.Add(new LineaCompraDTO(0, "Increible", 100, "HP", "Intel-Core i7", "12Gb", 2));

            CompraPorCrearDTO compraCantidadDemasiadoAlta = new CompraPorCrearDTO("C/Expedición", new List<LineaCompraDTO>(),
                "Jose", "García", Shared.TipoMetodoPago.TarjetaCredito);
            compraCantidadDemasiadoAlta.LineasCompra.Add(new LineaCompraDTO(1, "Aspire", 248.36, "Acer", "Snapdragon 888+", "8Gb", 1000000));

            var allTests = new List<object[]>
            {
                new object[] { compraNoCompraPortatiles, "Error! Debes incluir al menos un portátil para comprarlo", },
                new object[] { compraPortatilNoExiste, $"Error! El portátil con nombre {compraPortatilNoExiste.LineasCompra[0].Nombre} y con ID { compraPortatilNoExiste.LineasCompra[0].PortatilID} no existe en la base de datos.", },
                new object[] { compraCantidadDemasiadoAlta, $"Error! El portátil con nombre {compraCantidadDemasiadoAlta.LineasCompra[0].Nombre} solo tiene 24 unidades disponibles, pero has seleccionado {compraCantidadDemasiadoAlta.LineasCompra[0].Cantidad} unidades para comprar.", },
            };

            return allTests;
        }

        [Theory]
        [Trait("LevelTesting","Unit Testing")]
        [MemberData(nameof(TestCasesFor_CreatePurchase))]
        public async Task CrearCompra_Error_test(CompraPorCrearDTO? compraPorCrear, string errorExpected)
        {
            // Arrange
            var mock = new Mock<ILogger<ComprasController>>();
            ILogger<ComprasController> logger = mock.Object;

            var controller = new ComprasController(_context, logger);

            //Act
            var result = await controller.CrearCompra(compraPorCrear);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            //we check that the expected error message and actual are the same
            Assert.Equal(errorExpected, problemDetails.Errors.First().Value[0]);

        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task CrearCompra_Success_test()
        {
            //Arrange
            var mock = new Mock<ILogger<ComprasController>>();
            ILogger<ComprasController> logger = mock.Object;

            CompraPorCrearDTO compraPorCrear = new CompraPorCrearDTO("C/Expedición",
                new List<LineaCompraDTO>(), "Jose", "García", Shared.TipoMetodoPago.TarjetaCredito);

            compraPorCrear.LineasCompra.Add(new LineaCompraDTO(1, "Aspire", 248.36, "Acer", "Snapdragon 888+", "8Gb", 1));

            //we expected to have a new purchase in the database
            var expectedDetalleCompraDTO = new DetallesCompraDTO(2, "Jose", "García", "C/Expedición", new List<LineaCompraDTO>(),
                Shared.TipoMetodoPago.TarjetaCredito, DateTime.Today);

            expectedDetalleCompraDTO.LineasCompra.Add(new LineaCompraDTO(1, "Aspire", 248.36, "Acer", "Snapdragon 888+", "8Gb", 1));

            var controller = new ComprasController(_context, logger);

            //Act
            var result = await controller.CrearCompra(compraPorCrear);

            //Assert
            //we check that the response type CreatedAtAction and obtain the purchase created
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var purchaseDetailsActual = Assert.IsType<DetallesCompraDTO>(createdResult.Value);

            //we check that the expected and actual are the same
            Assert.Equal(expectedDetalleCompraDTO, purchaseDetailsActual);

        }
    }
}
