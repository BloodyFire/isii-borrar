namespace OneHope.UT.PedidosController_test
{
    public class GetPedido_test : OneHope4SqliteUT
    {
        public GetPedido_test()
        {
            var procesador = new Procesador("Intel I7 13700");
            var ram = new Ram("8Gb");
            var marca = new Marca("HP");
            var proveedor = new Proveedor(1, "Proveedores S.L.", "44444444A", "Calle providia numero 75", "proveemos@proveedor.com", "600000000");
            var portatil = new Portatil(id: 1, modelo: "HP-1151", procesador: procesador, ram: ram, marca: marca, nombre: "HP 486 del pleistoceno", precioCompra: 199.95, precioAlquiler: 6.66, precioCoste: 50.00, stock: 0, stockAlquilar: 5, proveedor: proveedor);
            var pedidos = new List<Pedido> {
                new Pedido(1, 50.0, new DateTime(1999, 1, 13), "Daniel.Tomas", "Calle falsa, 123. Ciudad de la piruleta.", new List<LineaPedido>(), TipoMetodoPago.Transferencia),
                new Pedido(2, 100.0, new DateTime(2020, 10, 10), "Daniel.Tomas", "Calle falsa, 123. Ciudad de la piruleta.", new List<LineaPedido>(), TipoMetodoPago.Transferencia, "Aqui un comentario.")
            };
            var linea1 = new LineaPedido(portatil, 1, pedidos[0], 50.0);
            pedidos[0].LineasPedido.Add(linea1);
            var linea2 = new LineaPedido(portatil, 2, pedidos[1], 50.0);
            pedidos[1].LineasPedido.Add(linea2);

            _context.Add(procesador);
            _context.Add(ram);
            _context.Add(marca);
            _context.Add(proveedor);
            _context.Add(portatil);
            _context.AddRange(pedidos);
            _context.SaveChanges(); //maybe async?
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetPedido_NotFound_test()
        {
            // Arrange
            var mock = new Mock<ILogger<PedidosController>>();
            ILogger<PedidosController> logger = mock.Object;

            var controller = new PedidosController(_context, logger);

            // Act
            var result = await controller.GetPedido(0);

            //Assert
            Assert.IsType<NotFoundResult>(result);

        }

        public static IEnumerable<object[]> TestCasesFor_SuccessGetPedido()
        {
            int pedidoSinComentarios = 1;
            int pedidoConComentarios = 2;

            DetallePedidoDTO expectedPedidoSinComentarios = new DetallePedidoDTO(1, "Calle falsa, 123. Ciudad de la piruleta.", new List<LineaPedidoDTO>(),
                "Daniel.Tomas", TipoMetodoPago.Transferencia, new DateTime(1999, 1, 13));
            expectedPedidoSinComentarios.LineasPedido.Add(new LineaPedidoDTO(1, "HP", "HP-1151", 50.0, 1));

            DetallePedidoDTO expectedPedidoConComentarios = new DetallePedidoDTO(2, "Calle falsa, 123. Ciudad de la piruleta.", new List<LineaPedidoDTO>(),
                "Daniel.Tomas", TipoMetodoPago.Transferencia, new DateTime(2020, 10, 10), "Aqui un comentario.");
            expectedPedidoConComentarios.LineasPedido.Add(new LineaPedidoDTO(1, "HP", "HP-1151", 50.0, 2));

            var allTests = new List<object[]>
            {
                new object[] { pedidoSinComentarios, expectedPedidoSinComentarios },
                new object[] { pedidoConComentarios, expectedPedidoConComentarios },
            };

            return allTests;
        }

        [Theory]
        [MemberData(nameof(TestCasesFor_SuccessGetPedido))]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetPedido_Success_test(int idPedido, DetallePedidoDTO pedidoExpected)
        {
            // Arrange
            var mock = new Mock<ILogger<PedidosController>>();
            ILogger<PedidosController> logger = mock.Object;

            var controller = new PedidosController(_context, logger);

            // Act
            var result = await controller.GetPedido(idPedido);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var pedidoActual = Assert.IsType<DetallePedidoDTO>(okResult.Value);

            // Check results
            Assert.Equal(pedidoExpected, pedidoActual);
        }
    }
}
