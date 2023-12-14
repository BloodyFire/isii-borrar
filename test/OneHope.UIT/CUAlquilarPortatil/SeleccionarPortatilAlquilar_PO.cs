using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHope.UIT.Shared {
    public class SeleccionarPortatilAlquilar_PO : PageObject {
        // Se definen los localizadores de cada uno de los elementos de la página con el que se a interactuar usando su id.
        private By _modeloPortatilBy = By.Id("portatilModelo");
        private By _marcaPortatilBy = By.Id("portatilMarcaSeleccionada");
        private By _procesadorPortatilBy = By.Id("portatilProcesadorSeleccionado");
        private By _ramPortatilBy = By.Id("portatilRamSeleccionada");
        private By _botonBuscarBy = By.Id("buscarPortatiles");
        private By _botonAlquilarBy = By.Id("Submit");
        private By _tablaArticulosBy = By.Id("TablaPortatiles");


        // Este código es equivalente a: // private IWebElement _nombreArticulo() { return _driver.FindElement(By.Id("_nombreArticuloBy")); }
        private IWebElement _modeloPortatil() => _driver.FindElement(_modeloPortatilBy);
        private IWebElement _marcaPortatil() => _driver.FindElement(_marcaPortatilBy);
        private IWebElement _procesadorPortatil() => _driver.FindElement(_procesadorPortatilBy);
        private IWebElement _ramPortatil() => _driver.FindElement(_ramPortatilBy);
        private IWebElement _botonBuscar() => _driver.FindElement(_botonBuscarBy);
        private IWebElement _botonAlquilar() => _driver.FindElement(_botonAlquilarBy);

        public SeleccionarPortatilAlquilar_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output) {
        }

        // Simular el paso de filtrar los portatiles. El método tendrá un parámetro por cada uno de los filtros que tengas.
        public void FiltrarArticulos(string? modeloPortatil, string? marcaPortatil, string? procesadorPortatil, string? ramPortatil) {
            // Para poder interaccionar con el elemento debe ser visible.
            // El método se le pasa el Id, no la referencia.
            WaitForBeingVisible(_modeloPortatilBy);

            // Se simula que se escribe en la cuadro de texto el filtro del modelo del portatil.
            _driver.FindElement(_modeloPortatilBy).SendKeys(modeloPortatil);

            // Si no se ha seleccionado ningúna marca, procesador o ram, entonces debe seleccionarse "Todos".
            if (marcaPortatil == "") marcaPortatil = "Todos";
            if (procesadorPortatil == "") procesadorPortatil = "Todos";
            if (ramPortatil == "") ramPortatil = "Todos";

            WaitForBeingVisible(_marcaPortatilBy);
            // Se crea la lista desplegable.
            SelectElement selectElement = new SelectElement(_marcaPortatil());
            // Selecciona la opción que se ha indicado en el parámetro para el filtro.
            selectElement.SelectByText(marcaPortatil);

            //Para procesador
            WaitForBeingVisible(_procesadorPortatilBy);
            selectElement = new SelectElement (_procesadorPortatil());
            selectElement.SelectByText(procesadorPortatil);

            //Para Ram
            WaitForBeingVisible(_ramPortatilBy);
            selectElement = new SelectElement(_ramPortatil());
            selectElement.SelectByText(ramPortatil);

            _botonBuscar().Click();
            // Se espera 2000 milisegundos para esperar a que la tabla se recargue.
            System.Threading.Thread.Sleep(2000);
        }

        // Este método permite comprobar si la lista de portatiles mostrada en la tabla coincide con la esperada o no.
        public bool CompruebaListaPortatiles(List<string[]> expectedPortatiles) {

            return CheckBodyTable(expectedPortatiles, _botonBuscarBy);
        }

        // Devuelve si el botón Alquilar está activo o no.
        public bool isEnabledComprar() {
            IWebElement botonAlquilar = _botonAlquilar();
            
            return botonAlquilar.Enabled;
        }

        // Selecciona los portatiles cuyos Ids aparecen en la lista.
        // Recuerda que en la tabla los Ids del input para seleccionar se han generado como: portatilAlquiler_@portatil.Id
        public void SeleccionarPortatiles(List<string> portatilesIds) {
            //we wait for till the laptops are available to be selected 
            foreach (var portatilId in portatilesIds) {
                WaitForBeingVisible(By.Id($"portatilAlquiler_{portatilId}"));
                _driver.FindElement(By.Id($"portatilAlquiler_{portatilId}")).Click();
            }
        }

        // Comprueba si los portatiles cuyos Ids parecen en la lista están todos seleccionados.
        // En caso contrario devuelve false.
        public bool ComprobarSeleccionPortatiles(List<string> portatilesIds) {

            //we wait for till the laptops are available to be selected 
            foreach (var portatilId in portatilesIds) {
                WaitForBeingVisible(By.Id($"portatilAlquiler_{portatilId}"));
                string value = _driver.FindElement(By.Id($"portatilAlquiler_{portatilId}")).GetAttribute("checked");
                if (value.Equals("false")) return false;
            }

            return true;
        }

        // Pulsar el botón Alquilar.
        public void Alquilar() {
            _botonAlquilar().Click();
            System.Threading.Thread.Sleep(200); // Si no añado este retardo no funcionan las pruebas.
        }

    }
}
