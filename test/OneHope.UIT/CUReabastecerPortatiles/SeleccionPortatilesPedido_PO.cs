﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHope.UIT.CUReabastecerPortatiles
{
    public class SeleccionPortatilesPedido_PO : PageObject
    {
        private By _modeloPortatilBy = By.Id("portatilModelo");
        private By _marcaPortatilBy = By.Id("portatilMarca");
        private By _stockMinimoPortatilBy = By.Id("portatilStockMinimo");
        private By _stockaMaximoPortatilBy = By.Id("portatilStockMaximo");
        private By _proveedorPortatilBy = By.Id("portatilProveedor");
        private By _nombrePortatilBy = By.Id("portatilNombre");
        private By _botonBuscarBy = By.Id("BuscarPortatiles");
        private By _botonPedirBy = By.Id("Submit");
        private By _tablaPortatilesBy = By.Id("TablaDePortatiles");


        private IWebElement _modeloPortatil() => _driver.FindElement(_modeloPortatilBy);
        private IWebElement _marcaPortatil() => _driver.FindElement(_marcaPortatilBy);
        private IWebElement _stockMinimoPortatil() => _driver.FindElement(_stockMinimoPortatilBy);
        private IWebElement _stockaMaximoPortatil() => _driver.FindElement(_stockaMaximoPortatilBy);
        private IWebElement _proveedorPortatil() => _driver.FindElement(_proveedorPortatilBy);
        private IWebElement _nombrePortatil() => _driver.FindElement(_nombrePortatilBy);
        private IWebElement _botonBuscar() => _driver.FindElement(_botonBuscarBy);
        private IWebElement _botonPedir() => _driver.FindElement(_botonPedirBy);
        private IWebElement _tablaPortatiles() => _driver.FindElement(_tablaPortatilesBy);


        public SeleccionPortatilesPedido_PO(IWebDriver driver, ITestOutputHelper output): base(driver, output)
        {
        }

        // Simular el paso de filtrar los portátiles. El método tendrá un parámetro por cada uno de los filtros que tengas.
        public void FiltrarPortatiles(string? portatilModelo, string? portatilMarca, int? portatilStockMinimo, int? portatilStockMaximo, string? portatilProveedor, string? portatilNombre)
        {
            SelectElement selectElement;

            // Para poder interaccionar con el elemento debe ser visible.
            // El método se le pasa el Id, no la referencia.
            WaitForBeingVisible(_modeloPortatilBy);
            WaitForBeingVisible(_marcaPortatilBy);
            WaitForBeingVisible(_stockMinimoPortatilBy);
            WaitForBeingVisible(_stockaMaximoPortatilBy);
            WaitForBeingVisible(_proveedorPortatilBy);
            WaitForBeingVisible(_nombrePortatilBy);

            // Se simula que se escribe en la cuadro de texto el filtro del nombre del artículo.
            _driver.FindElement(_modeloPortatilBy).SendKeys(portatilModelo);
            //_driver.FindElement(_marcaPortatilBy).SendKeys(portatilMarca);
            _driver.FindElement(_stockMinimoPortatilBy).SendKeys(portatilStockMinimo.ToString());
            _driver.FindElement(_stockaMaximoPortatilBy).SendKeys(portatilStockMaximo.ToString());
            //_driver.FindElement(_proveedorPortatilBy).SendKeys(portatilProveedor);
            _driver.FindElement(_nombrePortatilBy).SendKeys(portatilNombre);


            // Si no se ha seleccionado ningún color, entonces debe seleccionarse "Todos".
            if (portatilMarca == "") portatilMarca = "Todos";

            WaitForBeingVisible(_marcaPortatilBy);
            // Se crea la lista desplegable.
            selectElement = new SelectElement(_marcaPortatil());
            // Selecciona la opción que se ha indicado en el parámetro para el filtro.
            selectElement.SelectByText(portatilMarca);


            if (portatilProveedor == "") portatilProveedor = "Todos";
            WaitForBeingVisible(_proveedorPortatilBy);
            selectElement = new SelectElement(_proveedorPortatil());
            selectElement.SelectByText(portatilProveedor);

            _botonBuscar().Click();
            // Se espera 2000 milisegundos para esperar a que la tabla se recargue.
            System.Threading.Thread.Sleep(2000);
        }


        // Este método permite comprobar si la lista de portátiles mostrada en la tabla coincide con la esperada o no.
        public bool CompruebaListaPortatiles(List<string[]> expectedPortatiles)
        {

            return CheckBodyTable(expectedPortatiles, _tablaPortatilesBy);
        }

        // Devuelve si el botón Pedir está activo o no.
        public bool isEnabledPedir()
        {
            IWebElement botonPedir = _botonPedir();

            return botonPedir.Enabled;
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
        public void Pedir()
        {
            _botonPedir().Click();
            System.Threading.Thread.Sleep(200); // Si no añado este retardo no funcionan las pruebas.
        }
    }
}
