using OneHope.Shared.AlquilerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHope.UT.AlquileresController_test
{
    public class PostAlquiler_test : OneHope4SqliteUT
    {
        public PostAlquiler_test() 
        {
            var procesadores = new List<Procesador>() {
                new Procesador("Intel I5 12500k"),
                new Procesador("Ryzen 5 2900")
            };
            var rams = new List<Ram>() {
                new Ram("8Gb"),
                new Ram("16Gb")
            };
            var marcas = new List<Marca>() {
                new Marca("HP"),
                new Marca("ASUS")
            };
            var proveedores = new List<Proveedor>() {
                new Proveedor(1, "Proveedores S.L.", "44444444A", "Calle providia numero 75", "proveemos@proveedores.com", "600000000"),
                new Proveedor(2, "Portatiles Mayorista", "12345678T", "Poligono del unicornio, avenida de la purpurina numero 7. Narnia 02000", "pormay@yahoorespuestas.com", "999555666")
            };
            //TODO: Remove some Portatiles and keep just the needed ones for the test.
            var portatiles = new List<Portatil>()
            {
                new Portatil(id: 1, modelo: "HP-2023", procesador: procesadores[1], ram: rams[0], marca: marcas[0], nombre: "HP 486 del pleistoceno", precioCompra: 199.95, precioAlquiler: 6.66, precioCoste: 50.00, stock: 0, stockAlquilar: 5, proveedor: proveedores[0]),
                new Portatil(2, "DELL-1244", procesadores[0], rams[1], marcas[0], "DELL R5 gama alta", 1999.95, 66.66, 500.00, 1, 1, proveedores[0]),
                new Portatil(3, "ASUS-1362", procesadores[1], rams[1], marcas[1], "portatil marca asus perfecto para ir de camping.", 699.95, 23.33, 175.00, 5, 5, proveedores[1]),
                new Portatil(4, "LAPTOP-2.0", procesadores[0], rams[0], marcas[1], "Portatil supremo.", 1299.95, 43.33, 325.00, 15, 6, proveedores[1])
            };

            _context.AddRange(procesadores);
            _context.AddRange(rams);
            _context.AddRange(marcas);
            _context.AddRange(proveedores);
            _context.AddRange(portatiles);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_CrearAlquiler()
        {
            var alquilerSinPortatil = new AlquilerParaCrearDTO(DateTime.Today.AddDays(2), DateTime.Today.AddDays(5),
                 "juanito@uclm.es", "Juanito", "Golosinas", "Avda. España s/n, Albacete 02071", null, new List<LineaAlquilerDTO>(),
                   Shared.TipoMetodoPago.TarjetaCredito);

            var lineasAlquiler = new List<LineaAlquilerDTO>() { new LineaAlquilerDTO( 2, 66.66, 1, "HP", "DELL-1244", "Intel I5 12500k", "16Gb") };

            var alquilerFechaInicioAntes = new AlquilerParaCrearDTO(DateTime.Today, DateTime.Today.AddDays(5),
                 "juanito@uclm.es", "Juanito", "Golosinas", "Avda. España s/n, Albacete 02071", null, lineasAlquiler,
                   Shared.TipoMetodoPago.TarjetaCredito);

            var alquilerAntesDeFrom = new AlquilerParaCrearDTO(DateTime.Today.AddDays(7), DateTime.Today.AddDays(5),
                 "juanito@uclm.es", "Juanito", "Golosinas", "Avda. España s/n, Albacete 02071", null, lineasAlquiler,
                   Shared.TipoMetodoPago.TarjetaCredito);

            var alquilerPortatilNoDisponible = new AlquilerParaCrearDTO(DateTime.Today.AddDays(2), DateTime.Today.AddDays(5),
                 "juanito@uclm.es", "Juanito", "Golosinas", "Avda. España s/n, Albacete 02071", null, 
                 new List<LineaAlquilerDTO>() { new LineaAlquilerDTO( 2, 66.66, 5, "HP", "DELL-1244", "Intel I5 12500k", "16Gb") },
                   Shared.TipoMetodoPago.TarjetaCredito);

            var allTest = new List<object[]> {
                new object[] { alquilerSinPortatil, "Error! Debes alquilar al menos un portatil", },
                new object[] { alquilerFechaInicioAntes, "Error! Tu fecha de inicio de alquiler no puede empezar ni hoy ni antes", },
                new object[] { alquilerAntesDeFrom, "Error! Tu fecha de fin de alquiler no puede acabar antes o el mismo dia que tu fecha de inicio", },
                new object[] { alquilerPortatilNoDisponible, "Error! Portatil con id '2' no puede ser alquilado desde", },
            };

            return allTest;
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [MemberData(nameof(TestCasesFor_CrearAlquiler))]
        public async Task CreateRental_Error_test(AlquilerParaCrearDTO alquilerDTO, string errorExpected)
        {
            // Arrange
            var mock = new Mock<ILogger<AlquileresController>>();
            ILogger<AlquileresController> logger = mock.Object;

            var controller = new AlquileresController(_context, logger);

            // Act
            var result = await controller.CrearAlquiler(alquilerDTO);

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];

            //we check that the expected error message and actual are the same
            Assert.StartsWith(errorExpected, errorActual);

        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task CreateRental_Success_test()
        {
            // Arrange
            var mock = new Mock<ILogger<AlquileresController>>();
            ILogger<AlquileresController> logger = mock.Object;

            var controller = new AlquileresController(_context, logger);

            var alquilerDTO = new AlquilerParaCrearDTO(DateTime.Today.AddDays(2), DateTime.Today.AddDays(5),
                "juanito@uclm.es", "Juanito", "Golosinas", "Avda. España s/n, Albacete 02071", 0,
                new List<LineaAlquilerDTO>() { new LineaAlquilerDTO(1, 6.66, 2, "HP", "HP-2023", "Ryzen 5 2900", "8Gb") }, Shared.TipoMetodoPago.TarjetaCredito);

            var expectedAlquiler = new DetalleAlquilerDTO(id: 1, fechaAlquiler: DateTime.Now, emailCliente: "juanito@uclm.es",
                nombreCliente: "Juanito", apellidosCliente: "Golosinas",
                        direccionEnvio: "Avda. España s/n, Albacete 02071", telefonoCliente: 0, tipoMetodoPago: Shared.TipoMetodoPago.TarjetaCredito,
                        fechaInAlquiler: DateTime.Today.AddDays(2), fechaFinAlquiler: DateTime.Today.AddDays(5),
                        lineasAlquiler: new List<LineaAlquilerDTO>());
            expectedAlquiler.LineasAlquiler.Add(new LineaAlquilerDTO(1, 6.66, 2, "HP", "HP-2023", "Ryzen 5 2900", "8Gb"));

            // Act
            var result = await controller.CrearAlquiler(alquilerDTO);

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualRentalDetailDTO = Assert.IsType<DetalleAlquilerDTO>(createdResult.Value);

            Assert.Equal(expectedAlquiler, actualRentalDetailDTO);

        }

    }
}
