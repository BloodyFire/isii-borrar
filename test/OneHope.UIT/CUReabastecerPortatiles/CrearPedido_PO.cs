﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHope.UIT.Shared {
    public class CrearPedido_PO : PageObject
    {
        // Se definen los localizadores de cada uno de los elementos de la página con el que se a interactuar usando su id.
        private By _direccionPedidoBy = By.Id("direccionAlmacen");
        private By _comentariosPedidoBy = By.Id("comentarios");
        private By _botonPedirBy = By.Id("RealizarPedido");
        private By _botonVolverBy = By.Id("Volver");
        private By _seleccionarMetodoPagoBy = By.Id("selMetodoPago");
        private By _tablaPortatilesBy = By.Id("TablaPortatiles");

        // Este código es equivalente a: // private IWebElement _nombrePortatil() { return _driver.FindElement(By.Id("_nombrePortatilBy")); }
        private IWebElement _direccionPedido() => _driver.FindElement(_direccionPedidoBy);
        private IWebElement _comentariosPedido() => _driver.FindElement(_comentariosPedidoBy);
        private IWebElement _botonPedir() => _driver.FindElement(_botonPedirBy);
        private IWebElement _botonVolver() => _driver.FindElement(_botonVolverBy);
        private IWebElement _metodoPago() => _driver.FindElement(_seleccionarMetodoPagoBy);

        public CrearPedido_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output) {
        }

        // Pulsar el botón volver.
        public void Volver() {
            _botonVolver().Click();
            System.Threading.Thread.Sleep(200);
        }

        // Pulsar el botón realizar pedido.
        public void Pedir() {
            _botonPedir().Click();
            System.Threading.Thread.Sleep(200);
        }

        // Fija la cantidad que se desea pedir para el portátil cuyo id es Id.
        // El Id del input de la cantidad está generado como: id="cantidad_@portatil.PortatilId"
        public void setCantidad(string Id, string cantidad) {
            WaitForBeingVisible(By.Id($"cantidad_{Id}"));
            _driver.FindElement(By.Id($"cantidad_{Id}")).SendKeys(Keys.Backspace); // Borrar el 1
            _driver.FindElement(By.Id($"cantidad_{Id}")).SendKeys(cantidad); // Escribir la cantidad.
            _driver.FindElement(By.Id($"cantidad_{Id}")).SendKeys(Keys.Enter); // Darle a intro para forzar que salte la actualización del valor.
        }

        public void setDireccion(string direccion)
        {
            _direccionPedido().SendKeys(direccion);
            _direccionPedido().SendKeys(Keys.Enter);
        }

        public void serComentarios(string comentrarios)
        {
            _comentariosPedido().SendKeys(comentrarios);
            _comentariosPedido().SendKeys(Keys.Enter);
        }

        // Hay un problema, no salta el evento onChange por lo que no se actualiza correctamente el contenedor del estado.
        public void setMetodoPago(string metodoPago) {
            WaitForBeingClickable(_seleccionarMetodoPagoBy);
            // Se crea la lista desplegable.
            SelectElement selectElement = new SelectElement(_metodoPago());
            // Selecciona la opción que se ha indicado en el parámetro.
            selectElement.SelectByText(metodoPago);
            //TODO:Check if this fix the onChangeMethod
            //_metodoPago().SendKeys(Keys.Enter);
        }

        // Devuelve si el botón Pedir está activo o no.
        public bool isEnabledPedir() {
            IWebElement botonPedir = _botonPedir();

            return botonPedir.Enabled;
        }
    }
}