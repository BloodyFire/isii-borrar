using OneHope.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHope.UIT.Shared
{
    public class SeleccionPortatiles_PO : PageObject
    {
        // Se definen los localizadores de cada uno de los elementos de la página con el que se a interactuar usando su id.
        private By _idCompraBy = By.Id("portatilIdCompra");
        private By _fechaBy = By.Id("fecha");
        private By _precioBy = By.Id("precio");
        private By _botonBuscarBy = By.Id("buscarPortatiles");
        private By _botonDevolverBy = By.Id("Submit");
        private By _tablaPortatilesBy = By.Id("TableOfPortatiles");


        // Este código es equivalente a: // private IWebElement _nombreArticulo() { return _driver.FindElement(By.Id("_nombreArticuloBy")); }
        private IWebElement _idCompra() => _driver.FindElement(_idCompraBy);
        private IWebElement _fecha() => _driver.FindElement(_fechaBy);
        private IWebElement _precio() => _driver.FindElement(_precioBy);
        private IWebElement _botonBuscar() => _driver.FindElement(_botonBuscarBy);
        private IWebElement _botonDevolver() => _driver.FindElement(_botonDevolverBy);
        private IWebElement _tablaPortatiles() => _driver.FindElement(_tablaPortatilesBy);

        public SeleccionPortatiles_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        // Simular el paso de filtrar los portátiles. El método tendrá un parámetro por cada uno de los filtros que tengas.
        public void FiltrarPortatiles(string? idCompra, string? fecha, string? precio)
        {
            // Para poder interaccionar con el elemento debe ser visible.
            // El método se le pasa el Id, no la referencia.
            //WaitForBeingVisible(_idCompraBy);

            // Se simula que se escribe en la cuadro de texto el filtro del id de la compra del portátil.
            //_driver.FindElement(_idCompraBy).SendKeys(idCompra);

            //AÑADIR FILTROs
            if (idCompra != "")
            {
                WaitForBeingVisible(_idCompraBy);
                _idCompra().SendKeys($"{Keys.Backspace}{Keys.Backspace}");
                _idCompra().SendKeys(idCompra.ToString());
            }

            if (fecha != "")
            {
                WaitForBeingVisible(_fechaBy);
                var fecha_ = DateTime.Parse(fecha);
                InputDateInDatePicker(_fechaBy, fecha_);
                //_fecha().SendKeys($"{Keys.Backspace}{Keys.Backspace}");
               // _fecha().SendKeys(fecha.ToString());
            }

            WaitForBeingVisible(_precioBy);
            // Se simula que se escribe en la cuadro de texto el filtro del precio del portátil.
            _driver.FindElement(_precioBy).SendKeys(precio.ToString());

            _botonBuscar().Click();
            // Se espera 2000 milisegundos para esperar a que la tabla se recargue.
            System.Threading.Thread.Sleep(2000);
        }

        // Este método permite comprobar si la lista de artículos mostrada en la tabla coincide con la esperada o no.
        public bool CompruebaListaPortatiles(List<string[]> expectedPortatiles)
        {

            return CheckBodyTable(expectedPortatiles, _tablaPortatilesBy);
        }

        // Devuelve si el botón Devolver está activo o no.
        public bool isEnabledDevolver()
        {
            IWebElement botonDevolver = _botonDevolver();

            return botonDevolver.Enabled;
        }

        // Selecciona los artículos cuyos Ids aparecen en la lista.
        // Recuerda que en la tabla los Ids del input para seleccionar se han generado como: portatilDevolver_@portatil.IdCompra
        public void SeleccionarPortatiles(List<string> comprasIds)
        {
            //we wait for till the movies are available to be selected 
            foreach (var compraId in comprasIds)
            {
                WaitForBeingVisible(By.Id($"portatilDevolver_{compraId}"));
                _driver.FindElement(By.Id($"portatilDevolver_{compraId}")).Click();
            }
        }

        // Comprueba si los artículos cuyos Ids parecen en la lista están todos seleccionados.
        // En caso contrario devuelve false.
        public bool ComprobarSeleccionPortatiles(List<string> portatilesIds)
        {

            //we wait for till the movies are available to be selected 
            foreach (var portatilId in portatilesIds)
            {
                WaitForBeingVisible(By.Id($"portatilDevolver_{portatilId}"));
                string value = _driver.FindElement(By.Id($"portatilDevolver_{portatilId}")).GetAttribute("checked");
                if (value.Equals("false")) return false;
            }

            return true;
        }

        // Pulsar el botón devolver.
        public void Devolver()
        {
            _botonDevolver().Click();
            System.Threading.Thread.Sleep(200); // Si no añado este retardo no funcionan las pruebas.
        }

    }
}
