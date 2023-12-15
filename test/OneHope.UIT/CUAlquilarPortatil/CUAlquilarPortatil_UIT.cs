using OneHope.UIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHope.UIT.CUAlquilarPortatil
{
    public class CUAlquilarPortatil_UIT : IDisposable
    {

        IWebDriver _driver;
        //A reference to the URI of the web page to test
        string _URI;
        //this may be used whenever some result should be printed in E
        private readonly ITestOutputHelper _output;

        public CUAlquilarPortatil_UIT(ITestOutputHelper output)
        {
            UtilitiesUIT.SetUp_UIT(out _driver, out _URI);
            _output = output;
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

        // Está es la prueba de ejemplo del filtrado por modelo de portatil sin usar Theory.
        [Fact]
        public void Ejemplo_AP_3_FA1_FiltradoPorNombreArticulo()
        {
            // Arrange -----------
            // Se definen los valores de los filtros.
            string filtroModelo = "1151";
            string filtroMarca = "";
            string filtroProcesador = "";
            string filtroRam = "";
            // En este caso (con los datos que hay cargados) sólo debe devolver la fila con los siguientes datos.
            string modelo = "HP-1151";
            string marca = "HP";
            string procesador = "Intel 80486";
            string ram = "8Gb";
            string precioAlquiler = "6,66";
            string stockAlquiler = "5";

            var seleccionarPortatiles_PO = new SeleccionarPortatilAlquilar_PO(_driver, _output);
            var expectedArticulos = new List<string[]> { new string[] { modelo, marca, procesador, ram, precioAlquiler, stockAlquiler } };

            // Act ---------
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Comprar Artículos.
            Ir_A_AlquilarPortatiles();
            // Filtrar los artículos.
            seleccionarPortatiles_PO.FiltrarPortatiles(filtroModelo, filtroMarca, filtroProcesador, filtroRam);

            // Assert ----------
            // Comprobar que la lista de artículos que ha devuelto es la correcta.
            Assert.True(seleccionarPortatiles_PO.CompruebaListaPortatiles(expectedArticulos));
        }

        [Fact]
        public void AP_1_Flujo_Basico()
        {
            // Arrange
            var seleccionarPortatiles_PO = new SeleccionarPortatilAlquilar_PO(_driver, _output);
            var crearAlquiler_PO = new CrearAlquiler_PO(_driver, _output);
            var detalleAlquiler_PO = new DetalleAlquiler_PO(_driver, _output);

            string expectedCabecera = "Resumen del Alquiler";
            string expectedNombreCliente = "<b>Nombre Cliente: </b> Antonio";
            string expectedApellidosCliente = "<b>Apellidos Cliente: </b> Rosendo";
            string expectedDireccion = "<b>Direccion: </b> Calle avenida";
            string expectedMetodoPago = "<b>Método de Pago: </b> Tarjeta";
            string expectedFecha = "<b>Fecha de alquiler: </b> " + DateTime.Now.ToString("dd-MMM-yyyy HH").ToUpper();
            string expectedFechaIn = "<b>Fecha Inicio de alquiler: </b> " + new DateTime(year: 2023, 12, 21).ToString("dd-MMM-yyyy").ToUpper();
            string expectedFechaFin = "<b>Fecha Final de alquiler: </b> " + new DateTime(year: 2023, 12, 23).ToString("dd-MMM-yyyy").ToUpper();

            var expectedPortatiles = new List<string[]>();
            expectedPortatiles.Add(new string[] { "HP-1151", "HP", "6,66", "1" });

            var expectedTotal = new List<string[]>();
            expectedTotal.Add(new string[] { "Precio Alquiler Total", "13,32" });


            // Act
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Alquilar Portatiles.
            Ir_A_AlquilarPortatiles();
            // Seleccionar el portatil 1
            seleccionarPortatiles_PO.SeleccionarPortatiles(new List<string>() { "1" });
            // Pulsar el botón de alquilar.
            seleccionarPortatiles_PO.Alquilar();
            // Seleccionar como método de pago TarjetaCredito.
            crearAlquiler_PO.setMetodoPago("TarjetaCredito");
            //Rellenar los datos del cliente.
            crearAlquiler_PO.setNombre("Antonio");
            crearAlquiler_PO.setApellidos("Rosendo");
            crearAlquiler_PO.setDireccion("Calle avenida");
            crearAlquiler_PO.setEmailCliente("antonio@email.com");
            crearAlquiler_PO.setFechaInAlquiler(new DateTime(year: 2023, 12, 21));
            crearAlquiler_PO.setFechaFinAlquiler(new DateTime(year: 2023, 12, 23));
            // Poner las cantidades.
            crearAlquiler_PO.setCantidad("1", "1");
            // Pulsar alquilar para finalizar el alquiler.
            crearAlquiler_PO.Alquilar();
            // Ahora se debe haber mostrado la página de detalle y puedo comprobar si todo ha ido bien.

            //Esperar a que cargue el detalle
            UtilitiesUIT.WaitForBeingVisible(_driver, By.Id("Submit"));
            // Assert
            Assert.True(_driver.PageSource.Contains(expectedCabecera));
            Assert.True(_driver.PageSource.Contains(expectedNombreCliente));
            Assert.True(_driver.PageSource.Contains(expectedApellidosCliente));
            Assert.True(_driver.PageSource.Contains(expectedDireccion));
            Assert.True(_driver.PageSource.Contains(expectedMetodoPago));
            Assert.True(_driver.PageSource.Contains(expectedFecha));
            Assert.True(_driver.PageSource.Contains(expectedFechaIn));
            Assert.True(_driver.PageSource.Contains(expectedFechaFin));
            Assert.True(detalleAlquiler_PO.CompruebaListaPortatiles(expectedPortatiles));
            Assert.True(detalleAlquiler_PO.CompruebaTotal(expectedTotal));

        }

        [Fact]
        public void AP_2_FA0_No_Hay_Portatiles()
        {
            // Arrange -----------
            var expectedText = "No hay portátiles que cumplan los criterios seleccionados.";
            var seleccionarPortatiles_PO = new SeleccionarPortatilAlquilar_PO(_driver, _output);

            // Act ---------
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Alquilar Portatiles.
            Ir_A_AlquilarPortatiles();
            // Filtrar los artículos.
            seleccionarPortatiles_PO.FiltrarPortatiles("No hay", "", "", "");

            // Assert ----------
            // Comprobar que la lista de artículos que ha devuelto es la correcta.
            Assert.Contains(expectedText, _driver.PageSource);
        }

        [Theory]
        //--------- (Modelo, Marca, Procesador, Ram, Precio Alquiler, Stock Alquiler, filtroModelo, filtroMarca, filtroProcesador, filtroRam)
        [InlineData("HP-1151", "HP", "Intel 80486", "8Gb", "6,66", "5", "1151", "", "", "")]
        [InlineData("TOASTER-4461", "TOASTER", "Pentium 4", "32Gb", "56,66", "2", "", "TOASTER", "", "")]
        [InlineData("HP-1151", "HP", "Intel 80486", "8Gb", "6,66", "5", "", "", "Intel 80486", "")]
        [InlineData("TOASTER-4461", "TOASTER", "Pentium 4", "32Gb", "56,66", "2", "", "", "", "32Gb")]
        public void AP_3_FA1_Filtrado(string modelo, string marca, string procesador, string ram, string precioAlquiler, string stockAlquiler, string filtroModelo, 
            string filtroMarca, string filtroProcesador, string filtroRam)
        {
            // Arrange -----------
            var seleccionarPortatiles_PO = new SeleccionarPortatilAlquilar_PO(_driver, _output);
            var expectedPortatiles = new List<string[]> { new string[] { modelo, marca, procesador, ram, precioAlquiler, stockAlquiler } };

            // Act ---------
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Comprar Artículos.
            Ir_A_AlquilarPortatiles();
            // Filtrar los artículos.
            seleccionarPortatiles_PO.FiltrarPortatiles(filtroModelo, filtroMarca, filtroProcesador, filtroRam);

            // Assert ----------
            // Comprobar que la lista de artículos que ha devuelto es la correcta.
            Assert.True(seleccionarPortatiles_PO.CompruebaListaPortatiles(expectedPortatiles));
        }

        [Fact]
        public void AP_7_Carrito_Vacio()
        {
            // Arrange
            var seleccionarPortatiles_PO = new SeleccionarPortatilAlquilar_PO(_driver, _output);

            // Act
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Comprar Artículos.
            Ir_A_AlquilarPortatiles();

            //Espere a que cargue la pagina
            UtilitiesUIT.WaitForBeingVisible(_driver, By.Id("Submit"));
            // Assert
            Assert.False(seleccionarPortatiles_PO.isEnabledAlquilar());
        }

        private void Ir_A_AlquilarPortatiles()
        {
            // Esperar a que se cargue la página.
            UtilitiesUIT.WaitForBeingVisible(_driver, By.Id("AlquilarPortatilesNavLink"));
            // Pulsamos en la opción del menú de navegación de Comprar Artículos.
            _driver.FindElement(By.Id("AlquilarPortatilesNavLink")).Click();
        }

        void IDisposable.Dispose()
        {
            //To close and release all the resources allocated by the web driver
            _driver.Close();
            _driver.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}