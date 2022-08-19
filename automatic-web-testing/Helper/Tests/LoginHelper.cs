using automatic_web_testing.Helper.Predefined;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Linq;

namespace automatic_web_testing.Helper.Tests
{
    public class LoginHelper
    {
        /// <summary>
        /// Tato třída se přihlašuje správným loginem (s asserty).
        /// </summary>
        /// <param name="driver">Používá se pro ChromeDriver</param>
        /// <param name="wait">Čekací parametr nastavený na 5 vteřin</param>
        /// <returns>Pokud je vše v pořádku vrací hodnotu pravda (je to v češtině pro míru :))</returns>
        public static bool TryLogin(ChromeDriver driver, WebDriverWait wait)
        {
            try
            {
                IWebElement username = SearchHelper.WaitForElementById("Username", driver, 5, 250);

                Assert.IsNotNull(username, "Nenašel se input pro uživatelské jméno");
                IWebElement password = driver.FindElement(By.Id("Password"));
                Assert.IsNotNull(password, "Nenašel se input pro heslo míra ocas");

                username.SendKeys(UserHelper.CorrectUser.Email);
                Assert.AreEqual(UserHelper.CorrectUser.Email, username.GetAttribute("value"));
                password.SendKeys(UserHelper.CorrectUser.Password);
                Assert.AreEqual(UserHelper.CorrectUser.Password, password.GetAttribute("value"));

                IWebElement prihlasitButton = driver.FindElement(By.Name("button"));
                Assert.IsNotNull(prihlasitButton, "Nenašlo se tlačítko pro přihlášení");
                prihlasitButton.Click();

                wait.Until(e => e.FindElements(By.TagName("button")).Where(e => e.Text == "Zobrazit všechny projekty").FirstOrDefault());

                IWebElement poPrihlaseni = driver.FindElements(By.TagName("button")).Where(e => e.Text == "Zobrazit všechny projekty").First();
                Assert.IsTrue(poPrihlaseni.Displayed);

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Tato třída se přihlašuje správným loginem (bez assertů).
        /// </summary>
        /// <param name="driver">Používá se pro ChromeDriver</param>
        /// <param name="wait">Čekací parametr nastavený na 5 vteřin</param>
        /// <returns>Pokud je vše v pořádku vrací hodnotu pravda (je to v češtině pro míru :))</returns>
        public static ChromeDriver Login(ChromeDriver driver, WebDriverWait wait)
        {
            try
            {
                wait.Until(e => e.FindElement(By.Id("Username")));

                IWebElement username = driver.FindElement(By.Id("Username"));
                IWebElement password = driver.FindElement(By.Id("Password"));

                username.SendKeys(UserHelper.CorrectUser.Email);
                password.SendKeys(UserHelper.CorrectUser.Password);

                IWebElement prihlasitButton = driver.FindElement(By.Name("button"));
                prihlasitButton.Click();

                wait.Until(e => e.FindElements(By.TagName("button")).Where(e => e.Text == "Zobrazit všechny projekty").FirstOrDefault());

                IWebElement poPrihlaseni = driver.FindElements(By.TagName("button")).Where(e => e.Text == "Zobrazit všechny projekty").First();

                return driver;
            }
            catch
            {
                return null;
            }
        }
    }
}
