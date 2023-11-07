namespace OneHope.UT.PortatilesController_test
{
    public class GetPortatilesParaPedido_test : OneHope4SqliteUT
    {
        public GetPortatilesParaPedido_test()
        {
            var procesadores = new List<Procesador>() {
                new Procesador("Intel I7 13700")
            };
            var rams = new List<Ram>() {
                new Ram("8Gb")
            };
            var marcas = new List<Marca>() {
                new Marca("HP"),
                new Marca("DELL")
            };
            var proveedores = new List<Proveedor>() {
                new Proveedor(1, "Proveedores S.L.", "44444444A", "Calle providia numero 75", "proveemos@proveedores.com", "600000000"),
                new Proveedor(2, "Portatiles Mayorista", "12345678T", "Poligono del unicornio, avenida de la purpurina numero 7. Narnia 02000", "pormay@yahoorespuestas.com", "999555666")
            };
            //TODO: Remove some Portatiles and keep just the needed ones for the test.
            var portatiles = new List<Portatil>()
            {                
                new Portatil(id: 1, modelo: "HP-1151", procesador: procesadores[0], ram: rams[0], marca: marcas[0], nombre: "HP 486 del pleistoceno", precioCompra: 199.95, precioAlquiler: 6.66, precioCoste: 50.00, stock: 0, stockAlquilar: 5, proveedor: proveedores[0]),
                new Portatil(2, "DELL-1244", procesadores[0], rams[0], marcas[1], "DELL R5 gama alta", 1999.95, 66.66, 500.00, 1, 1, proveedores[0]),
                new Portatil(3, "ASUS-1362", procesadores[0], rams[0], marcas[0], "portatilote grandote marca asus perfecto para ir de camping.", 699.95, 23.33, 175.00, 5, 5, proveedores[1]),
                new Portatil(4, "TOASTER-1421", procesadores[0], rams[0], marcas[1], "Portatil de 82 pulgadas. (Para empotrar en la pared)", 1299.95, 43.33, 325.00, 15, 6, proveedores[1])
            };
            _context.AddRange(procesadores);
            _context.AddRange(rams);
            _context.AddRange(marcas);
            _context.AddRange(proveedores);
            _context.AddRange(portatiles);
            _context.SaveChanges(); //maybe async?
        }

        [Fact]
        public async Task GetPortatilesParaPedido_ok_null()
        {
            var procesadores = new List<Procesador>() {
                new Procesador("Intel I7 13700")
            };
            var rams = new List<Ram>() {
                new Ram("8Gb")
            };
            var marcas = new List<Marca>() {
                new Marca("HP"),
                new Marca("DELL")
            };
            var proveedores = new List<Proveedor>() {
                new Proveedor(1, "Proveedores S.L.", "44444444A", "Calle providia numero 75", "proveemos@proveedores.com", "600000000"),
                new Proveedor(2, "Portatiles Mayorista", "12345678T", "Poligono del unicornio, avenida de la purpurina numero 7. Narnia 02000", "pormay@yahoorespuestas.com", "999555666")
            };
            //TODO: Remove some Portatiles and keep just the needed ones for the test.
            var expectedPortatiles = new List<Portatil>()
            {
                new Portatil(id: 1, modelo: "HP-1151", procesador: procesadores[0], ram: rams[0], marca: marcas[0], nombre: "HP 486 del pleistoceno", precioCompra: 199.95, precioAlquiler: 6.66, precioCoste: 50.00, stock: 0, stockAlquilar: 5, proveedor: proveedores[0]),
                new Portatil(2, "DELL-1244", procesadores[0], rams[0], marcas[1], "DELL R5 gama alta", 1999.95, 66.66, 500.00, 1, 1, proveedores[0]),
                new Portatil(3, "ASUS-1362", procesadores[0], rams[0], marcas[0], "portatilote grandote marca asus perfecto para ir de camping.", 699.95, 23.33, 175.00, 5, 5, proveedores[1]),
                new Portatil(4, "TOASTER-1421", procesadores[0], rams[0], marcas[1], "Portatil de 82 pulgadas. (Para empotrar en la pared)", 1299.95, 43.33, 325.00, 15, 6, proveedores[1])
            }
            .OrderBy(p => p.Stock).Select(p => new PortatilParaPedidoDTO(p.Id, p.Modelo, p.Marca.NombreMarca, p.Stock, p.PrecioCoste, p.Proveedor.Nombre)).ToList();


            //ILogger<PortatilesController> logger = mock.Object;

            PortatilesController portatilesController = new PortatilesController(_context, null);
            var result = await portatilesController.GetPortatilesParaPedido(null,null,null,null,null);

            //Assert
            var okresult = Assert.IsType<OkObjectResult>(result);
            var actualPortatiles = Assert.IsType<List<PortatilParaPedidoDTO>>(okresult.Value);

            Assert.Equal<PortatilParaPedidoDTO>(expectedPortatiles, actualPortatiles);

        }

        public static IEnumerable<object[]> TestCasesFor_GetPortatilesParaPedido()
        {

            var portatilDTOs = new List<PortatilParaPedidoDTO>() {
                new PortatilParaPedidoDTO(1,"HP-1151", "HP", 0, 50.0, "Proveedores S.L."),
                new PortatilParaPedidoDTO(2,"DELL-1244", "DELL", 1, 500.00, "Proveedores S.L."),
                new PortatilParaPedidoDTO(3, "ASUS-1362", "HP", 5, 175.00,"Portatiles Mayorista"),
                new PortatilParaPedidoDTO(4, "TOASTER-1421", "DELL", 15, 325.00, "Portatiles Mayorista")
            };

            var portatilDTOsTC1 = new List<PortatilParaPedidoDTO>() { portatilDTOs[0] };
            
            var portatilDTOsTC2 = new List<PortatilParaPedidoDTO>() { portatilDTOs[0], portatilDTOs[2] }
                .OrderBy(p => p.Stock).ToList();
            var portatilDTOsTC3 = new List<PortatilParaPedidoDTO>() { portatilDTOs[2], portatilDTOs[3] }
                .OrderBy(p => p.Stock).ToList();
            var portatilDTOsTC4 = new List<PortatilParaPedidoDTO>() { portatilDTOs[0], portatilDTOs[1] }
                .OrderBy(p => p.Stock).ToList();

            var allTests = new List<object[]>
            {
                new object[] { "1151", null, null, null, null, portatilDTOsTC1 },
                new object[] { null, "HP", null, null, null, portatilDTOsTC2 },
                new object[] { null, null, 5, null, null, portatilDTOsTC3 },
                new object[] { null, null, null, 3, null, portatilDTOsTC4 },
                new object[] { null, null, null, null, "Proveedores S.L.", portatilDTOsTC4 }
            };

            return allTests;
        }

        [Theory]
        [MemberData(nameof(TestCasesFor_GetPortatilesParaPedido))]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetPortatilesParaPedido_testcase(string? filtroModelo, string? filtroMarca, int? filtroStockMinimo, int? filtroStockMaximo, string? filtroProveedor,
            IList<PortatilParaPedidoDTO> expectedPortatiles)
        {
            // Arrange
            var controller = new PortatilesController(_context, null);

            // Act
            var result = await controller.GetPortatilesParaPedido(filtroModelo, filtroMarca, filtroStockMinimo, filtroStockMaximo, filtroProveedor);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var movieDTOsActual = Assert.IsType<List<PortatilParaPedidoDTO>>(okResult.Value);

            //Check results
            Assert.Equal(expectedPortatiles, movieDTOsActual);

        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetPortatilesParaPedido_badrequest_test()
        {
            // Arrange
            var mock = new Mock<ILogger<PortatilesController>>();
            ILogger<PortatilesController> logger = mock.Object;
            var controller = new PortatilesController(_context, logger);

            // Act
            var result = await controller.GetPortatilesParaPedido(null, null, 5, 1, null);
            //Assert
            //we check that the response type is OK and obtain the list of movies
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);
            var problem = problemDetails.Errors.First().Value[0];

            Assert.Equal("filtroStockMinimo debe ser menor que filtroStockMaximo", problem);
        }

    }
}
