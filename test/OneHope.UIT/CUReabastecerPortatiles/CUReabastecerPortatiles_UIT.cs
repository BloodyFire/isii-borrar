﻿using Newtonsoft.Json;
using OneHope.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHope.UIT.CUReabastecerPortatiles
{

    public class CUReabastecerPortatiles_UIT : IDisposable
    {

        IWebDriver _driver;
        string _URI;
        private readonly ITestOutputHelper _output;

        public CUReabastecerPortatiles_UIT(ITestOutputHelper output)
        {
            //it is needed to run the browser and
            //know the URI of your app
            UtilitiesUIT.SetUp_UIT(out _driver, out _URI);
            //it is initialized using the logger provided by xUnit
            this._output = output;
        }

        void IDisposable.Dispose()
        {
            _driver.Close();
            _driver.Dispose();
            GC.SuppressFinalize(this);
        }

        // Navegar a la página inicial de la web.
        private void Inicio()
        {
            _driver.Navigate()
                .GoToUrl(_URI);
        }

        private void Ir_A_ReabastecerPortatiles()
        {
            // Esperar a que se cargue la página.
            UtilitiesUIT.WaitForBeingVisible(_driver, By.Id("PedirPortatilesNavLink"));
            // Pulsamos en la opción del menú de navegación de Reabastecer Portátiles.
            _driver.FindElement(By.Id("PedirPortatilesNavLink")).Click();
        }

        [Fact]
        public void Initial_step_opening_the_web_page()
        {
            //Arrange
            string expectedTitle = "Index";
            string expectedText = "Register";
            //Act
            Inicio();

            //Assert
            //Comprueba que el título coincide con el esperado
            Assert.Equal(expectedTitle, _driver.Title);

            //Comprueba si la página contiene el string indicado
            Assert.Contains(expectedText, _driver.PageSource);
        }

        [Fact]
        public void CU1_1_Flujo_Basico()
        {
            // Arrange
            var seleccionarPortatiles_PO = new SeleccionPortatilesPedido_PO(_driver, _output);
            var crearPedido_PO = new CrearPedido_PO(_driver, _output);
            var detallePedido_PO = new DetallePedido_PO(_driver, _output);

            string expectedCabecera = "Resumen del Pedido";
            string expectedCodigoEmpleado = "<b>Codigo de empleado:</b> Daniel.Tomas";
            string expectedMetodoPago = "<b>Método de Pago:</b> TarjetaCredito";
            string expectedFecha = "<b>Fecha de pedido:</b> ";
            string expectedDireccion = "<b>Dirección:</b> C/IS2, S/N\r\n02000\r\nAlbacete (Albacete)";
            string unespectedField = "<b>Observaciones:</b>";

            var expectedPortatiles = new List<string[]>
            {
                new string[] { "TOASTER", "TOASTER-4461", "850", "4", "3400" },
                new string[] { "DELL", "DELL-5211", "1000", "2", "2000" }
            };

            var expectedTotal = new List<string[]>
            {
                new string[] { "Precio Total:", "5400" }
            };

            // Act
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Reabastecer Portátiles.
            Ir_A_ReabastecerPortatiles();
            // Seleccionar los portátiles 4 y 10.
            seleccionarPortatiles_PO.SeleccionarPortatiles(new List<string>() { "4", "10" });
            // Pulsar el botón de pedir.
            seleccionarPortatiles_PO.Pedir();
            // Rellenar dirección de entrega de pago Tarjta.
            crearPedido_PO.setDireccion($"C/IS2, S/N{Keys.Enter}02000{Keys.Enter}Albacete (Albacete)");
            // Seleccionar como método de pago Tarjta.
            crearPedido_PO.setMetodoPago("TarjetaCredito");
            // Poner las cantidades.
            crearPedido_PO.setCantidad("4", "4");
            crearPedido_PO.setCantidad("10", "2");
            // Pulsar Realizar pedido para finalizar el pedido.
            expectedFecha += DateTime.Now.ToString("dd-MM-yyyy HH:mm").ToUpper();
            crearPedido_PO.Pedir();
            // Ahora se debe haber mostrado la página de detalle y puedo comprobar si todo ha ido bien.

            // Assert
            Assert.Contains(expectedCabecera,_driver.PageSource);
            Assert.Contains(expectedCodigoEmpleado, _driver.PageSource);
            Assert.Contains(expectedMetodoPago, _driver.PageSource);
            Assert.Contains(expectedFecha, _driver.PageSource);
            Assert.Contains(expectedDireccion, _driver.PageSource);
            Assert.DoesNotContain(unespectedField, _driver.PageSource);
            Assert.True(detallePedido_PO.CompruebaListaPortatiles(expectedPortatiles));
            Assert.True(detallePedido_PO.CompruebaTotal(expectedTotal));

        }

        [Fact]
        public void CU1_1_Flujo_Basico_Observaciones()
        {
            // Arrange
            var seleccionarPortatiles_PO = new SeleccionPortatilesPedido_PO(_driver, _output);
            var crearPedido_PO = new CrearPedido_PO(_driver, _output);
            var detallePedido_PO = new DetallePedido_PO(_driver, _output);

            string expectedCabecera = "Resumen del Pedido";
            string expectedCodigoEmpleado = "<b>Codigo de empleado:</b> Daniel.Tomas";
            string expectedMetodoPago = "<b>Método de Pago:</b> TarjetaCredito";
            string expectedFecha = "<b>Fecha de pedido:</b> ";
            string expectedDireccion = "<b>Dirección:</b> C/IS2, S/N\r\n02000\r\nAlbacete (Albacete)";
            string expectedObservaciones = "<b>Observaciones:</b> Llamar a Antonio cuando llegue el pedido";

            var expectedPortatiles = new List<string[]>
            {
                new string[] { "TOASTER", "TOASTER-4461", "850", "4", "3400" },
                new string[] { "DELL", "DELL-5211", "1000", "2", "2000" }
            };

            var expectedTotal = new List<string[]>
            {
                new string[] { "Precio Total:", "5400" }
            };

            // Act
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Reabastecer Portátiles.
            Ir_A_ReabastecerPortatiles();
            // Seleccionar los portátiles 4 y 10.
            seleccionarPortatiles_PO.SeleccionarPortatiles(new List<string>() { "4", "10" });
            // Pulsar el botón de pedir.
            seleccionarPortatiles_PO.Pedir();
            // Rellenar dirección de entrega.
            crearPedido_PO.setDireccion($"C/IS2, S/N{Keys.Enter}02000{Keys.Enter}Albacete (Albacete)");
            // Seleccionar como método de pago Tarjta.
            crearPedido_PO.setMetodoPago("TarjetaCredito");
            // Rellenar Observaciones.
            crearPedido_PO.setComentarios("Llamar a Antonio cuando llegue el pedido");
            // Poner las cantidades.
            crearPedido_PO.setCantidad("4", "4");
            crearPedido_PO.setCantidad("10", "2");
            // Pulsar pedir para finalizar el pedido.
            expectedFecha += DateTime.Now.ToString("dd-MM-yyyy HH:mm").ToUpper();
            crearPedido_PO.Pedir();

            // Ahora se debe haber mostrado la página de detalle y puedo comprobar si todo ha ido bien.

            // Assert
            Assert.Contains(expectedCabecera, _driver.PageSource);
            Assert.Contains(expectedCodigoEmpleado, _driver.PageSource);
            Assert.Contains(expectedMetodoPago, _driver.PageSource);
            Assert.Contains(expectedFecha, _driver.PageSource);
            Assert.Contains(expectedDireccion, _driver.PageSource);
            Assert.Contains(expectedObservaciones, _driver.PageSource);
            Assert.True(detallePedido_PO.CompruebaListaPortatiles(expectedPortatiles));
            Assert.True(detallePedido_PO.CompruebaTotal(expectedTotal));

        }

        [Theory]
        //--------- (Modelo, Marca, StockMin, StockMax, Proveedor, Nombre, Portatiles)
        [InlineData(null, null, null, null, null, "Tostadora", "[[ \"TOASTER-3452\", \"TOASTER\", \"23\", \"1050\", \"Portatiles Mayorista\" ]]")]
        [InlineData("DELL-1244", null, null, null, null, null, "[[ \"DELL-1244\", \"DELL\", \"18\", \"1000\", \"Empresaurio genérico S.A.\" ]]")]
        [InlineData(null, "ASUS", null, null, null, null, "[[ \"ASUS-3314\", \"ASUS\", \"16\", \"1150\", \"Empresaurio genérico S.A.\"], [\"ASUS-2371\", \"ASUS\", \"24\", \"300\", \"Proveedores S.L.\" ]]")]
        [InlineData(null, null, 25, null, null, null, "[[ \"DELL-2222\", \"DELL\", \"29\", \"250\", \"Portatiles Mayorista\" ]]")]
        [InlineData(null, null, null, 0, null, null, "[[ \"DELL-5211\", \"DELL\", \"0\", \"1000\", \"Proveedores S.L.\" ]]")]
        [InlineData(null, null, null, null, "Portatiles Mayorista", null, "[[ \"HP-5132\", \"HP\", \"9\", \"1000\", \"Portatiles Mayorista\"], [\"TOASTER-3452\", \"TOASTER\", \"23\", \"1050\", \"Portatiles Mayorista\"], [\"DELL-2222\", \"DELL\", \"29\", \"250\", \"Portatiles Mayorista\" ]]")]
        public void CU1_2_FA0_Filtrado(string? portatilModelo, string? portatilMarca, int? portatilStockMinimo, int? portatilStockMaximo, string? portatilProveedor, string? portatilNombre, string portatilesJson)
        {
            // Arrange -----------
            var seleccionarPortatiles_PO = new SeleccionPortatilesPedido_PO(_driver, _output);
            //Utilizamos una estructura serializada en json para poder introducir un número variable de portátiles.
            var expectedPortatiles = JsonConvert.DeserializeObject<List<string[]>>(portatilesJson);
            
            // Act ---------
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Pedir Portátiles.
            Ir_A_ReabastecerPortatiles();
            // Filtrar los portátiles.
            seleccionarPortatiles_PO.FiltrarPortatiles(portatilModelo, portatilMarca, portatilStockMinimo, portatilStockMaximo, portatilProveedor, portatilNombre);
            
            // Assert ----------
            // Comprobar que la lista de portátiles que ha devuelto es la correcta.
            Assert.True(seleccionarPortatiles_PO.CompruebaListaPortatiles(expectedPortatiles));
        }

        [Fact]
        public void CU1_3_FA1_No_Hay_Portatiles()
        {
            // Arrange -----------
            var expectedText = "No hay portátiles que cumplan los criterios seleccionados.";
            var seleccionarPortatiles_PO = new SeleccionPortatilesPedido_PO(_driver, _output);
            
            // Act ---------
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Reabastecer Portátiles.
            Ir_A_ReabastecerPortatiles();
            // Filtrar los artículos.
            seleccionarPortatiles_PO.FiltrarPortatiles(null, null, null, null, null, "No hay ninguno");
            
            // Assert ----------
            // Comprobar que la lista de portátiles que ha devuelto es la correcta.
            Assert.Contains(expectedText, _driver.PageSource);
        }

        [Fact]
        public void CU1_4_FA2_Eliminar()
        {
            // Arrange -----------
            var seleccionarPortatiles_PO = new SeleccionPortatilesPedido_PO(_driver, _output);
            var crearPedido_PO = new CrearPedido_PO(_driver, _output);
            var expectedPortatiles = new List<string[]>
            {
                //CompruebaListaPortatiles() no comprueba la columna de cantidad en esta vista, asi que no la introducimos.
                new string[] { "TOASTER", "TOASTER-4461", "850", "850", "Eliminar" } 
            };

            var expectedTotal = new List<string[]>
            {
                new string[] { "Precio Total:", "850" },
                new string[] { "Volver\r\nRealizar Pedido" } //Los botones hay que ponerlos así o el CompruebaTotal() falla.
            };

            // Act ---------
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Reabastecer Portátiles.
            Ir_A_ReabastecerPortatiles();
            // Seleccionar los portátiles 4 y 10.
            seleccionarPortatiles_PO.SeleccionarPortatiles(new List<string>() { "4", "10" });
            // Pulsar el botón de pedir.
            seleccionarPortatiles_PO.Pedir();
            // Eliminar portatil 10
            crearPedido_PO.Eliminar("10");

            // Assert ----------
            // Comprobar que la lista de portátiles que ha devuelto es la correcta.
            Assert.True(crearPedido_PO.CompruebaListaPortatiles(expectedPortatiles));
            Assert.True(crearPedido_PO.CompruebaTotal(expectedTotal));
        }

        [Fact]
        public void CU1_5_FA3_Cantidad()
        {
            // Arrange -----------
            var seleccionarPortatiles_PO = new SeleccionPortatilesPedido_PO(_driver, _output);
            var crearPedido_PO = new CrearPedido_PO(_driver, _output);
            var expectedPortatiles = new List<string[]>
            {
                //CompruebaListaPortatiles() no comprueba la columna de cantidad en esta vista, asi que no la introducimos. Solo hay que comprobar que se actualizan los totales.
                new string[] { "HP", "HP-4124", "550", "2750", "Eliminar" },
                new string[] { "HP", "HP-5132", "1000", "10000", "Eliminar" }
            };

            var expectedTotal = new List<string[]>
            {
                new string[] { "Precio Total:", "12750" },
                new string[] { "Volver\r\nRealizar Pedido" } //Los botones hay que ponerlos así o el CompruebaTotal() falla.
            };

            // Act ---------
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Reabastecer Portátiles.
            Ir_A_ReabastecerPortatiles();
            // Seleccionar los portátiles 4 y 10.
            seleccionarPortatiles_PO.SeleccionarPortatiles(new List<string>() { "9", "5" });
            // Pulsar el botón de pedir.
            seleccionarPortatiles_PO.Pedir();
            // Poner las cantidades.
            crearPedido_PO.setCantidad("9", "5");
            crearPedido_PO.setCantidad("5", "10");

            // Assert ----------
            // Comprobar que la lista de portátiles que ha devuelto es la correcta.
            Assert.True(crearPedido_PO.CompruebaListaPortatiles(expectedPortatiles));
            Assert.True(crearPedido_PO.CompruebaTotal(expectedTotal));
        }

        [Fact]
        public void CU1_6_FA4_Carrito_Vacio()
        {
            // Arrange
            var seleccionarPortatiles_PO = new SeleccionPortatilesPedido_PO(_driver, _output);
            
            // Act
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Pedir Portátiles.
            Ir_A_ReabastecerPortatiles();
            
            // Assert
            Assert.False(seleccionarPortatiles_PO.isEnabledPedir());
        }

        [Fact]
        public void CU1_7_FA5_Datos_Obligatorios()
        {
            // Arrange
            var seleccionarPortatiles_PO = new SeleccionPortatilesPedido_PO(_driver, _output);
            var crearPedido_PO = new CrearPedido_PO(_driver, _output);
            string expectedError = "(*) Por favor, indica una dirección de entrega.";

            // Act
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Pedir Portátiles.
            Ir_A_ReabastecerPortatiles();
            // Seleccionar los portátiles 9 y 5.
            seleccionarPortatiles_PO.SeleccionarPortatiles(new List<string>() { "9" });
            // Pulsar el botón de pedir.
            seleccionarPortatiles_PO.Pedir();
            // Pulsar Realizar pedido para intentar finalizar el pedido.
            crearPedido_PO.Pedir();

            // Assert
            Assert.Contains(expectedError, _driver.PageSource);
        }

        [Fact]
        public void CU1_8_FA6_Volver_Atras()
        {
            // Arrange
            var seleccionarPortatiles_PO = new SeleccionPortatilesPedido_PO(_driver, _output);
            var crearPedido_PO = new CrearPedido_PO(_driver, _output);
            
            // Act
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Pedir Portátiles.
            Ir_A_ReabastecerPortatiles();
            // Seleccionar los portátiles 9 y 5.
            seleccionarPortatiles_PO.SeleccionarPortatiles(new List<string>() { "9", "5" });
            // Pulsar el botón de pedir.
            seleccionarPortatiles_PO.Pedir();
            // Ahora volver.
            crearPedido_PO.Volver();
            
            // Assert
            Assert.True(seleccionarPortatiles_PO.ComprobarSeleccionPortatiles(new List<string>() { "9", "5" }));
        }


    }
}
