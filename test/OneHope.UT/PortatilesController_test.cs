using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneHope.API.Controllers;
using OneHope.API.Models;
using OneHope.Shared.PortatilDTOs;

namespace OneHope.UT
{
    public class PortatilesController_test : OneHope4SqliteUT
    {
        public PortatilesController_test()
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
                new Marca("Lenovo")
            };

            var portatiles = new List<Portatil>()
            {
                new Portatil(1, "5", procesadores[3], rams[2], marcas[2], "Aspire", 248.36, 24.99, 100, 24, 10, new Proveedor(1, "China S.L", "200", "C/Xi", "a@","654")),
                new Portatil(2, "3", procesadores[1], rams[0], marcas[0], "Galaxy", 850.99, 49.99, 499.85, 17, 2, new Proveedor(2, "EEUU S.L", "458", "C/Monolito", "eeuu@","657")),
                new Portatil(3, "1", procesadores[2], rams[1], marcas[3], "Wolf", 150.00, 29.99, 100.99, 40, 5, new Proveedor(2, "UE S.L", "547", "C/Constitucion", "ue@","101")),
                new Portatil(87, "8", procesadores[0], rams[3], marcas[1], "Notebook", 275.89, 57.00, 140.24, 78, 14, new Proveedor(3, "Japan S.L", "689", "C/Toyota", "japan@","213"))
            };

            _context.AddRange(rams);
            _context.AddRange(procesadores);
            _context.AddRange(marcas);
            _context.AddRange(portatiles);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetPortatilesParaComprarNULL_test()
        {
            //Arrange
            IList<PortatilParaComprarDTO> expectedPortatiles = new List<PortatilParaComprarDTO>()
            {
                new PortatilParaComprarDTO(1, "5", 248.36, "3Gb", "Acer", "Aspire", "Intel Pentium", 24),
                new PortatilParaComprarDTO(2, "3", 850.99, "12Gb", "Samsung", "Galaxy", "Intel-Core i7", 17),
                new PortatilParaComprarDTO(3, "1", 150.00, "8Gb", "Lenovo", "Wolf", "AMD Ryzen 7", 40),
                new PortatilParaComprarDTO(87, "8", 275.89, "128Mb", "HP", "Notebook", "Snapdragon 888+", 78)
            };

            var controller = new PortatilesController(_context, null);

            //Act

            var result = await controller.GetPortatilesParaComprar(null, null, null, null, null);

            //Assert

            var okResult = Assert.IsType<OkObjectResult>(result);
            var portatilDTOsActual = Assert.IsType<List<PortatilParaComprarDTO>>(okResult.Value);

            Assert.Equal(expectedPortatiles, portatilDTOsActual);
        }
    }

}

