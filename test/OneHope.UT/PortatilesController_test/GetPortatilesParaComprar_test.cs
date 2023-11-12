using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneHope.API.Controllers;
using OneHope.API.Models;
using OneHope.Shared.PortatilDTOs;

namespace OneHope.UT.PortatilesController_test
{
    public class GetPortatilesParaComprar_test : OneHope4SqliteUT
    {
        public GetPortatilesParaComprar_test()
        {
            var rams = new List<Ram>()
            {
                new Ram("12Gb"),
                new Ram("8Gb"),
                new Ram("3Gb"),
                new Ram("128Mb")
            };

            var procesadores = new List<Procesador>()
            {
                new Procesador("Snapdragon 888+"),
                new Procesador("Intel-Core i7"),
                new Procesador("AMD Ryzen 7"),
                new Procesador("Intel Pentium")
            };

            var marcas = new List<Marca>()
            {
                new Marca("Samsung"),
                new Marca("HP"),
                new Marca("Acer"),
                new Marca("Dell")
            };

            var portatiles = new List<Portatil>()
            {
                new Portatil(1, "5", procesadores[3], rams[2], marcas[2], "Aspire", 248.36, 24.99, 100, 24, 10, new Proveedor(1, "ChinaSA", "45", "C/Maravillas", "c@", "125")),
                new Portatil(2, "3", procesadores[1], rams[0], marcas[0], "Galaxy", 850.99, 49.99, 499.85, 17, 2, new Proveedor(2, "USA", "46", "C/Patria", "eeuu@", "258")),
                new Portatil(3, "1", procesadores[2], rams[1], marcas[3], "Wolf", 150.00, 29.99, 100.99, 40, 5, new Proveedor(3, "Japan", "87", "C/Sushi", "ja@", "800")),
                new Portatil(87, "8", procesadores[0], rams[3], marcas[1], "Notebook", 275.89, 57.00, 140.24, 78, 14, new Proveedor(4, "Spain", "70", "C/Mirador", "sp@", "200"))
            };

            //var compra = new Compra(1, 10, DateTime.Today, "C/Nueva Armonía", TipoMetodoPago.TarjetaCredito, 148.99);
            //var compraPortatil = new LineaCompra(portatiles[portatiles.Count-1], compra, 1, portatiles[portatiles.Count-1].PrecioCompra);
            // compra.LineasCompra.Add(compraPortatil);

            _context.AddRange(rams);
            _context.AddRange(procesadores);
            _context.AddRange(marcas);
            _context.AddRange(portatiles);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_GetPortatilesParaComprar()
        {
            var portatilDTOs = new List<PortatilParaComprarDTO>()
            {
                new PortatilParaComprarDTO(1, "5", 248.36, "3Gb", "Acer", "Aspire", "Intel Pentium", 24),
                new PortatilParaComprarDTO(2, "3", 850.99, "12Gb", "Samsung", "Galaxy", "Intel-Core i7", 17),
                new PortatilParaComprarDTO(3, "1", 150.00, "8Gb", "Dell", "Wolf", "AMD Ryzen 7", 40),
                new PortatilParaComprarDTO(87, "8", 275.89, "128Mb", "HP", "Notebook", "Snapdragon 888+", 78)
            };

            var portatilDTOsTC1 = new List<PortatilParaComprarDTO>()
            {
                portatilDTOs[0], portatilDTOs[1], portatilDTOs[2], portatilDTOs[3]
            }.OrderBy(p => p.Nombre).ToList();

            var portatilDTOsTC2 = new List<PortatilParaComprarDTO>() { portatilDTOs[0] };
            var portatilDTOsTC3 = new List<PortatilParaComprarDTO>() { portatilDTOs[2] };
            var portatilDTOsTC4 = new List<PortatilParaComprarDTO>() { portatilDTOs[3] };
            var portatilDTOsTC5 = new List<PortatilParaComprarDTO>() { portatilDTOs[1] };

            var allTests = new List<object[]>
            {
                new object[] { null, null, null, null, null, null, portatilDTOsTC1, },
                new object[] { "Asp", null, null, null, null, null, portatilDTOsTC2, },
                new object[] { null, "Dell", null, null, null, null, portatilDTOsTC3, },
                new object[] { null, null, "1", null, null, null, portatilDTOsTC3, },
                new object[] { null, null, null, "128Mb", null, null, portatilDTOsTC4, },
                new object[] { null, null, null, null, "Intel-Core i7", null, portatilDTOsTC5, },
                new object[] { null, null, null, null, null, 248.36, portatilDTOsTC2, },

            };

            return allTests;
        }

        [Theory]
        [MemberData(nameof(TestCasesFor_GetPortatilesParaComprar))]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetPortatilesParaComprar_testcase(string? filtrarNombre, string? filtrarMarca, string? filtrarModelo,
            string? filtrarRam, string? filtrarProcesador, double? filtrarPrecio,
            IList<PortatilParaComprarDTO> expectedPortatiles)
        {
            //Arrange
            var controller = new PortatilesController(_context, null);

            //Act
            var result = await controller.GetPortatilesParaComprar(filtrarNombre, filtrarModelo, filtrarMarca, filtrarProcesador, filtrarRam, filtrarPrecio);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var portatilDTOsActual = Assert.IsType<List<PortatilParaComprarDTO>>(okResult.Value);

            //We check that the expected and actual are the same
            Assert.Equal(expectedPortatiles, portatilDTOsActual);
        }
    }

}

