using OneHope.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHope.UIT.Shared
{
    public class CrearDevolucion_PO : PageObject
    {

        // Se definen los localizadores de cada uno de los elementos de la página con el que se a interactuar usando su id.
        private By _direccionRecogidaBy = By.Id("direccionRecogida");
        private By _motivoDevolucionBy = By.Id("motivoDevolucion");
        private By _notaRepartidorBy = By.Id("notaRepartidor");
        private By _botonDevolverBy = By.Id("RealizarDevolucion");
        private By _botonVolverBy = By.Id("Volver");
        private By _tablaArticulosBy = By.Id("TablaPortatiles");

        // Este código es equivalente a: // private IWebElement _nombreArticulo() { return _driver.FindElement(By.Id("_nombreArticuloBy")); }
        private IWebElement _direccionRecogida() => _driver.FindElement(_direccionRecogidaBy);
        private IWebElement _motivoDevolucion() => _driver.FindElement(_motivoDevolucionBy);
        private IWebElement _notaRepartidor() => _driver.FindElement(_notaRepartidorBy);
        private IWebElement _botonDevolver() => _driver.FindElement(_botonDevolverBy);
        private IWebElement _botonVolver() => _driver.FindElement(_botonVolverBy);
        //PREGUNTAR SI HACE FALTA LA TABLA DE PORTATILES

        public CrearDevolucion_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        // Pulsar el botón volver.
        public void Volver()
        {
            _botonVolver().Click();
            System.Threading.Thread.Sleep(200);
        }

        // Pulsar el botón comprar.
        public void Devolver()
        {
            _botonDevolver().Click();
            System.Threading.Thread.Sleep(200);
        }

        public void setDireccionRecogida(string direccionRecogida)
        {
            _direccionRecogida().SendKeys(direccionRecogida);
            _direccionRecogida().SendKeys(Keys.Enter);
        }

        public void setMotivoDevolucion(string motivoDevolucion)
        {
            _motivoDevolucion().SendKeys(motivoDevolucion);
            _motivoDevolucion().SendKeys(Keys.Enter);
        }

        public void setNotaRepartidor(string notaRepartidor)
        {
            _notaRepartidor().SendKeys(notaRepartidor);
            _notaRepartidor().SendKeys(Keys.Enter);
        }

        // Fija la cantidad que se desea comprar para el artículo cuyo id es Id.
        // El Id del input de la cantidad está generado como: id="cantidad_@articulo.ArticuloId"
        public void setCantidad(string Id, string cantidad)
        {
            WaitForBeingVisible(By.Id($"cantidad_{Id}"));
            _driver.FindElement(By.Id($"cantidad_{Id}")).SendKeys(Keys.Backspace); // Borrar el 1
            _driver.FindElement(By.Id($"cantidad_{Id}")).SendKeys(cantidad); // Escribir la cantidad.
            _driver.FindElement(By.Id($"cantidad_{Id}")).SendKeys(Keys.Enter); // Darle a intro para forzar que salte la actualización del valor.
        }

        // Devuelve si el botón Comprar está activo o no.
        public bool isEnabledDevolver()
        {
            IWebElement botonDevolver = _botonDevolver();

            return botonDevolver.Enabled;
        }
    }
}

