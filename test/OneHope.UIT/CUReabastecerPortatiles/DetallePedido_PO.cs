using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHope.UIT.Shared {
    public class DetallePedido_PO : PageObject {
        private By _tablaPortatilesBy = By.Id("TablaPortatiles");


        public DetallePedido_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output) {
        }

        // Este método permite comprobar si la lista de portátiles mostrada en la tabla coincide con la esperada o no.
        public bool CompruebaListaPortatiles(List<string[]> expectedPortatiles) {

            return CheckBodyTable(expectedPortatiles, _tablaPortatilesBy);
        }

        // Este método permite comprobar si la lista de portátiles mostrada en la tabla coincide con la esperada o no.
        public bool CompruebaTotal(List<string[]> expectedPortatiles) {

            return CheckFooterTable(expectedPortatiles, _tablaPortatilesBy);
        }

    }
}
