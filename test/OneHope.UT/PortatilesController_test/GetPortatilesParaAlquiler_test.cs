namespace OneHope.UT.PortatilesController_test
{
    public class GetPortatilesParaAlquiler_test : OneHope4SqliteUT
    {
        public GetPortatilesParaAlquiler_test()
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

        [Fact]
        public async Task GetPortatilesParaAlquiler_ok_null()
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
            var expectedPortatiles = new List<Portatil>()
            {
                new Portatil(id: 1, modelo: "HP-2023", procesador: procesadores[1], ram: rams[0], marca: marcas[0], nombre: "HP 486 del pleistoceno", precioCompra: 199.95, precioAlquiler: 6.66, precioCoste: 50.00, stock: 0, stockAlquilar: 5, proveedor: proveedores[0]),
                new Portatil(2, "DELL-1244", procesadores[0], rams[1], marcas[0], "DELL R5 gama alta", 1999.95, 66.66, 500.00, 1, 1, proveedores[0]),
                new Portatil(3, "ASUS-1362", procesadores[1], rams[1], marcas[1], "portatil marca asus perfecto para ir de camping.", 699.95, 23.33, 175.00, 5, 5, proveedores[1]),
                new Portatil(4, "LAPTOP-2.0", procesadores[0], rams[0], marcas[1], "Portatil supremo.", 1299.95, 43.33, 325.00, 15, 6, proveedores[1])
            }
            .OrderBy(p => p.StockAlquilar).Select(p => new PortatilParaAlquilerDTO(p.Id, p.Modelo, p.Marca.NombreMarca, p.Procesador.ModeloProcesador, p.Ram.Capacidad, p.StockAlquilar, p.PrecioAlquiler)).ToList();

            PortatilesController portatilesController = new PortatilesController(_context, null);
            var result = await portatilesController.GetPortatilesParaAlquiler(null, null, null);

            //Assert
            var okresult = Assert.IsType<OkObjectResult>(result.Result);
            var actualPortatiles = Assert.IsType<List<PortatilParaAlquilerDTO>>(okresult.Value);

            Assert.Equal<PortatilParaAlquilerDTO>(expectedPortatiles, actualPortatiles);
        }

        public static IEnumerable<object[]> TestCasesFor_GetPortatilesParaAlquiler()
        {
            var portatilDTOs = new List<PortatilParaAlquilerDTO>() {
                new PortatilParaAlquilerDTO(id: 1, modelo: "HP-2023", marca:"HP", procesador: "Ryzen 5 2900", ram: "8Gb", stockAlquilar: 5, precioAlquiler: 6.66),
                new PortatilParaAlquilerDTO(2,"DELL-1244", "HP", "Intel I5 12500k", "16Gb", 1, 66.66),
                new PortatilParaAlquilerDTO(3, "ASUS-1362", "ASUS", "Ryzen 5 2900", "16Gb", 5, 23.33),
                new PortatilParaAlquilerDTO(4, "LAPTOP-2.0", "ASUS", "Intel I5 12500k", "8Gb", 6, 43.33)
            };

            var portatilDTOsTC1 = new List<PortatilParaAlquilerDTO>() { portatilDTOs[2], portatilDTOs[3] }
            .OrderBy(p => p.StockAlquilar).ToList(); //Marca ASUS

            var portatilDTOsTC2 = new List<PortatilParaAlquilerDTO>() { portatilDTOs[0], portatilDTOs[2] }
            .OrderBy(p => p.StockAlquilar).ToList(); //Procesador Ryzen 5 2900

            var portatilDTOsTC3 = new List<PortatilParaAlquilerDTO>() { portatilDTOs[1], portatilDTOs[2] } 
            .OrderBy(p => p.StockAlquilar).ToList(); //16Gb Ram

            var allTests = new List<object[]>
            {
                new object[] { "ASUS", null, null, portatilDTOsTC1 },
                new object[] { null, "Ryzen 5 2900", null, portatilDTOsTC2 },
                new object[] { null, null, "16Gb", portatilDTOsTC3 },
            };

            return allTests;
        }

        [Theory]
        [MemberData(nameof(TestCasesFor_GetPortatilesParaAlquiler))]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetPortatilesParaAlquiler_testcase(string? filtroMarca, string? filtroProcesador, string? filtroRam,
            IList<PortatilParaAlquilerDTO> expectedPortatiles)
        {
            // Arrange
            var controller = new PortatilesController(_context, null);

            // Act
            var result = await controller.GetPortatilesParaAlquiler(filtroMarca, filtroProcesador, filtroRam);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var portatilDTOsActual = Assert.IsType<List<PortatilParaAlquilerDTO>>(okResult.Value);

            //Check results
            Assert.Equal(expectedPortatiles, portatilDTOsActual);
        }


    }
}
