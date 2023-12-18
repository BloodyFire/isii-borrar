using OneHope.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHope.UIT.CUDevolverPortatil
{
    public class CUDevolverPortatil_UIT : IDisposable
    {

        IWebDriver _driver;
        //A reference to the URI of the web page to test
        string _URI;
        //this may be used whenever some result should be printed in E
        private readonly ITestOutputHelper _output;

        public CUDevolverPortatil_UIT(ITestOutputHelper output)
        {
            UtilitiesUIT.SetUp_UIT(out _driver, out _URI);
            _output = output;
        }

        void IDisposable.Dispose()
        {
            //To close and release all the resources allocated by the web driver
            _driver.Close();
            _driver.Dispose();
            GC.SuppressFinalize(this);
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

        private void Inicio()
        {
            _driver.Navigate()
                .GoToUrl(_URI);
        }

        [Fact]
        public void AP_1_Flujo_Basico()
        {
            // Arrange
            var seleccionarPortatiles_PO = new SeleccionPortatiles_PO(_driver, _output);
            var crearDevolucion_PO = new CrearDevolucion_PO(_driver, _output);
            var detalleDevolucion_PO = new DetalleDevolucion_PO(_driver, _output);

            string expectedCabecera = "Resumen de la devolución";
            string expectedDireccionRecogida = "<b>Dirección: </b> Calle Rosario 12";
            string expectedFecha = "<b>Fecha de devolución: </b> " + DateTime.Now.ToString("dd-MMM-yyyy HH").ToUpper();

            var expectedPortatiles = new List<string[]>();
            expectedPortatiles.Add(new string[] { "18", "DELL-2222", "1"});
            expectedPortatiles.Add(new string[] { "17", "HP-2112", "2"});

            var expectedTotal = new List<string[]>();
            expectedTotal.Add(new string[] { "Cuantía de devolución", "1699,85" });


            // Act
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Devolver Portatiles.
            UtilitiesUIT.WaitForBeingVisible(_driver, By.Id("DevolverPortatilesNavLink"));
            Ir_A_DevolverPortatiles();
            // Seleccionar el portatil 1 y 7
            seleccionarPortatiles_PO.SeleccionarPortatiles(new List<string>() { "18" });
            seleccionarPortatiles_PO.SeleccionarPortatiles(new List<string>() { "17" });
            // Pulsar el botón de devolver.
            seleccionarPortatiles_PO.Devolver();
         
            crearDevolucion_PO.setDireccionRecogida("Calle Rosario 12");
            //Rellenar los datos del cliente.
            crearDevolucion_PO.setMotivoDevolucion("Quiero mas capacidad");
            crearDevolucion_PO.setNotaRepartidor("Estaré sobre las 5 en casa");
            // Poner las cantidades.
            crearDevolucion_PO.setCantidad("18", "1");
            crearDevolucion_PO.setCantidad("17", "2");
            // Pulsar alquilar para finalizar el alquiler.
            crearDevolucion_PO.Devolver();
            // Ahora se debe haber mostrado la página de detalle y puedo comprobar si todo ha ido bien.

            //Esperar a que cargue el detalle
            UtilitiesUIT.WaitForBeingVisible(_driver, By.Id("Submit"));
            // Assert
            Assert.True(_driver.PageSource.Contains(expectedCabecera));
            Assert.True(_driver.PageSource.Contains(expectedDireccionRecogida));
            Assert.True(_driver.PageSource.Contains(expectedFecha));
            Assert.True(detalleDevolucion_PO.CompruebaListaPortatiles(expectedPortatiles));
            Assert.True(detalleDevolucion_PO.CompruebaTotal(expectedTotal));

        }
        
        [Fact]
        public void AP_2_FA0_No_Hay_Portatiles()
        {
            // Arrange -----------
            var expectedText = "No hay artículos que cumplan los criterios seleccionados.";
            var seleccionarPortatiles_PO = new SeleccionPortatiles_PO(_driver, _output);

            // Act ---------
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Alquilar Portatiles.
            Ir_A_DevolverPortatiles();
            // Filtrar los artículos.
            seleccionarPortatiles_PO.FiltrarPortatiles( "19", "" , "");

            // Assert ----------
            // Comprobar que la lista de artículos que ha devuelto es la correcta.
            Assert.Contains(expectedText, _driver.PageSource);
        }
        
        
        
        [Theory]
        //--------- (idCompra, Marca, Cantidad, FechaCompra, Preciounitario, filtroIdCompra, filtroFecha, filtroPrecio)
        [InlineData("18", "DELL", "1", "28/11/2023 0:00:00 +01:00", "499,95", "18", "", "")]
        [InlineData("17", "HP", "2", "29/11/2023 0:00:00 +01:00", "599,95", "", "29/11/2023", "")]
        [InlineData("17", "HP", "2", "29/11/2023 0:00:00 +01:00", "599,95", "", "", "500")]
        public void CU1_3_FA1_Filtrado(string? idCompra, string? marca, string? cantidad, string? fechaCompra,string? precioUnitario, string? filtroIdCompra, string? filtroFecha, string? filtroPrecio)
        {
            // Arrange -----------
            var seleccionarPortatiles_PO = new SeleccionPortatiles_PO(_driver, _output);
            var expectedPortatiles = new List<string[]> { new string[] {idCompra, marca, cantidad, fechaCompra, precioUnitario } };

            // Act ---------
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Comprar Artículos.
            Ir_A_DevolverPortatiles();
            // Filtrar los artículos.
            seleccionarPortatiles_PO.FiltrarPortatiles(filtroIdCompra, filtroFecha, filtroPrecio);

            // Assert ----------
            // Comprobar que la lista de artículos que ha devuelto es la correcta.
            Assert.True(seleccionarPortatiles_PO.CompruebaListaPortatiles(expectedPortatiles));
        }
        

        [Fact]
        public void FA_2_Carrito_Vacio()
        {
            // Arrange
            var seleccionarPortatiles_PO = new SeleccionPortatiles_PO(_driver, _output);

            // Act
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Comprar Artículos.
            Ir_A_DevolverPortatiles();

            //Espere a que cargue la pagina
            UtilitiesUIT.WaitForBeingVisible(_driver, By.Id("Submit"));
            // Assert
            Assert.False(seleccionarPortatiles_PO.isEnabledDevolver());
        }

        [Fact]
        public void FA_6_Volver_Atras()
        {
            // Arrange
            var seleccionarPortatiles_PO = new SeleccionPortatiles_PO(_driver, _output);
            var crearDevolucion_PO = new CrearDevolucion_PO(_driver, _output);
            // Act
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Comprar Artículos.
            Ir_A_DevolverPortatiles();
            // Seleccionar los artículos 3 y 5.
            seleccionarPortatiles_PO.SeleccionarPortatiles(new List<string>() { "18", "17" });
            // Pulsar el botón de comprar.
            seleccionarPortatiles_PO.Devolver();
            // Ahora volver.
            crearDevolucion_PO.Volver();

            // Assert
            Assert.True(seleccionarPortatiles_PO.ComprobarSeleccionPortatiles(new List<string>() { "18", "17" }));
        }


        [Fact]
        public void CU1_8_FA5_Datos_Obligatorios_DireccionRecogida()
        {
            // Arrange
            var seleccionarPortatiles_PO = new SeleccionPortatiles_PO(_driver, _output);
            var crearDevolucion_PO = new CrearDevolucion_PO(_driver, _output);
            string expectedError = "(*) The Dirección de Recogida field is required.";
            // Act
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Pedir Portátiles.
            Ir_A_DevolverPortatiles();
            // Seleccionar los portátiles 9 y 5.
            seleccionarPortatiles_PO.SeleccionarPortatiles(new List<string>() { "17" });
            // Pulsar el botón de pedir.
            seleccionarPortatiles_PO.Devolver();

            crearDevolucion_PO.setMotivoDevolucion("Quiero mas capacidad");
            // Pulsar Realizar pedido para intentar finalizar el pedido.
            crearDevolucion_PO.Devolver();
            // Assert
            Assert.Contains(expectedError, _driver.PageSource);
        }

        [Fact]
        public void CU1_8_FA5_Datos_Obligatorios_MotivoDevolucion()
        {
            // Arrange
            var seleccionarPortatiles_PO = new SeleccionPortatiles_PO(_driver, _output);
            var crearDevolucion_PO = new CrearDevolucion_PO(_driver, _output);
            string expectedError = "(*) The Motivo Devolucion field is required.";
            // Act
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Pedir Portátiles.
            Ir_A_DevolverPortatiles();
            // Seleccionar los portátiles 9 y 5.
            seleccionarPortatiles_PO.SeleccionarPortatiles(new List<string>() { "17" });
            // Pulsar el botón de pedir.
            seleccionarPortatiles_PO.Devolver();

            crearDevolucion_PO.setDireccionRecogida("Calle Rosario 12");
            // Pulsar Realizar pedido para intentar finalizar el pedido.
            crearDevolucion_PO.Devolver();
            // Assert
            Assert.Contains(expectedError, _driver.PageSource);
        }

        [Fact]
        public void CU1_9_FA4_Cantidad()
        {
            // Arrange
            var seleccionarPortatiles_PO = new SeleccionPortatiles_PO(_driver, _output);
            var crearDevolucion_PO = new CrearDevolucion_PO(_driver, _output);
            string expectedError = "(*) Error! No puedes devolver mas portatiles de los comprados";
            // Act
            // Si has usado autenticación tendrás hacer login, en mi ejemplo no se usa.
            Inicio();
            // Navegar hasta la página de Devolver Portátiles.
            Ir_A_DevolverPortatiles();
            // Seleccionar la compra con el id 17.
            seleccionarPortatiles_PO.SeleccionarPortatiles(new List<string>() { "17" });
            // Pulsar el botón de pedir.
            seleccionarPortatiles_PO.Devolver();

            crearDevolucion_PO.setDireccionRecogida("Calle Rosario 12");
            crearDevolucion_PO.setMotivoDevolucion("Quiero mas capacidad");
            crearDevolucion_PO.setNotaRepartidor("Estaré sobre las 5 en casa");
            crearDevolucion_PO.setCantidad("17", "5");
            // Pulsar Realizar pedido para intentar finalizar el pedido.
            crearDevolucion_PO.Devolver();
            // Assert
            Assert.Contains(expectedError, _driver.PageSource);
        }



        private void Ir_A_DevolverPortatiles()
        {
            // Esperar a que se cargue la página.
            UtilitiesUIT.WaitForBeingVisible(_driver, By.Id("DevolverPortatilesNavLink"));
            // Pulsamos en la opción del menú de navegación de Comprar Artículos.
            _driver.FindElement(By.Id("DevolverPortatilesNavLink")).Click();
        }
    }
}