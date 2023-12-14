using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHope.UIT.Shared {
    public class DetalleAlquiler_PO : PageObject {
        private By _tablaPortatilesBy = By.Id("TablaPortatiles");


        public DetalleAlquiler_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output) {
        }

        // Este método permite comprobar si la lista de artículos mostrada en la tabla coincide con la esperada o no.
        public bool CompruebaListaPortatiles(List<string[]> expectedPortatiles) {

            return CheckBodyTable(expectedPortatiles, _tablaPortatilesBy);
        }

        // Este método permite comprobar si la lista de artículos mostrada en la tabla coincide con la esperada o no.
        public bool CompruebaTotal(List<string[]> expectedPortatiles) {

            return CheckFooterTable(expectedPortatiles, _tablaPortatilesBy);
        }

        public bool CheckFooterTable(List<string[]> expectedRows, By IdTable) {
            string expectedRow, actualRow;
            int i, j;
            bool result = true;
            WaitForBeingVisible(IdTable);

            IList<IWebElement> actualrows = _driver
                .FindElement(IdTable)
                .FindElement(By.TagName("tfoot"))
                //.FindElements(By.XPath(".//tr"))
                .FindElements(By.TagName("tr"))//we obtain just the rows of the footer
                .ToList();

            if (actualrows.Count != expectedRows.Count) {
                _output.WriteLine($"Error: \n Expected number of rows:{expectedRows.Count} \n Actual number of rows:{actualrows.Count}");
                return false;
            }

            for (i = 0; i < expectedRows.Count; i++) {
                expectedRow = expectedRows[i][0];
                for (j = 1; j < expectedRows[i].Count(); j++)
                    expectedRow = expectedRow + " " + expectedRows[i][j];
                actualRow = actualrows
                    .Select(m => m.Text) //we return the text of the row
                    .ToList()[i];

                if (!actualRow.StartsWith(expectedRow)) {
                    _output.WriteLine($"Error: \n \t expected row:{expectedRow} \n \t actual row:{actualRow}");
                    result = false;

                }
            }
            return result;

        }
    }
}
