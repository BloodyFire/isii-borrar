using OneHope.Shared.DevolucionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHope.UT.DevolucionesController_test
{
    public class GetDevoluciones_test : OneHope4SqliteUT
    {

        public GetDevoluciones_test()
        {
            var procesadores = new List<Procesador>()
            {
                new Procesador("Intel I7 13700"),
                new Procesador("Pentium 4")
            };

            var rams = new List<Ram>() {

                new Ram("16Gb"),
                new Ram("32Gb"),
                new Ram("128Mb")
            };

            var marcas = new List<Marca>() {

                new Marca("HP"),
                new Marca("DELL"),
                new Marca("ASUS"),
                new Marca("TOASTER")
            };

            var proveedores = new List<Proveedor>() {
                new Proveedor(1, "Proveedores S.L.", "44444444A", "Calle providia numero 75", "proveemos@proveedores.com", "600000000"),
            };


            var portatiles = new List<Portatil>(){
                new Portatil(1, "ASUS-3314", procesadores[0], rams[1], marcas[2], "ASUS PRO STATION 3000", 2299.95, 76.66, 1150, 16, 5, proveedores[0] ),
                new Portatil(2, "HP-1151", procesadores[1], rams[2], marcas[0], "HP 486 del pleistoceno", 199.95, 6.66, 100, 13, 5, proveedores[0]),
                new Portatil(3, "TOASTER-3452", procesadores[0], rams[0], marcas[3], "Tostadora de otra era.", 2099.95, 70, 1050, 23, 3, proveedores[0] ),
                //este portatil no se puede devolver en la compra porque stock = 0
                new Portatil(4, "DELL-2274", procesadores[0], rams[2], marcas[1],"Portatil ultra fino 0.5cm de grosor. 2Kg", 1799.95, 50, 900,0, 4, proveedores[0]),
            };

            var compras = new List<Compra>()
            {
                new Compra (1, 3, new DateTime(2023, 10, 29), "c/Circunvalacion, nº 23", TipoMetodoPago.PayPal, 2499.90, "Nombre Generico","Apellido"),
                new Compra (2, 3, new DateTime(2023, 10, 30), "c/Circunvalacion, nº 23", TipoMetodoPago.PayPal, 2099.95, "Nombre Generico","Apellido"),
                new Compra (3, 8, new DateTime(2023, 10, 02), "c/Simon Abril, nº 8", TipoMetodoPago.Transferencia, 1799.85, "Ser Humano","Nada Sospechoso")
            };

            var lineasCompra = new List<LineaCompra>()
            {
                new LineaCompra(1, portatiles[0], compras[0], 1, 2299.95),
                new LineaCompra(2, portatiles[1], compras[0], 2, 199.95),
                new LineaCompra(3, portatiles[2], compras[1], 1, 2099.95),
                new LineaCompra(4, portatiles[2], compras[2], 1, 2099.95)

            };

            var devoluciones = new List<Devolucion>()
            {
                new Devolucion(1, new DateTime(2023, 11, 01), 599.95f, "c/Circunvalacion, nº23", "acercate sobre las 6", "quiero mayor capacidad")
            };

            var lineasDevolucion = new List<LineaDevolucion>()
            {
                new LineaDevolucion(1, 1, lineasCompra[1], devoluciones[0]),
                new LineaDevolucion(2, 1, lineasCompra[2], devoluciones[0])
            };

            _context.AddRange(procesadores);
            _context.AddRange(rams);
            _context.AddRange(marcas);
            _context.AddRange(proveedores);
            _context.AddRange(portatiles);
            _context.AddRange(compras);
            _context.AddRange(lineasCompra);
            _context.AddRange(devoluciones);
            _context.AddRange(lineasDevolucion);
            _context.SaveChanges(); //maybe async?

        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetDevolucion_NotFound_test()
        {
            // Arrange
            var mock = new Mock<ILogger<DevolucionesController>>();
            ILogger<DevolucionesController> logger = mock.Object;

            var controller = new DevolucionesController(_context, logger);

            // Act
            var result = await controller.GetDevolucion(0);

            //Assert
            //we check that the response type is OK and obtain the list of laptops
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetDevolucion_Found_test()
        {
            // Arrange
            var mock = new Mock<ILogger<DevolucionesController>>();
            ILogger<DevolucionesController> logger = mock.Object;
            var controller = new DevolucionesController(_context, logger);

            var expectedDevolucion = new DevolucionDetailDTO(1,"mas capacidad", "c/Circunvalacion, nº23", new DateTime(2023, 11, 01), new List<DevolucionItemDTO>(), "no estare hasta las 5");
            expectedDevolucion.LineasDevoluciones.Add(new DevolucionItemDTO(2, 1, "ASUS - 2371", 3, 4, 199.95));
            // Act
            var result = await controller.GetDevolucion(1);

            //Assert
            //we check that the response type is OK and obtain the list of movies
            var okResult = Assert.IsType<OkObjectResult>(result);
            var purchaseDTOActual = Assert.IsType<DevolucionDetailDTO>(okResult.Value);

            //we check that the expected and actual are the same
            Assert.Equal(expectedDevolucion, purchaseDTOActual);

        }
        
       
    }
}
