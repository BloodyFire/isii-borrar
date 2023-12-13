using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHope.UIT.CUReabastecerPortatiles
{

    public class CUReabastecerPortatiles_UIT : IDisposable
    {

        IWebDriver _driver;
        string _URI;
        private readonly ITestOutputHelper _output;

        public CUReabastecerPortatiles_UIT(ITestOutputHelper output)
        {
            //it is needed to run the browser and
            //know the URI of your app
            UtilitiesUIT.SetUp_UIT(out _driver, out _URI);
            //it is initialized using the logger provided by xUnit
            this._output = output;
        }

        void IDisposable.Dispose()
        {
            _driver.Close();
            _driver.Dispose();
            GC.SuppressFinalize(this);
        }

        // Navegar a la página inicial de la web.
        private void Inicio()
        {
            _driver.Navigate()
                .GoToUrl(_URI);
        }

        private void Ir_A_ReabastecerPortatiles()
        {
            // Esperar a que se cargue la página.
            UtilitiesUIT.WaitForBeingVisible(_driver, By.Id("PedirPortatilesNavLink"));
            // Pulsamos en la opción del menú de navegación de Reabastecer Portátiles.
            _driver.FindElement(By.Id("PedirPortatilesNavLink")).Click();
        }

        [Fact]
        public void Initial_step_opening_the_web_page()
        {
            //Arrange
            string expectedTitle = "Index";
            string expectedText = "Register";
            //Act
            Inicio();

            //Assert
            //Comprueba que el título coincide con el esperado
            Assert.Equal(expectedTitle, _driver.Title);

            //Comprueba si la página contiene el string indicado
            Assert.Contains(expectedText, _driver.PageSource);
        }









    }
}
