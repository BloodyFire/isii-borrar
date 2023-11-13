using OneHope.Shared.DevolucionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHope.UT.DevolucionesController_test
{
    public class PostDevoluciones_test : OneHope4SqliteUT
    {

        public PostDevoluciones_test()
        {

            var procesadores = new List<Procesador>()
            {
                new Procesador("Intel 80486"),
                new Procesador("Intel I5 14500"),
                new Procesador("Intel I7 13700"),
                new Procesador("Pentium 4")
            };

            var rams = new List<Ram>() {

                new Ram("4Gb"),
                new Ram("8Gb"),
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
                new Portatil(1, "ASUS-3314", procesadores[2], rams[1], marcas[2], "ASUS PRO STATION 3000", 2299.95, 76.66, 1150, 16, 5, proveedores[0] ),
                new Portatil(2, "HP-1151", procesadores[1], rams[3], marcas[0], "HP 486 del pleistoceno", 199.95, 6.66, 100, 13, 5, proveedores[0]),
                new Portatil(3, "TOASTER-3452", procesadores[3], rams[0], marcas[3], "Tostadora de otra era.", 2099.95, 70, 1050, 23, 3, proveedores[0] ),
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
                new Devolucion(1, DateTime.Now , 599.95f, "c/Circunvalacion, nº23", "acercate sobre las 6", "quiero mayor capacidad")
            };

            var lineasDevolucion = new List<LineaDevolucion>()
            {
                new LineaDevolucion(1, 1, lineasCompra[1], devoluciones[0]),
                //new LineaDevolucion(2, 1, lineasCompra[2], devoluciones[0])
            };

            _context.AddRange(procesadores);
            _context.AddRange(rams);
            _context.AddRange(marcas);
            _context.AddRange(portatiles);
            _context.AddRange(compras);
            _context.AddRange(lineasCompra);
            _context.AddRange(devoluciones);
            _context.AddRange(lineasDevolucion);
            _context.SaveChanges();
        }


        
        public static IEnumerable<object[]> TestCasesFor_CreateDevolucion()
        {
            DevolucionForCreateDTO devolucionSinNota = new DevolucionForCreateDTO( "quiero un portatil con mas capacidad", "c/Circunvalacion, nº23"
                 , new DateTime(2023, 11, 12), new List<DevolucionItemDTO>());
            devolucionSinNota.LineasDevoluciones.Add(new DevolucionItemDTO( 2, 1, "HP-1151", 3, 4, 199.95));

            DevolucionForCreateDTO devolucionConNota = new DevolucionForCreateDTO( "quiero un portatil con mas capacidad", "c/Circunvalacion, nº23"
                 , new DateTime(2023, 11, 12), new List<DevolucionItemDTO>(), "no estare hasta las 5");
            devolucionConNota.LineasDevoluciones.Add(new DevolucionItemDTO(2, 1, "HP-1151", 3, 4, 199.95));

            DevolucionDetailDTO expectedDevolucionSinNota = new DevolucionDetailDTO(2, "quiero un portatil con mas capacidad", "c/Circunvalacion, nº23"
                 , new DateTime(2023, 11, 12), new List<DevolucionItemDTO>());
            expectedDevolucionSinNota.LineasDevoluciones.Add(new DevolucionItemDTO(2, 1, "HP-1151", 3, 4, 199.95));

            DevolucionDetailDTO expectedDevolucionConNota = new DevolucionDetailDTO(2,"quiero un portatil con mas capacidad", "c/Circunvalacion, nº23"
                 , new DateTime(2023, 11, 12), new List<DevolucionItemDTO>(), "no estare hasta las 5");
            expectedDevolucionConNota.LineasDevoluciones.Add(new DevolucionItemDTO(2, 1, "HP-1151", 3, 4, 199.95));

            var allTests = new List<object[]>
            {
                new object[] { devolucionSinNota, expectedDevolucionSinNota },
                new object[] { devolucionConNota, expectedDevolucionConNota},
            };

            return allTests;
        }

        [Theory]
        [MemberData(nameof(TestCasesFor_CreateDevolucion))]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task CreateDevolucion_Success_test(DevolucionForCreateDTO devolucionParaCrear, DevolucionDetailDTO devolucionExpected)
        {
            // Arrange
            var mock = new Mock<ILogger<DevolucionesController>>();
            ILogger<DevolucionesController> logger = mock.Object;

            var controller = new DevolucionesController(_context, logger);

            // Act
            var result = await controller.CreateDevolucion(devolucionParaCrear);

            // Assert
            var okResult = Assert.IsType<CreatedAtActionResult>(result);
            var devolucionActual = Assert.IsType<DevolucionDetailDTO>(okResult.Value);

            // Check results
            Assert.Equal(devolucionExpected, devolucionActual);
        }

        


      
        public static IEnumerable<object[]> TestCasesFor_ErrorCreateDevolucion()
        {
            DevolucionForCreateDTO devolucionSinLineas = new DevolucionForCreateDTO( "la bateria dura poco", "c/ Rosario, nº2", DateTime.Now, new List<DevolucionItemDTO>());

            DevolucionForCreateDTO devolucionPortatilNoExiste = new DevolucionForCreateDTO("la bateria dura poco", "c/ Rosario, nº2", DateTime.Now, new List<DevolucionItemDTO>());
            devolucionPortatilNoExiste.LineasDevoluciones.Add(new DevolucionItemDTO(2, 1, "HP-1151", 3, 14, 199.95));

            DevolucionForCreateDTO devolucionMasPortatiles = new DevolucionForCreateDTO("la bateria dura poco", "c/ Rosario, nº2", DateTime.Now, new List<DevolucionItemDTO>());
            devolucionMasPortatiles.LineasDevoluciones.Add(new DevolucionItemDTO(2, 3, "HP-1151", 3, 4, 199.95));

            var allTests = new List<object[]>
            {
                new object[] { devolucionSinLineas, "Error: Tienes que incluir al menos un portatil en la devolucion.", },
                new object[] { devolucionPortatilNoExiste, $"Error: El portatil modelo {devolucionPortatilNoExiste.LineasDevoluciones[0].Modelo} con Id {devolucionPortatilNoExiste.LineasDevoluciones[0].IdPortatil} no existe en la base de datos.", },
                new object[] { devolucionMasPortatiles, $"Error! No puedes devolver mas portatiles de los comprados", }
            };

            return allTests;
        }


        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [MemberData(nameof(TestCasesFor_ErrorCreateDevolucion))]
        public async Task CreateDevolucion_Error_test(DevolucionForCreateDTO? devolucionDTO, string errorExpected)
        {
            // Arrange
            var mock = new Mock<ILogger<DevolucionesController>>();
            ILogger<DevolucionesController> logger = mock.Object;

            var controller = new DevolucionesController(_context, logger);

            // Act
            var result = await controller.CreateDevolucion(devolucionDTO);

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];

            //we check that the expected error message and actual are the same
            Assert.StartsWith(errorExpected, errorActual);




        }
    }

}