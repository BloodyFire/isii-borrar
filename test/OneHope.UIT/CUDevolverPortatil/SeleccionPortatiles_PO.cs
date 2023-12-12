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
        private By _nombreArticuloBy = By.Id("articuloNombre");
        private By _colorArticuloBy = By.Id("selectColor");
        private By _botonBuscarBy = By.Id("botonBuscarArticulos");
        private By _botonComprarBy = By.Id("botonComprar");
        private By _tablaArticulosBy = By.Id("TablaArticulos");


        // Este código es equivalente a: // private IWebElement _nombreArticulo() { return _driver.FindElement(By.Id("_nombreArticuloBy")); }
        private IWebElement _nombreArticulo() => _driver.FindElement(_nombreArticuloBy);
        private IWebElement _colorArticulo() => _driver.FindElement(_colorArticuloBy);
        private IWebElement _botonBuscar() => _driver.FindElement(_botonBuscarBy);
        private IWebElement _botonComprar() => _driver.FindElement(_botonComprarBy);

        public SeleccionarArticulos_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        // Simular el paso de filtrar los artículos. El método tendrá un parámetro por cada uno de los filtros que tengas.
        public void FiltrarArticulos(string nombreArticulo, string colorArticulo)
        {
            // Para poder interaccionar con el elemento debe ser visible.
            // El método se le pasa el Id, no la referencia.
            WaitForBeingVisible(_nombreArticuloBy);

            // Se simula que se escribe en la cuadro de texto el filtro del nombre del artículo.
            _driver.FindElement(_nombreArticuloBy).SendKeys(nombreArticulo);

            // Si no se ha seleccionado ningún color, entonces debe seleccionarse "Todos".
            if (colorArticulo == "") colorArticulo = "Todos";

            WaitForBeingVisible(_colorArticuloBy);
            // Se crea la lista desplegable.
            SelectElement selectElement = new SelectElement(_colorArticulo());
            // Selecciona la opción que se ha indicado en el parámetro para el filtro.
            selectElement.SelectByText(colorArticulo);

            _botonBuscar().Click();
            // Se espera 2000 milisegundos para esperar a que la tabla se recargue.
            System.Threading.Thread.Sleep(2000);
        }

        // Este método permite comprobar si la lista de artículos mostrada en la tabla coincide con la esperada o no.
        public bool CompruebaListaArticulos(List<string[]> expectedArticulos)
        {

            return CheckBodyTable(expectedArticulos, _tablaArticulosBy);
        }

        // Devuelve si el botón Comprar está activo o no.
        public bool isEnabledComprar()
        {
            IWebElement botonComprar = _botonComprar();

            return botonComprar.Enabled;
        }

        // Selecciona los artículos cuyos Ids aparecen en la lista.
        // Recuerda que en la tabla los Ids del input para seleccionar se han generado como: articuloCompra_@articulo.Id
        public void SeleccionarArticulos(List<string> articulosIds)
        {
            //we wait for till the movies are available to be selected 
            foreach (var articuloId in articulosIds)
            {
                WaitForBeingVisible(By.Id($"articuloCompra_{articuloId}"));
                _driver.FindElement(By.Id($"articuloCompra_{articuloId}")).Click();
            }
        }

        // Comprueba si los artículos cuyos Ids parecen en la lista están todos seleccionados.
        // En caso contrario devuelve false.
        public bool ComprobarSeleccionArticulos(List<string> articulosIds)
        {

            //we wait for till the movies are available to be selected 
            foreach (var articuloId in articulosIds)
            {
                WaitForBeingVisible(By.Id($"articuloCompra_{articuloId}"));
                string value = _driver.FindElement(By.Id($"articuloCompra_{articuloId}")).GetAttribute("checked");
                if (value.Equals("false")) return false;
            }

            return true;
        }

        // Pulsar el botón comprar.
        public void Comprar()
        {
            _botonComprar().Click();
            System.Threading.Thread.Sleep(200); // Si no añado este retardo no funcionan las pruebas.
        }

    }
}
