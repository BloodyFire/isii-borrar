using Microsoft.EntityFrameworkCore;
using OneHope.Shared.AlquilerDTOs;
using SQLitePCL;

namespace OneHope.UT.AlquileresController_test
{
    public class GetAlquiler_test : OneHope4SqliteUT
    {
        public GetAlquiler_test() 
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

            var alquiler = new Alquiler(1, DateTime.Now, DateTime.Today.AddDays(2), DateTime.Today.AddDays(5), 
                (float)portatiles[0].PrecioAlquiler * 3, "Juanito", "Golosinas", 
                "Avda. España s/n, Albacete 02071", "juanito@uclm.es", 0, 
                   OneHope.Shared.TipoMetodoPago.TarjetaCredito,
                    new List<LineaAlquiler>());
            alquiler.LineasAlquiler.Add(new LineaAlquiler(1, 2, portatiles[0], alquiler));

            _context.AddRange(procesadores);
            _context.AddRange(rams);
            _context.AddRange(marcas);
            _context.AddRange(proveedores);
            _context.AddRange(portatiles);
            _context.Add(alquiler);
            _context.SaveChanges();
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetAlquiler_NotFound_test()
        {
            // Arrange
            var mock = new Mock<ILogger<AlquileresController>>();
            ILogger<AlquileresController> logger = mock.Object;

            var controller = new AlquileresController(_context, logger);

            // Act
            var result = await controller.GetAlquiler(0);

            //Assert
            //we check that the response type is OK and obtain the list of movies
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetAlquiler_Found_test()
        {
            // Arrange
            var mock = new Mock<ILogger<AlquileresController>>();
            ILogger<AlquileresController> logger = mock.Object;
            var controller = new AlquileresController(_context, logger);


            var expectedAlquiler = new DetalleAlquilerDTO(id: 1, fechaAlquiler: DateTime.Now, emailCliente: "juanito@uclm.es", 
                nombreCliente: "Juanito", apellidosCliente: "Golosinas",
                        direccionEnvio: "Avda. España s/n, Albacete 02071", telefonoCliente: 0, tipoMetodoPago: Shared.TipoMetodoPago.TarjetaCredito,
                        fechaInAlquiler: DateTime.Today.AddDays(2), fechaFinAlquiler: DateTime.Today.AddDays(5),
                        lineasAlquiler: new List<LineaAlquilerDTO>()); 
            expectedAlquiler.LineasAlquiler.Add(new LineaAlquilerDTO( 1, 6.66, 2));

            // Act 
            var result = await controller.GetAlquiler(1);

            //Assert
            //we check that the response type is OK and obtain the rental
            var okResult = Assert.IsType<OkObjectResult>(result);
            var alquilerDTOActual = Assert.IsType<DetalleAlquilerDTO>(okResult.Value);

            //we check that the expected and actual are the same
            Assert.Equal(expectedAlquiler, alquilerDTOActual);

        }
    }
}
