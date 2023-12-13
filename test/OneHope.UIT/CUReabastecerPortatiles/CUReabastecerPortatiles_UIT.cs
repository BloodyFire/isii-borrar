﻿using System;
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

            string expectedCabecera = "Resumen de la Pedido";
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
            // Pulsar pedir para finalizar la pedido.
            expectedFecha += DateTime.Now.ToString("dd-MM-yyyy HH:mm").ToUpper();
            crearPedido_PO.Pedir();
            // Ahora se debe haber mostrado la página de detalle y puedo comprobar si todo ha ido bien.

            // Assert
            Assert.True(_driver.PageSource.Contains(expectedCabecera));
            Assert.True(_driver.PageSource.Contains(expectedCodigoEmpleado));
            Assert.True(_driver.PageSource.Contains(expectedMetodoPago));
            Assert.True(_driver.PageSource.Contains(expectedFecha));
            Assert.True(_driver.PageSource.Contains(expectedDireccion));
            Assert.False(_driver.PageSource.Contains(unespectedField));
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

            string expectedCabecera = "Resumen de la Pedido";
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
            // Pulsar pedir para finalizar la pedido.
            expectedFecha += DateTime.Now.ToString("dd-MM-yyyy HH:mm").ToUpper();
            crearPedido_PO.Pedir();

            // Ahora se debe haber mostrado la página de detalle y puedo comprobar si todo ha ido bien.

            // Assert
            Assert.True(_driver.PageSource.Contains(expectedCabecera));
            Assert.True(_driver.PageSource.Contains(expectedCodigoEmpleado));
            Assert.True(_driver.PageSource.Contains(expectedMetodoPago));
            Assert.True(_driver.PageSource.Contains(expectedFecha));
            Assert.True(_driver.PageSource.Contains(expectedDireccion));
            Assert.True(_driver.PageSource.Contains(expectedObservaciones));
            Assert.True(detallePedido_PO.CompruebaListaPortatiles(expectedPortatiles));
            Assert.True(detallePedido_PO.CompruebaTotal(expectedTotal));

        }








    }
}
