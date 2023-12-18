using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace OneHope.UIT.CUComprarPortatil
{
    public class CUComprarPortatil_UIT : IDisposable
    {
        //Webdriver: A reference to the browser
        IWebDriver _driver;

        //A reference to the URI of the web page to test
        string _URI;

        //this may be used whenever some result should be printed in E
        private readonly ITestOutputHelper _output;

        //The code for your test Methods goes here
        public CUComprarPortatil_UIT(ITestOutputHelper output)
        {
            //it is needed to run the browser and
            //know the URI of your app
            UtilitiesUIT.SetUp_UIT(out _driver, out _URI);

            //it is initialized using the logger provided by xUnit
            this._output = output;
        }


        [Fact]
        public void Initial_step_opening_the_web_page2()
        {
            //Arrange
            string expectedTitle = "Index";
            string expectedText = "Register";

            //Act
            //El navegador cargará la URI indicada
            _driver.Navigate().GoToUrl(_URI);
            //Assert
            //Comprueba que el título coincide con el esperado
            Assert.Equal(expectedTitle, _driver.Title);
            //Comprueba si la página contiene el string indicado
            Assert.Contains(expectedText, _driver.PageSource);
        }

        // Navegar a la página inicial de la web.
        private void Inicio()
        {
            _driver.Navigate()
                .GoToUrl(_URI);
        }

        [Fact]
        public void CU1_1_Flujo_Basico()
        {
            // Arrange
            var seleccionarPortatiles_PO = new SeleccionarPortatilesCompra_PO(_driver, _output);
            var crearCompra_PO = new CrearCompra_PO(_driver, _output);
            var detalleCompra_PO = new DetalleCompra_PO(_driver, _output);

            string expectedNombre = "Jose";
            string expectedApellidos = "García Molina";
            string expectedDireccion = "C/San Francisco";
            string expectedMetodoPago = "<b>Método de Pago: </b> TarjetaCredito";
            string expectedFecha = "<b>Fecha de compra: </b> " + DateTime.Now.ToString("dd-MMM-yyyy").ToUpper();

            var expectedPortatiles = new List<string[]>();
            expectedPortatiles.Add(new string[] { "TOASTER", "All-In-One de 5 pulgadas", "32Gb", "Intel I5 14500", "1" });

            var expectedTotal = new List<string[]>();
            expectedTotal.Add(new string[] { "Precio Total", "199,95" });


            // Act
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Comprar Artículos.
            Ir_A_ComprarArticulos();
            // Seleccionar los artículos 3 y 5.
            seleccionarPortatiles_PO.SeleccionarPortatiles(new List<string>() { "44" });
            // Pulsar el botón de comprar.
            seleccionarPortatiles_PO.Comprar();

            //Seleccionar nombre
            crearCompra_PO.setNombre("Jose");
            //Seleccionar apellidos
            crearCompra_PO.setApellidos("García Molina");
            //Seleccionar dirección
            crearCompra_PO.setDireccion("C/San Francisco");
            // Seleccionar como método de pago PayPal.
            crearCompra_PO.setMetodoPago("TarjetaCredito");
            // Poner las cantidades.
            crearCompra_PO.setCantidad("44","1");
            // Pulsar comprar para finalizar la compra.
            crearCompra_PO.Comprar();
            // Ahora se debe haber mostrado la página de detalle y puedo comprobar si todo ha ido bien.

            UtilitiesUIT.WaitForBeingVisible(_driver, By.Id("Submit"));

            // Assert
            Assert.True(_driver.PageSource.Contains(expectedNombre));
            Assert.True(_driver.PageSource.Contains(expectedApellidos));
            Assert.True(_driver.PageSource.Contains(expectedDireccion));
            Assert.True(_driver.PageSource.Contains(expectedMetodoPago));
            Assert.True(_driver.PageSource.Contains(expectedFecha));

            Assert.True(detalleCompra_PO.CompruebaListaPortatiles(expectedPortatiles));
            Assert.True(detalleCompra_PO.CompruebaTotal(expectedTotal));

        }

        [Fact]
        public void CU1_2_FA0_No_Hay_Articulos()
        {
            // Arrange -----------
            var expectedText = "No hay portátiles que cumplan los criterios seleccionados.";
            var seleccionarPortatiles_PO = new SeleccionarPortatilesCompra_PO(_driver, _output);

            // Act ---------
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Comprar Artículos.
            Ir_A_ComprarArticulos();
            // Filtrar los artículos.
            seleccionarPortatiles_PO.FiltrarPortatiles("No hay ninguno","","","","0","0");

            UtilitiesUIT.WaitForBeingVisible(_driver, By.Id("buscarPortatiles"));

            // Assert ----------
            // Comprobar que la lista de artículos que ha devuelto es la correcta.
            Assert.Contains(expectedText, _driver.PageSource);
        }

        [Theory]
        //--------- (Nombre, Marca, Ram, Procesador, filtroPrecio, filtroStock)
        [InlineData("All-In-One de 5 pulgadas", "TOASTER", "TOASTER-4422", "32Gb", "Intel I5 14500", "199,95", "19", "pul", "TOASTER", "32Gb", "Intel I5 14500", "", "")]
        [InlineData("ASUS PRO STATION 3000", "ASUS", "ASUS-3314", "16Gb", "Intel I7 13700", "2299,95", "16", "","","","","2000.00","")]
        [InlineData("DELL I5 para ofimatica", "DELL", "DELL-2222", "4Gb", "Intel I5 14500", "499,95", "28", "","","","","","20")]
        public void CU1_3_FA1_Filtrado(string nombre, string marca, string modelo, string ram, string procesador, string precioCompra, string stock,
            string filtroNombre, string filtroMarca, string filtroRam, string filtroProcesador, string filtroPrecioMin, string filtroStockMin)
        {
            // Arrange -----------
            var seleccionarArticulos_PO = new SeleccionarPortatilesCompra_PO(_driver, _output);
            var expectedArticulos = new List<string[]> { new string[] { marca, nombre, modelo, ram, procesador, precioCompra, stock} };

            // Act ---------
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Comprar Artículos.
            Ir_A_ComprarArticulos();
            // Filtrar los artículos.
            seleccionarArticulos_PO.FiltrarPortatiles(filtroNombre, filtroMarca, filtroRam, filtroProcesador, filtroPrecioMin, filtroStockMin);

            // Assert ----------
            // Comprobar que la lista de artículos que ha devuelto es la correcta.
            Assert.True(seleccionarArticulos_PO.CompruebaListaPortatiles(expectedArticulos));
        }

        [Fact]
        public void CU1_9_Carrito_Vacio()
        {
            // Arrange
            var seleccionarArticulos_PO = new SeleccionarPortatilesCompra_PO(_driver, _output);

            // Act
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Comprar Artículos.
            Ir_A_ComprarArticulos();

            UtilitiesUIT.WaitForBeingVisible(_driver, By.Id("Submit"));
            // Assert
            Assert.False(seleccionarArticulos_PO.isEnabledComprar());
        }

        [Fact]
        public void CU1_10_Volver_Atras()
        {
            // Arrange
            var seleccionarArticulos_PO = new SeleccionarPortatilesCompra_PO(_driver, _output);
            var crearCompra_PO = new CrearCompra_PO(_driver, _output);
            // Act
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Comprar Artículos.
            Ir_A_ComprarArticulos();
            UtilitiesUIT.WaitForBeingVisible(_driver, By.Id("Submit"));
            // Seleccionar los artículos 3 y 5.
            seleccionarArticulos_PO.SeleccionarPortatiles(new List<string>() { "44" });
            // Pulsar el botón de comprar.
            seleccionarArticulos_PO.Comprar();
            UtilitiesUIT.WaitForBeingVisible(_driver, By.Id("Volver"));
            // Ahora volver.
            crearCompra_PO.Volver();

            // Assert
            Assert.True(seleccionarArticulos_PO.ComprobarSeleccionPortatiles(new List<string>() { "44" }));
        }

        [Fact]
        public void CU1_11_Cantidad_0()
        {
            // Arrange
            var seleccionarArticulos_PO = new SeleccionarPortatilesCompra_PO(_driver, _output);
            var crearCompra_PO = new CrearCompra_PO(_driver, _output);
            // Act
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Comprar Artículos.
            Ir_A_ComprarArticulos();
            UtilitiesUIT.WaitForBeingVisible(_driver, By.Id("Submit"));
            // Seleccionar los artículos 3 y 5.
            seleccionarArticulos_PO.SeleccionarPortatiles(new List<string>() { "44" });
            // Pulsar el botón de comprar.
            seleccionarArticulos_PO.Comprar();
            UtilitiesUIT.WaitForBeingVisible(_driver, By.Id("RealizarCompra"));
            // Seleccionar como método de pago PayPal.
            crearCompra_PO.setMetodoPago("PayPal");
            // Poner como cantidad 0 para el elemento 5.
            crearCompra_PO.setCantidad("44", "0");

            // Assert
            Assert.False(crearCompra_PO.isEnabledComprar());
        }

        [Fact]
        public void CU1_12_No_Hay_Stock()
        {
            // Arrange
            var seleccionarArticulos_PO = new SeleccionarPortatilesCompra_PO(_driver, _output);
            var crearCompra_PO = new CrearCompra_PO(_driver, _output);
            var detalleCompra_PO = new DetalleCompra_PO(_driver, _output);

            string expectedErrorTitleText = "Ha habido un problema al procesar tu compra.";
            string expectedErrorText = "(*) Error! El portátil con nombre HP 486 del pleistoceno solo tiene 12 unidades disponibles, pero has seleccionado 1000 unidades para comprar.";


            // Act
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Comprar Artículos.
            Ir_A_ComprarArticulos();
            // Seleccionar los artículos 3 y 5.
            seleccionarArticulos_PO.SeleccionarPortatiles(new List<string>() { "1" });
            // Pulsar el botón de comprar.
            seleccionarArticulos_PO.Comprar();
            UtilitiesUIT.WaitForBeingVisible(_driver, By.Id("RealizarCompra"));
            crearCompra_PO.setNombre("Jose");
            crearCompra_PO.setApellidos("García Molina");
            crearCompra_PO.setDireccion("C/San Francisco");
            // Seleccionar como método de pago PayPal.
            crearCompra_PO.setMetodoPago("PayPal");
            // Poner las cantidades.
            crearCompra_PO.setCantidad("1", "1000");
            // Pulsar comprar para finalizar la compra.
            crearCompra_PO.Comprar();
            // Ahora se mostrará el error al no haber stock.
            
            // Assert
            Assert.True(_driver.PageSource.Contains(expectedErrorText));
            Assert.True(_driver.PageSource.Contains(expectedErrorTitleText));
        }

        [Fact]
        public void CU1_13_Cantidad_No_Valida()
        {
            // Arrange
            var seleccionarArticulos_PO = new SeleccionarPortatilesCompra_PO(_driver, _output);
            var crearCompra_PO = new CrearCompra_PO(_driver, _output);
            var detalleCompra_PO = new DetalleCompra_PO(_driver, _output);

            string expectedErrorTitleText = "Ha habido un problema al procesar tu compra.";
            string expectedErrorText = "(*) Debes de indicar una cantidad válida.";


            // Act
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Comprar Artículos.
            Ir_A_ComprarArticulos();
            // Seleccionar los artículos 3 y 5.
            seleccionarArticulos_PO.SeleccionarPortatiles(new List<string>() { "1" });
            // Pulsar el botón de comprar.
            seleccionarArticulos_PO.Comprar();
            UtilitiesUIT.WaitForBeingVisible(_driver, By.Id("RealizarCompra"));
            crearCompra_PO.setNombre("Jose");
            crearCompra_PO.setApellidos("García Molina");
            crearCompra_PO.setDireccion("C/San Francisco");
            // Seleccionar como método de pago PayPal.
            crearCompra_PO.setMetodoPago("PayPal");
            // Poner las cantidades.
            crearCompra_PO.setCantidad("1", "-8");
            // Pulsar comprar para finalizar la compra.
            crearCompra_PO.Comprar();
            // Ahora se mostrará el error al no haber stock.

            // Assert
            Assert.True(_driver.PageSource.Contains(expectedErrorText));
            Assert.True(_driver.PageSource.Contains(expectedErrorTitleText));
        }

        [Fact]
        public void CU1_14_Datos_Mal_Introducidos()
        {
            // Arrange
            var seleccionarArticulos_PO = new SeleccionarPortatilesCompra_PO(_driver, _output);
            var crearCompra_PO = new CrearCompra_PO(_driver, _output);
            var detalleCompra_PO = new DetalleCompra_PO(_driver, _output);

            string expectedErrorTitleText = "Ha habido un problema al procesar tu compra.";
            string expectedErrorText = "(*) Por favor, introduzca su dirección de envío. (*) La dirección de envío tiene que tener al menos 10 caracteres.(*) Por favor, introduzca su nombre.(*) Por favor, introduzca sus apellidos.";


            // Act
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Comprar Artículos.
            Ir_A_ComprarArticulos();
            // Seleccionar los artículos 3 y 5.
            seleccionarArticulos_PO.SeleccionarPortatiles(new List<string>() { "1" });
            // Pulsar el botón de comprar.
            seleccionarArticulos_PO.Comprar();
            UtilitiesUIT.WaitForBeingVisible(_driver, By.Id("RealizarCompra"));
            // Seleccionar como método de pago PayPal.
            crearCompra_PO.setMetodoPago("PayPal");
            // Poner las cantidades.
            crearCompra_PO.setCantidad("1", "1");
            // Pulsar comprar para finalizar la compra.
            crearCompra_PO.Comprar();
            // Ahora se mostrará el error al no haber stock.

            // Assert
            Assert.True(_driver.PageSource.Contains(expectedErrorText));
            Assert.True(_driver.PageSource.Contains(expectedErrorTitleText));
        }

        private void Ir_A_ComprarArticulos()
        {
            // Esperar a que se cargue la página.
            UtilitiesUIT.WaitForBeingVisible(_driver, By.Id("ComprarPortatiles"));
            // Pulsamos en la opción del menú de navegación de Comprar Artículos.
            _driver.FindElement(By.Id("ComprarPortatiles")).Click();
        }

        void IDisposable.Dispose()
        {
            // Se deben liberar los recursos que se hayan usado en la prueba.
            _driver.Close();
            _driver.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
