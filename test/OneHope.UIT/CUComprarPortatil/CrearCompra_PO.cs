using OneHope.UIT.Shared;
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
    public class CrearCompra_PO : PageObject
    {
        // Se definen los localizadores de cada uno de los elementos de la página con el que se a interactuar usando su id.
        private By _nombreClienteBy = By.Id("nombre");
        private By _apellidosClienteBy = By.Id("apellidos");
        private By _direccionCompraBy = By.Id("direccion");
        private By _botonVolverBy = By.Id("Volver");
        private By _seleccionarMetodoPagoBy = By.Id("selMetodoPago");
        private By _botonComprarBy = By.Id("RealizarCompra");
        private By _tablaPortatilesBy = By.Id("TablaPortatiles");

        // Este código es equivalente a: // private IWebElement _nombrePortatil() { return _driver.FindElement(By.Id("_nombrePortatilBy")); }
        private IWebElement _nombreCliente() => _driver.FindElement(_nombreClienteBy);
        private IWebElement _apellidosCliente() => _driver.FindElement(_apellidosClienteBy);
        private IWebElement _direccionCompra() => _driver.FindElement(_direccionCompraBy);
        private IWebElement _botonVolver() => _driver.FindElement(_botonVolverBy);
        private IWebElement _botonComprar() => _driver.FindElement(_botonComprarBy);
        private IWebElement _metodoPago() => _driver.FindElement(_seleccionarMetodoPagoBy);

        public CrearCompra_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        // Pulsar el botón volver.
        public void Volver()
        {
            _botonVolver().Click();
            System.Threading.Thread.Sleep(200);
        }

        // Pulsar el botón realizar pedido.
        public void Comprar()
        {
            _botonComprar().Click();
            System.Threading.Thread.Sleep(2000);
        }

        public void setNombre(string nombre)
        {
            _nombreCliente().SendKeys(nombre);
        }

        public void setApellidos(string apellidos)
        {
            _apellidosCliente().SendKeys(apellidos);
        }

        public void setDireccion(string direccion)
        {
            _direccionCompra().SendKeys(direccion);
        }

        // Fija la cantidad que se desea pedir para el portátil cuyo id es Id.
        // El Id del input de la cantidad está generado como: id="cantidad_@portatil.PortatilId"
        public void setCantidad(string Id, string cantidad)
        {
            WaitForBeingVisible(By.Id($"cantidad_{Id}"));
            _driver.FindElement(By.Id($"cantidad_{Id}")).SendKeys(Keys.Backspace); // Borrar el 1
            _driver.FindElement(By.Id($"cantidad_{Id}")).SendKeys(cantidad); // Escribir la cantidad.
            _driver.FindElement(By.Id($"cantidad_{Id}")).SendKeys(Keys.Enter); // Darle a intro para forzar que salte la actualización del valor.
        }

        // Hay un problema, no salta el evento onChange por lo que no se actualiza correctamente el contenedor del estado.
        public void setMetodoPago(string metodoPago)
        {
            WaitForBeingClickable(_seleccionarMetodoPagoBy);
            // Se crea la lista desplegable.
            SelectElement selectElement = new SelectElement(_metodoPago());
            // Selecciona la opción que se ha indicado en el parámetro.
            selectElement.SelectByText(metodoPago);
            //TODO:Check if this fix the onChangeMethod
            //_metodoPago().SendKeys(Keys.Enter);
        }

        // Devuelve si el botón Comprar está activo o no.
        public bool isEnabledComprar()
        {
            IWebElement botonComprar = _botonComprar();

            return botonComprar.Enabled;
        }
    }
}
