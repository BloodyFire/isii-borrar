namespace OneHope.UT.PedidosController_test
{
    public class CreatePedido_test : OneHope4SqliteUT
    {
        public CreatePedido_test()
        {
            var procesador = new Procesador("Intel I7 13700");
            var ram = new Ram("8Gb");
            var marca = new Marca("DELL");
            var proveedor = new Proveedor(1, "Portatiles Mayorista", "12345678T", "Poligono del unicornio, avenida de la purpurina numero 7. Narnia 02000", "pormay@yahoorespuestas.com", "999555666");

            var portatil = new Portatil(1, "TOASTER-1421", procesador, ram, marca, "Portatil de 82 pulgadas. (Para empotrar en la pared)", 1299.95, 43.33, 325.00, 15, 6, proveedor);
            _context.Add(procesador);
            _context.Add(ram);
            _context.Add(marca);
            _context.Add(proveedor);
            _context.Add(portatil);
            _context.SaveChanges(); //maybe async?
        }

        public static IEnumerable<object[]> TestCasesFor_SuccessCreatePedido()
        {
            PedidoParaCrearDTO pedidoSinComentarios = new PedidoParaCrearDTO("Calle falsa, 123. Ciudad de la piruleta.", new List<LineaPedidoDTO>(),
                "Daniel.Tomas", TipoMetodoPago.Transferencia);
            pedidoSinComentarios.LineasPedido.Add(new LineaPedidoDTO(1, "TOASTER", "TOASTER-1421", 325.0, 1));

            PedidoParaCrearDTO pedidoConComentarios = new PedidoParaCrearDTO("Calle falsa, 123. Ciudad de la piruleta.",
                new List<LineaPedidoDTO>(),
                 "Daniel.Tomas", TipoMetodoPago.Transferencia, "Tengo comentarios al respecto.");
            pedidoConComentarios.LineasPedido.Add(new LineaPedidoDTO(1, "TOASTER", "TOASTER-1421", 325.0, 1));

            DetallePedidoDTO expectedPedidoSinComentarios = new DetallePedidoDTO(1, "Calle falsa, 123. Ciudad de la piruleta.", new List<LineaPedidoDTO>(),
                "Daniel.Tomas", TipoMetodoPago.Transferencia, DateTime.Now);
            expectedPedidoSinComentarios.LineasPedido.Add(new LineaPedidoDTO(1, "TOASTER", "TOASTER-1421", 325.0, 1));

            DetallePedidoDTO expectedPedidoConComentarios = new DetallePedidoDTO(1, "Calle falsa, 123. Ciudad de la piruleta.", new List<LineaPedidoDTO>(),
                "Daniel.Tomas", TipoMetodoPago.Transferencia, DateTime.Now, "Tengo comentarios al respecto.");
            expectedPedidoConComentarios.LineasPedido.Add(new LineaPedidoDTO(1, "TOASTER", "TOASTER-1421", 325.0, 1));

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
            pedidoPortatilNoExiste.LineasPedido.Add(new LineaPedidoDTO(15, "HP", "NOEXISTE", 1325.0, 10));

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
