using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Chrome;

namespace automatic_web_testing.Helper.Setup
{
    /// <summary>
    /// Tato třída se používá na dostání se na dv1.aspehub.cz.
    /// Obsahuje také vstupní parametr na čekání.
    /// Nakonec obsahuje i funkci dispose která zavírá session. 
    /// </summary>
    public static class ChromeDriverSetup
    {
        /// <summary>
        /// Tahle metoda nám dává vstupní parametr a má v sobě funkci navigate a wait.
        /// </summary>
        /// <param name="url">Vstupní parametr pro webovou adresu</param>
        /// <param name="wait">Výstpuní parametr pro funkci wait => čeká určitou dobu než se mu načte určitá věc</param>
        /// <returns>Vrací hodnotu pro url a wait => adresu kam má jít a jak dlouho má čekat </returns>
        public static ChromeDriver Setup(string url, out WebDriverWait wait)
        {
            var options = new ChromeOptions();
            var driver = new ChromeDriver(options);

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            //wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);

            return driver;
        }
        /// <summary>
        /// Tato metoda nám zavírá session a zbavuje se zbytků co můžou zůstat
        /// </summary>
        /// <param name="driver">Vyvolá příkaz na uzavření a zbaví se historie</param>
        public static void Dispose(ChromeDriver driver)
        {
            driver.Quit();
        }
    }
}
