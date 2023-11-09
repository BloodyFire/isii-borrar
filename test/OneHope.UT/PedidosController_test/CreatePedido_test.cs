namespace OneHope.UT.PedidosController_test
{
    public class CreatePedido_test : OneHope4SqliteUT
    {
        public CreatePedido_test()
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

        public static IEnumerable<object[]> TestCasesFor_SuccessCreatePedido()
        {
            PedidoParaCrearDTO pedidoSinComentarios = new PedidoParaCrearDTO("Calle falsa, 123. Ciudad de la piruleta.", new List<LineaPedidoDTO>(),
                "Daniel.Tomas", TipoMetodoPago.Transferencia);
            pedidoSinComentarios.LineasPedido.Add(new LineaPedidoDTO(4, "TOASTER-1421", 325.0, 1));

            PedidoParaCrearDTO pedidoConComentarios = new PedidoParaCrearDTO("Calle falsa, 123. Ciudad de la piruleta.",
                new List<LineaPedidoDTO>(),
                 "Daniel.Tomas", TipoMetodoPago.Transferencia, "Tengo comentarios al respecto.");
            pedidoConComentarios.LineasPedido.Add(new LineaPedidoDTO(4, "TOASTER-1421", 325.0, 1));

            DetallePedidoDTO expectedPedidoSinComentarios = new DetallePedidoDTO(1, "Calle falsa, 123. Ciudad de la piruleta.", new List<LineaPedidoDTO>(),
                "Daniel.Tomas", TipoMetodoPago.Transferencia, DateTime.Now);
            expectedPedidoSinComentarios.LineasPedido.Add(new LineaPedidoDTO(4, "TOASTER-1421", 325.0, 1));

            DetallePedidoDTO expectedPedidoConComentarios = new DetallePedidoDTO(1, "Calle falsa, 123. Ciudad de la piruleta.", new List<LineaPedidoDTO>(),
                "Daniel.Tomas", TipoMetodoPago.Transferencia, DateTime.Now, "Tengo comentarios al respecto.");
            expectedPedidoConComentarios.LineasPedido.Add(new LineaPedidoDTO(4, "TOASTER-1421", 325.0, 1));

            var allTests = new List<object[]>
            {
                new object[] { pedidoSinComentarios, expectedPedidoSinComentarios },
                new object[] { pedidoConComentarios, expectedPedidoConComentarios },
            };

            return allTests;
        }

        [Theory]
        [MemberData(nameof(TestCasesFor_SuccessCreatePedido))]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task CreatePedido_Success_test(PedidoParaCrearDTO pedidoParaCrear, DetallePedidoDTO pedidoExpected)
        {
            // Arrange
            var mock = new Mock<ILogger<PedidosController>>();
            ILogger<PedidosController> logger = mock.Object;

            var controller = new PedidosController(_context, logger);

            // Act
            var result = await controller.CreatePedido(pedidoParaCrear);

            // Assert
            var okResult = Assert.IsType<CreatedAtActionResult>(result);
            var pedidoActual = Assert.IsType<DetallePedidoDTO>(okResult.Value);

            // Check results
            Assert.Equal(pedidoExpected, pedidoActual);
        }

        public static IEnumerable<object[]> TestCasesFor_ErrorCreatePedido()
        {
            PedidoParaCrearDTO pedidoSinLineas = new PedidoParaCrearDTO("Calle falsa, 123. Ciudad de la piruleta.", new List<LineaPedidoDTO>(),
                "Daniel.Tomas", TipoMetodoPago.Transferencia);

            PedidoParaCrearDTO pedidoPortatilNoExiste = new PedidoParaCrearDTO("Calle falsa, 123. Ciudad de la piruleta.",
                new List<LineaPedidoDTO>(),
                 "Daniel.Tomas", TipoMetodoPago.Transferencia);
            pedidoPortatilNoExiste.LineasPedido.Add(new LineaPedidoDTO(15, "NOEXISTE", 1325.0, 10));

            var allTests = new List<object[]>
            {
                new object[] { pedidoSinLineas, "Error: Tienes que incluir al menos un portatil en el pedido.",  },
                new object[] { pedidoPortatilNoExiste, $"Error: El portatil modelo {pedidoPortatilNoExiste.LineasPedido[0].Modelo} con Id {pedidoPortatilNoExiste.LineasPedido[0].PortatilID} no existe en la base de datos.", },
            };

            return allTests;
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [MemberData(nameof(TestCasesFor_ErrorCreatePedido))]
        public async Task CreatePedido_Error_test(PedidoParaCrearDTO? pedidoParaCrear, string errorExpected)
        {
            // Arrange
            var mock = new Mock<ILogger<PedidosController>>();
            ILogger<PedidosController> logger = mock.Object;

            var controller = new PedidosController(_context, logger);

            // Act
            var result = await controller.CreatePedido(pedidoParaCrear);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            // Check error messages
            Assert.Equal(errorExpected, problemDetails.Errors.First().Value[0]);
        }
    }
}
