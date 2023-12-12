using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace OneHope.UIT.CUComprarPortatil
{
    public class SeleccionarPortatilesCompra_PO : Shared.PageObject
    {

        private By _nombrePortatilBy = By.Id("portatilNombre");
        private By _marcaPortatilBy = By.Id("SeleccionarMarca");
        private By _ramPortatilBy = By.Id("SeleccionarRam");
        private By _procesadorPortatilBy = By.Id("SeleccionarProcesador");
        private By _precioMinPortatilBy = By.Id("portatilPrecioCompra");
        private By _stockMinPortatilBy = By.Id("portatilStock");
        private By _botonBuscarPortatilesBy = By.Id("buscarPortatiles");
        private By _botonComprarBy = By.Id("Submit");
        private By _tablaPortatilesBy = By.Id("TablaDePortatiles");

        private IWebElement _nombrePortatil() => _driver.FindElement(_nombrePortatilBy);
        private IWebElement _marcaPortatil() => _driver.FindElement(_marcaPortatilBy);
        private IWebElement _ramPortatil() => _driver.FindElement(_ramPortatilBy);
        private IWebElement _procesadorPortatil() => _driver.FindElement(_procesadorPortatilBy);
        private IWebElement _precioMinPortatil() => _driver.FindElement(_precioMinPortatilBy);
        private IWebElement _stockMinPortatil() => _driver.FindElement(_stockMinPortatilBy);
        private IWebElement _botonBuscar() => _driver.FindElement(_botonBuscarPortatilesBy);
        private IWebElement _botonComprar() => _driver.FindElement(_botonComprarBy);
        private IWebElement _tablaPortatiles() => _driver.FindElement(_tablaPortatilesBy);
        public SeleccionarPortatilesCompra_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        // Simular el paso de filtrar los portátiles. El método tendrá un parámetro por cada uno de los filtros que tengas.
        public void FiltrarPortatiles(string? portatilNombre, string? portatilMarca, string? portatilRam, string? portatilProcesador, int? portatilPrecioMin, int? portatilStockMin)
        {
            SelectElement selectElement;

            // Para poder interaccionar con el elemento debe ser visible.
            // El método se le pasa el Id, no la referencia.

            WaitForBeingVisible(_nombrePortatilBy);
            WaitForBeingVisible(_marcaPortatilBy);
            WaitForBeingVisible(_ramPortatilBy);
            WaitForBeingVisible(_procesadorPortatilBy);
            WaitForBeingVisible(_precioMinPortatilBy);
            WaitForBeingVisible(_stockMinPortatilBy);

            // Se simula que se escribe en la cuadro de texto el filtro del nombre del artículo.
            _driver.FindElement(_nombrePortatilBy).SendKeys(portatilNombre);
            _driver.FindElement(_marcaPortatilBy).SendKeys(portatilMarca);
            _driver.FindElement(_ramPortatilBy).SendKeys(portatilRam);
            _driver.FindElement(_procesadorPortatilBy).SendKeys(portatilProcesador);
            _driver.FindElement(_precioMinPortatilBy).SendKeys(portatilPrecioMin.ToString());
            _driver.FindElement(_stockMinPortatilBy).SendKeys(portatilStockMin.ToString());

            // Si no se ha seleccionado ningún color, entonces debe seleccionarse "Todos".
            if (portatilNombre == "") portatilNombre = "Todos";

            WaitForBeingVisible(_nombrePortatilBy);
            // Se crea la lista desplegable.
            selectElement = new SelectElement(_nombrePortatil());
            // Selecciona la opción que se ha indicado en el parámetro para el filtro.
            selectElement.SelectByText(portatilNombre);

            if (portatilMarca == "") portatilMarca = "Todos";
            WaitForBeingVisible(_marcaPortatilBy);
            selectElement = new SelectElement(_marcaPortatil());
            selectElement.SelectByText(portatilMarca);

            if (portatilRam == "") portatilRam = "Todos";
            WaitForBeingVisible(_ramPortatilBy);
            selectElement = new SelectElement(_ramPortatil());
            selectElement.SelectByText(portatilRam);

            if (portatilProcesador == "") portatilProcesador = "Todos";
            WaitForBeingVisible(_procesadorPortatilBy);
            selectElement = new SelectElement(_procesadorPortatil());
            selectElement.SelectByText(portatilProcesador);

            _botonBuscar().Click();
            // Se espera 2000 milisegundos para esperar a que la tabla se recargue.
            System.Threading.Thread.Sleep(2000);
        }

        // Este método permite comprobar si la lista de portátiles mostrada en la tabla coincide con la esperada o no.
        public bool CompruebaListaPortatiles(List<string[]> expectedPortatiles)
        {

            return CheckBodyTable(expectedPortatiles, _tablaPortatilesBy);
        }

        // Devuelve si el botón Comprar está activo o no.
        public bool isEnabledPedir()
        {
            IWebElement botonComprar = _botonComprar();

            return botonComprar.Enabled;
        }

        // Selecciona los portátiles cuyos Ids aparecen en la lista.
        // Recuerda que en la tabla los Ids del input para seleccionar se han generado como: portatilPedido_@portatil.Id
        public void SeleccionarPortatiles(List<string> portatilsIds)
        {
            //we wait for till the movies are available to be selected 
            foreach (var portatilId in portatilsIds)
            {
                WaitForBeingVisible(By.Id($"portatilPedido_{portatilId}"));
                _driver.FindElement(By.Id($"portatilPedido_{portatilId}")).Click();
            }
        }

        // Comprueba si los portátiles cuyos Ids parecen en la lista están todos seleccionados.
        // En caso contrario devuelve false.
        public bool ComprobarSeleccionPortatiles(List<string> portatilsIds)
        {

            //we wait for till the movies are available to be selected 
            foreach (var portatilId in portatilsIds)
            {
                WaitForBeingVisible(By.Id($"portatilPedido_{portatilId}"));
                string value = _driver.FindElement(By.Id($"portatilPedido_{portatilId}")).GetAttribute("checked");
                if (value.Equals("false")) return false;
            }

            return true;
        }

        // Pulsar el botón pedir.
        public void Comprar()
        {
            _botonComprar().Click();
            System.Threading.Thread.Sleep(200); // Si no añado este retardo no funcionan las pruebas.
        }
    }
}
