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
            string precioAlquiler = "6.66";
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