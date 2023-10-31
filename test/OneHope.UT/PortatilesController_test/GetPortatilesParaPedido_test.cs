using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OneHope.UT.PortatilesController_test
{
    public class GetPortatilesParaPedido_test : OneHope4SqliteUT
    {
        public GetPortatilesParaPedido_test()
        {
            var procesadores = new List<Procesador>() {
                new Procesador("Intel I7 13700"),
                new Procesador("Intel I5 14500"),
                new Procesador("Ryzen 3  "),
                new Procesador("Ryzen 5"),
                new Procesador("Intel 80486"),
                new Procesador("Pentium 4"),
                new Procesador("Snapdragon")
            };
            var rams = new List<Ram>() {
                new Ram("8Gb"),
                new Ram("4Gb"),
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
                new Proveedor(2, "Portatiles Mayorista ", "12345678T", "Poligono del unicornio, avenida de la purpurina numero 7. Narnia 02000", "pormay@yahoorespuestas.com", "999555666"),
                new Proveedor(3, "Empresa en quiebra S.A", "NOPE12345", "Calle del cierre numero 0", "nope@quiebra.com", "654908234"),
                new Proveedor(4, "Empresaurio genérico S.A.", "RA000000WR", "Wall street (detras de donde venden churros)", "empresaurio@rawr.com", "+34 600 700 800")
            };
            //TODO: Remove some Portatiles and keep just the needed ones for the test.
            var portatiles = new List<Portatil>()
            {                
                new Portatil(id: 1, modelo: "HP-1151", procesador: procesadores[4], ram: rams[0], marca: marcas[0], nombre: "HP 486 del pleistoceno", precioCompra: 199.95, precioAlquiler: 6.66, precioCoste: 11, stock: 13, stockAlquilar: 5, proveedor: proveedores[0]),
                new Portatil(2, "DELL-2222", procesadores[1], rams[1], marcas[1], "DELL I5 para ofimatica", 499.95, 16.66, 11, 29, 7, proveedores[1]),
                new Portatil(3, "ASUS-3314", procesadores[0], rams[2], marcas[2], "ASUS PRO STATION 3000", 2299.95, 76.66, 11, 16, 5, proveedores[3]),
                new Portatil(4, "TOASTER-4461", procesadores[5], rams[3], marcas[3], "Pentium 4 fiable de toda la vida", 1699.95, 56.66, 11, 5, 2, proveedores[0]),
                new Portatil(5, "HP-5132", procesadores[2], rams[4], marcas[0], "HP Quantum singstar edition plus", 1999.95, 66.66, 11, 9, 9, proveedores[1]),
                new Portatil(6, "DELL-1244", procesadores[3], rams[0], marcas[1], "DELL R5 gama alta", 1999.95, 66.66, 11, 18, 1, proveedores[3]),
                new Portatil(7, "ASUS-2371", procesadores[6], rams[1], marcas[2], "ASUS workstation con procesador de movil", 599.95, 20, 11, 24, 7, proveedores[0]),
                new Portatil(8, "TOASTER-3452", procesadores[4], rams[2], marcas[3], "Tostadora de otra era.", 2099.95, 70, 11, 23, 3, proveedores[1]),
                new Portatil(9, "HP-4124", procesadores[1], rams[3], marcas[0], "Intel HP pro ultra con lucecitas.", 1099.95, 36.66, 11, 6, 9, proveedores[3]),
                new Portatil(10, "DELL-5211", procesadores[0], rams[4], marcas[1], "DELL gaming pro. Sin luces. (Campaña sensibilizacion contra la epilepsia)", 1999.95, 66.66, 11, 17, 1, proveedores[0]),
                new Portatil(11, "ASUS-1362", procesadores[5], rams[0], marcas[2], "portatilote grandote marca asus perfecto para ir de camping.", 699.95, 23.33, 11, 15, 5, proveedores[1]),
                new Portatil(12, "TOASTER-2434", procesadores[2], rams[1], marcas[3], "GRILL BBQ 3000W GAMING", 899.95, 30, 11, 19, 0, proveedores[3]),
                new Portatil(13, "HP-3141", procesadores[3], rams[2], marcas[0], "HP intel core ryzen 5 workstation", 699.95, 23.33, 11, 6, 2, proveedores[0]),
                new Portatil(14, "DELL-4272", procesadores[6], rams[3], marcas[1], "DELL ultraluna portatil sin teclado fancy.", 1399.95, 46.66, 11, 22, 1, proveedores[1]),
                new Portatil(15, "ASUS-5354", procesadores[4], rams[4], marcas[2], "ASUS-tado GAMING pro", 1599.95, 53.33, 11, 21, 7, proveedores[3]),
                new Portatil(16, "TOASTER-1421", procesadores[1], rams[0], marcas[3], "Portatil de 82 pulgadas. (Para empotrar en la pared)", 1299.95, 43.33, 11, 15, 6, proveedores[0]),
                new Portatil(17, "HP-2112", procesadores[0], rams[1], marcas[0], "HP “delicias de la programación” ", 599.95, 20, 11, 7, 4, proveedores[1]),
                new Portatil(18, "DELL-3264", procesadores[5], rams[2], marcas[1], "Portatil DELL perfecto para IS2", 1099.95, 36.66, 11, 17, 1, proveedores[3]),
                new Portatil(19, "ASUS-4331", procesadores[2], rams[3], marcas[2], "Portatil ASUS UNICORN FORCE EXTREME +18", 1099.95, 36.66, 11, 21, 2, proveedores[0]),
                new Portatil(20, "TOASTER-5442", procesadores[3], rams[4], marcas[3], "Portatil Ultrafiable con SAIS incorporado. Levantar con las piernas, no con la espalda.", 899.95, 30, 11, 25, 3, proveedores[1]),
                new Portatil(21, "HP-1174", procesadores[6], rams[0], marcas[0], "HP edicion HP con varita mágica.", 299.95, 10, 11, 4, 10, proveedores[3]),
                new Portatil(22, "DELL-2251", procesadores[4], rams[1], marcas[1], "Portatil DELL no tan perfecto para IS2", 699.95, 23.33, 11, 20, 3, proveedores[0]),
                new Portatil(23, "ASUS-3322", procesadores[1], rams[2], marcas[2], "Portatil ASUS futurista sin pantalla", 1399.95, 46.66, 11, 9, 3, proveedores[1]),
                new Portatil(24, "TOASTER-4414", procesadores[0], rams[3], marcas[3], "TOASTER WORKSTATION Skynet edition", 799.95, 26.66, 11, 12, 2, proveedores[3]),
                new Portatil(25, "HP-5161", procesadores[5], rams[4], marcas[0], "Portatil HP resistente a golpes suaves.", 1099.95, 36.66, 11, 7, 9, proveedores[0]),
                new Portatil(26, "DELL-1232", procesadores[2], rams[0], marcas[1], "Portatil DELL ultraseguridad anti-hack. Sin tarjeta de red ni usb.", 2099.95, 70, 11, 18, 0, proveedores[1]),
                new Portatil(27, "ASUS-2344", procesadores[3], rams[1], marcas[2], "Portatil ASUS cúbico. Parece WALL-E.", 1899.95, 63.33, 11, 19, 10, proveedores[3]),
                new Portatil(28, "TOASTER-3471", procesadores[6], rams[2], marcas[3], "Portatil ultra. Especial para sacar la ingeniería.", 999.95, 33.33, 11, 2, 10, proveedores[0]),
                new Portatil(29, "HP-4152", procesadores[4], rams[3], marcas[0], "Portatil HP que sale en la peli de iron man.", 599.95, 20, 11, 1, 10, proveedores[1]),
                new Portatil(30, "DELL-5224", procesadores[1], rams[4], marcas[1], "¡OFERTA! Portatil DELL ¡OFERTA!", 999.95, 33.33, 11, 0, 5, proveedores[3]),
                new Portatil(31, "ASUS-1311", procesadores[0], rams[0], marcas[2], "DELL I7 32Tb 16Gb 3200 y muchos más numeros.", 299.95, 10, 11, 22, 5, proveedores[0]),
                new Portatil(32, "TOASTER-2462", procesadores[5], rams[1], marcas[3], "Portatil KONAMI. El teclado solo incluye las flechas de direccion, A y B.", 899.95, 30, 11, 28, 5, proveedores[1]),
                new Portatil(33, "HP-3134", procesadores[2], rams[2], marcas[0], "HP Visual Studio Enterprise", 1199.95, 40, 11, 13, 3, proveedores[3]),
                new Portatil(34, "DELL-4241", procesadores[3], rams[3], marcas[1], "Portatil DELL de apenas 12 años.", 999.95, 33.33, 11, 25, 10, proveedores[0]),
                new Portatil(35, "ASUS-5372", procesadores[6], rams[4], marcas[2], "Portatil reguapo así como negro con colorines.", 1999.95, 66.66, 11, 13, 4, proveedores[1]),
                new Portatil(36, "TOASTER-1454", procesadores[4], rams[0], marcas[3], "Toaster edicion cyberpunk. No incluye gráfica.", 1599.95, 53.33, 11, 29, 3, proveedores[3]),
                new Portatil(37, "HP-2121", procesadores[1], rams[1], marcas[0], "HP cream! ahora con el doble de crema y más chocolate.", 2299.95, 76.66, 11, 17, 9, proveedores[0]),
                new Portatil(38, "DELL-3212", procesadores[0], rams[2], marcas[1], "Portatil DELL supercompuglobalmeganet.", 1099.95, 36.66, 11, 5, 7, proveedores[1]),
                new Portatil(39, "ASUS-4364", procesadores[5], rams[3], marcas[2], "Portatil ASUS DojoMojoCasaHouse P4 especial hogar.", 1299.95, 43.33, 11, 0, 7, proveedores[3]),
                new Portatil(40, "TOASTER-5431", procesadores[2], rams[4], marcas[3], "Portatil edición limitada con brilli brilli", 2199.95, 73.33, 11, 20, 10, proveedores[0]),
                new Portatil(41, "HP-1142", procesadores[3], rams[0], marcas[0], "HP ultima generacion. Perfecto para facebook.", 1499.95, 50, 11, 16, 10, proveedores[1]),
                new Portatil(42, "DELL-2274", procesadores[6], rams[1], marcas[1], "Portatil ultra fino 0.5cm de grosor. 2Kg", 1799.95, 60, 11, 0, 4, proveedores[3]),
                new Portatil(43, "ASUS-3351", procesadores[4], rams[2], marcas[2], "Portatil ASUS pixie dust to neverland.", 1599.95, 53.33, 11, 11, 2, proveedores[0]),
                new Portatil(44, "TOASTER-4422", procesadores[1], rams[3], marcas[3], "All-In-One de 5 pulgadas", 199.95, 6.66, 11, 20, 6, proveedores[1]),
                new Portatil(45, "HP-5114", procesadores[0], rams[4], marcas[0], "Portatroll HP. Diseñado por y para twitterx", 1899.95, 63.33, 11, 4, 3, proveedores[3]),
                new Portatil(46, "DELL-1261", procesadores[5], rams[0], marcas[1], "HAL 9000", 1399.95, 46.66, 11, 2, 10, proveedores[0]),
                new Portatil(47, "ASUS-2332", procesadores[2], rams[1], marcas[2], "ASUS workstation 3500 plus con subscripcion a netflix de 3 dias.", 799.95, 26.66, 11, 30, 2, proveedores[1]),
                new Portatil(48, "TOASTER-3444", procesadores[3], rams[2], marcas[3], "Ultrabook no tan ultra y sin libro.", 1399.95, 46.66, 11, 24, 5, proveedores[3]),
                new Portatil(49, "HP-4171", procesadores[6], rams[3], marcas[0], "Portatil HP barato a más no poder", 299.95, 10, 11, 27, 9, proveedores[0]),
                new Portatil(50, "DELL-5252", procesadores[4], rams[4], marcas[1], "Portail DELL con ultron en el ssd.", 1699.95, 56.66, 11, 15, 7, proveedores[1])
            };
            _context.AddRange(procesadores);
            _context.AddRange(rams);
            _context.AddRange(marcas);
            _context.AddRange(proveedores);
            _context.AddRange(portatiles);
            _context.SaveChanges(); //maybe async?
        }

        [Fact]
        public async Task GetPortatilesParaPedido_ok_null()
        {
            var procesadores = new List<Procesador>() {
                new Procesador("Intel I7 13700"),
                new Procesador("Intel I5 14500"),
                new Procesador("Ryzen 3  "),
                new Procesador("Ryzen 5"),
                new Procesador("Intel 80486"),
                new Procesador("Pentium 4"),
                new Procesador("Snapdragon")
            };
            var rams = new List<Ram>() {
                new Ram("8Gb"),
                new Ram("4Gb"),
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
                new Proveedor(2, "Portatiles Mayorista ", "12345678T", "Poligono del unicornio, avenida de la purpurina numero 7. Narnia 02000", "pormay@yahoorespuestas.com", "999555666"),
                new Proveedor(3, "Empresa en quiebra S.A", "NOPE12345", "Calle del cierre numero 0", "nope@quiebra.com", "654908234"),
                new Proveedor(4, "Empresaurio genérico S.A.", "RA000000WR", "Wall street (detras de donde venden churros)", "empresaurio@rawr.com", "+34 600 700 800")
            };
            //TODO: Remove some Portatiles and keep just the needed ones for the test.
            var expectedPortatiles = new List<Portatil>()
            {
                new Portatil(id: 1, modelo: "HP-1151", procesador: procesadores[4], ram: rams[0], marca: marcas[0], nombre: "HP 486 del pleistoceno", precioCompra: 199.95, precioAlquiler: 6.66, precioCoste: 11, stock: 13, stockAlquilar: 5, proveedor: proveedores[0]),
                new Portatil(2, "DELL-2222", procesadores[1], rams[1], marcas[1], "DELL I5 para ofimatica", 499.95, 16.66, 11, 29, 7, proveedores[1]),
                new Portatil(3, "ASUS-3314", procesadores[0], rams[2], marcas[2], "ASUS PRO STATION 3000", 2299.95, 76.66, 11, 16, 5, proveedores[3]),
                new Portatil(4, "TOASTER-4461", procesadores[5], rams[3], marcas[3], "Pentium 4 fiable de toda la vida", 1699.95, 56.66, 11, 5, 2, proveedores[0]),
                new Portatil(5, "HP-5132", procesadores[2], rams[4], marcas[0], "HP Quantum singstar edition plus", 1999.95, 66.66, 11, 9, 9, proveedores[1]),
                new Portatil(6, "DELL-1244", procesadores[3], rams[0], marcas[1], "DELL R5 gama alta", 1999.95, 66.66, 11, 18, 1, proveedores[3]),
                new Portatil(7, "ASUS-2371", procesadores[6], rams[1], marcas[2], "ASUS workstation con procesador de movil", 599.95, 20, 11, 24, 7, proveedores[0]),
                new Portatil(8, "TOASTER-3452", procesadores[4], rams[2], marcas[3], "Tostadora de otra era.", 2099.95, 70, 11, 23, 3, proveedores[1]),
                new Portatil(9, "HP-4124", procesadores[1], rams[3], marcas[0], "Intel HP pro ultra con lucecitas.", 1099.95, 36.66, 11, 6, 9, proveedores[3]),
                new Portatil(10, "DELL-5211", procesadores[0], rams[4], marcas[1], "DELL gaming pro. Sin luces. (Campaña sensibilizacion contra la epilepsia)", 1999.95, 66.66, 11, 17, 1, proveedores[0]),
                new Portatil(11, "ASUS-1362", procesadores[5], rams[0], marcas[2], "portatilote grandote marca asus perfecto para ir de camping.", 699.95, 23.33, 11, 15, 5, proveedores[1]),
                new Portatil(12, "TOASTER-2434", procesadores[2], rams[1], marcas[3], "GRILL BBQ 3000W GAMING", 899.95, 30, 11, 19, 0, proveedores[3]),
                new Portatil(13, "HP-3141", procesadores[3], rams[2], marcas[0], "HP intel core ryzen 5 workstation", 699.95, 23.33, 11, 6, 2, proveedores[0]),
                new Portatil(14, "DELL-4272", procesadores[6], rams[3], marcas[1], "DELL ultraluna portatil sin teclado fancy.", 1399.95, 46.66, 11, 22, 1, proveedores[1]),
                new Portatil(15, "ASUS-5354", procesadores[4], rams[4], marcas[2], "ASUS-tado GAMING pro", 1599.95, 53.33, 11, 21, 7, proveedores[3]),
                new Portatil(16, "TOASTER-1421", procesadores[1], rams[0], marcas[3], "Portatil de 82 pulgadas. (Para empotrar en la pared)", 1299.95, 43.33, 11, 15, 6, proveedores[0]),
                new Portatil(17, "HP-2112", procesadores[0], rams[1], marcas[0], "HP “delicias de la programación” ", 599.95, 20, 11, 7, 4, proveedores[1]),
                new Portatil(18, "DELL-3264", procesadores[5], rams[2], marcas[1], "Portatil DELL perfecto para IS2", 1099.95, 36.66, 11, 17, 1, proveedores[3]),
                new Portatil(19, "ASUS-4331", procesadores[2], rams[3], marcas[2], "Portatil ASUS UNICORN FORCE EXTREME +18", 1099.95, 36.66, 11, 21, 2, proveedores[0]),
                new Portatil(20, "TOASTER-5442", procesadores[3], rams[4], marcas[3], "Portatil Ultrafiable con SAIS incorporado. Levantar con las piernas, no con la espalda.", 899.95, 30, 11, 25, 3, proveedores[1]),
                new Portatil(21, "HP-1174", procesadores[6], rams[0], marcas[0], "HP edicion HP con varita mágica.", 299.95, 10, 11, 4, 10, proveedores[3]),
                new Portatil(22, "DELL-2251", procesadores[4], rams[1], marcas[1], "Portatil DELL no tan perfecto para IS2", 699.95, 23.33, 11, 20, 3, proveedores[0]),
                new Portatil(23, "ASUS-3322", procesadores[1], rams[2], marcas[2], "Portatil ASUS futurista sin pantalla", 1399.95, 46.66, 11, 9, 3, proveedores[1]),
                new Portatil(24, "TOASTER-4414", procesadores[0], rams[3], marcas[3], "TOASTER WORKSTATION Skynet edition", 799.95, 26.66, 11, 12, 2, proveedores[3]),
                new Portatil(25, "HP-5161", procesadores[5], rams[4], marcas[0], "Portatil HP resistente a golpes suaves.", 1099.95, 36.66, 11, 7, 9, proveedores[0]),
                new Portatil(26, "DELL-1232", procesadores[2], rams[0], marcas[1], "Portatil DELL ultraseguridad anti-hack. Sin tarjeta de red ni usb.", 2099.95, 70, 11, 18, 0, proveedores[1]),
                new Portatil(27, "ASUS-2344", procesadores[3], rams[1], marcas[2], "Portatil ASUS cúbico. Parece WALL-E.", 1899.95, 63.33, 11, 19, 10, proveedores[3]),
                new Portatil(28, "TOASTER-3471", procesadores[6], rams[2], marcas[3], "Portatil ultra. Especial para sacar la ingeniería.", 999.95, 33.33, 11, 2, 10, proveedores[0]),
                new Portatil(29, "HP-4152", procesadores[4], rams[3], marcas[0], "Portatil HP que sale en la peli de iron man.", 599.95, 20, 11, 1, 10, proveedores[1]),
                new Portatil(30, "DELL-5224", procesadores[1], rams[4], marcas[1], "¡OFERTA! Portatil DELL ¡OFERTA!", 999.95, 33.33, 11, 0, 5, proveedores[3]),
                new Portatil(31, "ASUS-1311", procesadores[0], rams[0], marcas[2], "DELL I7 32Tb 16Gb 3200 y muchos más numeros.", 299.95, 10, 11, 22, 5, proveedores[0]),
                new Portatil(32, "TOASTER-2462", procesadores[5], rams[1], marcas[3], "Portatil KONAMI. El teclado solo incluye las flechas de direccion, A y B.", 899.95, 30, 11, 28, 5, proveedores[1]),
                new Portatil(33, "HP-3134", procesadores[2], rams[2], marcas[0], "HP Visual Studio Enterprise", 1199.95, 40, 11, 13, 3, proveedores[3]),
                new Portatil(34, "DELL-4241", procesadores[3], rams[3], marcas[1], "Portatil DELL de apenas 12 años.", 999.95, 33.33, 11, 25, 10, proveedores[0]),
                new Portatil(35, "ASUS-5372", procesadores[6], rams[4], marcas[2], "Portatil reguapo así como negro con colorines.", 1999.95, 66.66, 11, 13, 4, proveedores[1]),
                new Portatil(36, "TOASTER-1454", procesadores[4], rams[0], marcas[3], "Toaster edicion cyberpunk. No incluye gráfica.", 1599.95, 53.33, 11, 29, 3, proveedores[3]),
                new Portatil(37, "HP-2121", procesadores[1], rams[1], marcas[0], "HP cream! ahora con el doble de crema y más chocolate.", 2299.95, 76.66, 11, 17, 9, proveedores[0]),
                new Portatil(38, "DELL-3212", procesadores[0], rams[2], marcas[1], "Portatil DELL supercompuglobalmeganet.", 1099.95, 36.66, 11, 5, 7, proveedores[1]),
                new Portatil(39, "ASUS-4364", procesadores[5], rams[3], marcas[2], "Portatil ASUS DojoMojoCasaHouse P4 especial hogar.", 1299.95, 43.33, 11, 0, 7, proveedores[3]),
                new Portatil(40, "TOASTER-5431", procesadores[2], rams[4], marcas[3], "Portatil edición limitada con brilli brilli", 2199.95, 73.33, 11, 20, 10, proveedores[0]),
                new Portatil(41, "HP-1142", procesadores[3], rams[0], marcas[0], "HP ultima generacion. Perfecto para facebook.", 1499.95, 50, 11, 16, 10, proveedores[1]),
                new Portatil(42, "DELL-2274", procesadores[6], rams[1], marcas[1], "Portatil ultra fino 0.5cm de grosor. 2Kg", 1799.95, 60, 11, 0, 4, proveedores[3]),
                new Portatil(43, "ASUS-3351", procesadores[4], rams[2], marcas[2], "Portatil ASUS pixie dust to neverland.", 1599.95, 53.33, 11, 11, 2, proveedores[0]),
                new Portatil(44, "TOASTER-4422", procesadores[1], rams[3], marcas[3], "All-In-One de 5 pulgadas", 199.95, 6.66, 11, 20, 6, proveedores[1]),
                new Portatil(45, "HP-5114", procesadores[0], rams[4], marcas[0], "Portatroll HP. Diseñado por y para twitterx", 1899.95, 63.33, 11, 4, 3, proveedores[3]),
                new Portatil(46, "DELL-1261", procesadores[5], rams[0], marcas[1], "HAL 9000", 1399.95, 46.66, 11, 2, 10, proveedores[0]),
                new Portatil(47, "ASUS-2332", procesadores[2], rams[1], marcas[2], "ASUS workstation 3500 plus con subscripcion a netflix de 3 dias.", 799.95, 26.66, 11, 30, 2, proveedores[1]),
                new Portatil(48, "TOASTER-3444", procesadores[3], rams[2], marcas[3], "Ultrabook no tan ultra y sin libro.", 1399.95, 46.66, 11, 24, 5, proveedores[3]),
                new Portatil(49, "HP-4171", procesadores[6], rams[3], marcas[0], "Portatil HP barato a más no poder", 299.95, 10, 11, 27, 9, proveedores[0]),
                new Portatil(50, "DELL-5252", procesadores[4], rams[4], marcas[1], "Portail DELL con ultron en el ssd.", 1699.95, 56.66, 11, 15, 7, proveedores[1])
            }
            .OrderBy(p => p.Stock).Select(p => new PortatilParaPedidoDTO(p.Id, p.Modelo, p.Marca.NombreMarca, p.Stock, p.PrecioCoste, p.Proveedor.Nombre)).ToList();


            //ILogger<PortatilesController> logger = mock.Object;

            PortatilesController portatilesController = new PortatilesController(_context, null);
            var result = await portatilesController.GetPortatilesParaPedido(null,null,null,null,null);

            //Assert
            var okresult = Assert.IsType<OkObjectResult>(result);
            var actualPortatiles = Assert.IsType<List<PortatilParaPedidoDTO>>(okresult.Value);

            Assert.Equal<PortatilParaPedidoDTO>(expectedPortatiles, actualPortatiles);

        }
    }
}
