using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHope.UIT.Shared {
    public class CrearAlquiler_PO : PageObject {
        // Se definen los localizadores de cada uno de los elementos de la página con el que se a interactuar usando su id.
        private By _nombreClienteBy = By.Id("nombreCliente");
        private By _apellidosClienteBy = By.Id("apellidosCliente");
        private By _direccionAlquilerBy = By.Id("direccionCliente");
        private By _emailClienteBy = By.Id("emailCliente");
        private By _telefonoClienteBy = By.Id("telefonoCliente");
        private By _metodoPagoSelBy = By.Id("metodoPagoSel");
        private By _fechaInAlquilerBy = By.Id("fechaInAlquiler");
        private By _fechaFinAlquilerBy = By.Id("fechaFinAlquiler");
        private By _botonAlquilarBy = By.Id("Alquilar");
        private By _botonVolverBy = By.Id("Volver");
        private By _tablaArticulosBy = By.Id("TablaArticulos");

        // Este código es equivalente a: // private IWebElement _nombreArticulo() { return _driver.FindElement(By.Id("_nombreArticuloBy")); }
        private IWebElement _nombreCliente() => _driver.FindElement(_nombreClienteBy);
        private IWebElement _apellidosCliente() => _driver.FindElement(_apellidosClienteBy);
        private IWebElement _direccionAlquiler() => _driver.FindElement(_direccionAlquilerBy);
        private IWebElement _emailCliente() => _driver.FindElement(_emailClienteBy);
        private IWebElement _telefonoCliente() => _driver.FindElement(_telefonoClienteBy);
        private IWebElement _fechaInAlquiler() => _driver.FindElement(_fechaInAlquilerBy);
        private IWebElement _fechaFinAlquiler() => _driver.FindElement(_fechaFinAlquilerBy);
        private IWebElement _botonAlquilar() => _driver.FindElement(_botonAlquilarBy);
        private IWebElement _botonVolver() => _driver.FindElement(_botonVolverBy);
        private IWebElement _metodoPago() => _driver.FindElement(_metodoPagoSelBy);

        public CrearAlquiler_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output) {
        }

        // Pulsar el botón volver.
        public void Volver() {
            _botonVolver().Click();
            System.Threading.Thread.Sleep(200);
        }

        // Pulsar el botón alquilar.
        public void Alquilar() {
            _botonAlquilar().Click();
            System.Threading.Thread.Sleep(200);
        }

        //Fija el nombre del cliente
        public void setNombre(string nombreCliente)
        {
            _nombreCliente().SendKeys(nombreCliente);
        }
        //Fija los apellidos del cliente
        public void setApellidos(string apellidosCliente)
        {
            _apellidosCliente().SendKeys(apellidosCliente);
        }
        //Fija la direccion del cliente
        public void setDireccion(string direccion)
        {
            _direccionAlquiler().SendKeys(direccion);
        }
        //Fija el email del cliente
        public void setEmailCliente(string emailCliente)
        {
            _emailCliente().SendKeys(emailCliente);
        }
        //Fija el telefono del cliente
        public void setTelefono(string telefono)
        {
            _telefonoCliente().SendKeys(telefono);
        }
        // Fija la cantidad que se desea alquilar para el artículo cuyo id es Id.
        // El Id del input de la cantidad está generado como: id="cantidad_@portatil.PortatilID"
        public void setCantidad(string Id, string cantidad) {
            WaitForBeingVisible(By.Id($"cantidad_{Id}"));
            _driver.FindElement(By.Id($"cantidad_{Id}")).SendKeys(Keys.Backspace); // Borrar el 1
            _driver.FindElement(By.Id($"cantidad_{Id}")).SendKeys(cantidad); // Escribir la cantidad.
            _driver.FindElement(By.Id($"cantidad_{Id}")).SendKeys(Keys.Enter); // Darle a intro para forzar que salte la actualización del valor.
        }

        // Hay un problema, no salta el evento onChange por lo que no se actualiza correctamente el contenedor del estado.
        public void setMetodoPago(string metodoPago) {
            WaitForBeingClickable(_metodoPagoSelBy);
            // Se crea la lista desplegable.
            SelectElement selectElement = new SelectElement(_metodoPago());
            // Selecciona la opción que se ha indicado en el parámetro.
            selectElement.SelectByText(metodoPago);
        }
        //Establece la fecha de inicio de alquiler
        public void setFechaInAlquiler(DateTime fechaInAlquiler)
        {
            /*
            WaitForBeingClickable(_fechaInAlquilerBy);
            // Se crea la lista desplegable.
            SelectElement selectElement = new SelectElement(_fechaInAlquiler());
            // Selecciona la opción que se ha indicado en el parámetro.
            selectElement.SelectByText(fechaInAlquiler);
            */
            InputDateInDatePicker(_fechaInAlquilerBy, fechaInAlquiler);
        }
        //Establece la fecha del final del alquiler
        public void setFechaFinAlquiler(DateTime fechaFinAlquiler)
        {
            /*
            WaitForBeingClickable(_fechaFinAlquilerBy);
            // Se crea la lista desplegable.
            SelectElement selectElement = new SelectElement(_fechaFinAlquiler());
            // Selecciona la opción que se ha indicado en el parámetro.
            selectElement.SelectByText(fechaFinAlquiler);
            */
            InputDateInDatePicker(_fechaFinAlquilerBy, fechaFinAlquiler);
        }

        // Devuelve si el botón Comprar está activo o no.
        public bool isEnabledComprar() {
            IWebElement botonComprar = _botonAlquilar();

            return botonComprar.Enabled;
        }
    }
}
