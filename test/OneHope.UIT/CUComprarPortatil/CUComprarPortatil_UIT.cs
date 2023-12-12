using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace OneHope.UIT.CUComprarPortatil
{
    public class CUComprarPortatil_UIT : IDisposable
    {
        //Webdriver: A reference to the browser
        IWebDriver _driver;

        //A reference to the URI of the web page to test
        string _URI;

        //this may be used whenever some result should be printed in E
        private readonly ITestOutputHelper _output;

        //The code for your test Methods goes here
        public CUComprarPortatil_UIT(ITestOutputHelper output)
        {
            //it is needed to run the browser and
            //know the URI of your app
            Shared.UtilitiesUIT.SetUp_UIT(out _driver, out _URI);

            //it is initialized using the logger provided by xUnit
            this._output = output;
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
    }
}
