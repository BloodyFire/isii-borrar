﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneHope.API.Models;
using OneHope.Shared.PortatilDTOs;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHope.UT.PortatilesController_test
{
    public class GetPortatilesParaDevolver_test : OneHope4SqliteUT
    {
        public GetPortatilesParaDevolver_test()
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
                new Compra (1, 3, DateTime.Now.Date.AddDays(-1), "c/Circunvalacion, nº 23", TipoMetodoPago.PayPal, 2499.90, "Nombre Generico","Apellido"),
                new Compra (2, 3, DateTime.Now.Date, "c/Circunvalacion, nº 23", TipoMetodoPago.PayPal, 2099.95, "Nombre Generico","Apellido"),
                new Compra (3, 8, DateTime.Now.Date.AddDays(-31), "c/Simon Abril, nº 8", TipoMetodoPago.Transferencia, 1799.85, "Ser Humano","Nada Sospechoso")
            };

            var lineasCompra = new List<LineaCompra>()
            {
                new LineaCompra(1, portatiles[0], compras[0], 1, 2299.95),
                new LineaCompra(2, portatiles[1], compras[0], 2, 199.95),
                new LineaCompra(3, portatiles[2], compras[1], 1, 2099.95),
                new LineaCompra(4, portatiles[2], compras[2], 1, 2099.95)

            };

            _context.AddRange(procesadores);
            _context.AddRange(rams);
            _context.AddRange(marcas);
            _context.AddRange(portatiles);
            _context.AddRange(compras);
            _context.AddRange(lineasCompra);
            _context.SaveChanges();

        }


        public static IEnumerable<object[]> TestCasesPara_GetPortatilesParaDevolver()
        {
            DateTime defaultDate = DateTime.Now.Date.AddDays(-1);
            DateTime defaultDate2 = DateTime.Now.Date;

            var portatilDTOs = new List<PortatilesParaDevolverDTO>() {
                new PortatilesParaDevolverDTO(1, 1, 1, "ASUS", "ASUS-3314", 1, new DateTime(defaultDate.Year, defaultDate.Month, defaultDate.Day),  2299.95),
                new PortatilesParaDevolverDTO(1, 2, 2, "HP", "HP-1151", 2, new DateTime(defaultDate.Year, defaultDate.Month, defaultDate.Day), 199.95),
                new PortatilesParaDevolverDTO(2, 3, 3, "TOASTER", "TOASTER-3452", 1, new DateTime(defaultDate2.Year, defaultDate2.Month, defaultDate2.Day), 2099.95),
            }.OrderBy(p => p.FechaCompra).ToList();

            var portatilDTOsTC1 = new List<PortatilesParaDevolverDTO>() { portatilDTOs[0], portatilDTOs[1] }
                .OrderBy(p => p.FechaCompra).ToList();


            var portatilDTOsTC2 = new List<PortatilesParaDevolverDTO>() { portatilDTOs[2] };
            var portatilDTOsTC3 = new List<PortatilesParaDevolverDTO>() { };
            var portatilDTOsTC4 = new List<PortatilesParaDevolverDTO>() { portatilDTOs[0] };


            var allTests = new List<object[]>
            {             //filters to apply - expected laptops
                new object[] { null, null, 3, null, portatilDTOs },
                new object[] { 1, null, 3, null, portatilDTOsTC1 },
                new object[] { null, new DateTime(defaultDate2.Year, defaultDate2.Month, defaultDate2.Day), 3, null, portatilDTOsTC2 },
                new object[] { null, null, 8, null, portatilDTOsTC3 },
                new object[] {null, null, 3, 2099.95, portatilDTOsTC4 }
            };

            return allTests;
        }

        [Theory]
        [MemberData(nameof(TestCasesPara_GetPortatilesParaDevolver))]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetPortatilesParaDevolver_testcase(int? idCompra, DateTime? fecha, int CustomerId, double? precio,
            IList<PortatilesParaDevolverDTO> expectedPortatiles)
        {
            // Arrange
            var controller = new PortatilesController(_context, null);

            // Act
            var result = await controller.GetPortatilesParaDevolver(idCompra, fecha, CustomerId, precio);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var PortatilDTOsActual = Assert.IsType<List<PortatilesParaDevolverDTO>>(okResult.Value);

            //we check that the expected and actual are the same
            Assert.Equal(expectedPortatiles, PortatilDTOsActual);

        }



    }

}