using automatic_web_testing.Helper.Predefined;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Linq;

namespace automatic_web_testing.Helper.Tests
{
    public static class LoginHelper
    {
        /// <summary>
        /// Tato třída se přihlašuje správným loginem (s asserty).
        /// </summary>
        /// <param name="driver">Používá se pro ChromeDriver využívá funkce chromu</param>
        /// <param name="wait">Čekací parametr nastavený na 5 vteřin</param>
        /// <returns>Pokud je vše v pořádku vrací hodnotu true </returns>
        public static bool TryLogin(ChromeDriver driver, WebDriverWait wait)
        {
            try
            {
                var username = SearchHelper.WaitForElementById("Username", driver, 5, 250);

                Assert.IsNotNull(username, "Nenašel se input pro uživatelské jméno");
                var password = driver.FindElement(By.Id("Password"));
                Assert.IsNotNull(password, "Nenašel se input pro heslo míra ocas");

                username.SendKeys(UserHelper.CorrectUser.Email);
                Assert.AreEqual(UserHelper.CorrectUser.Email, username.GetAttribute("value"), "Zadané hodnoty nesouhlasí s obdrženými");
                password.SendKeys(UserHelper.CorrectUser.Password);
                Assert.AreEqual(UserHelper.CorrectUser.Password, password.GetAttribute("value"), "Zadané hodnoty nesouhlasí s obdrženými");

                var prihlasitButton = driver.FindElement(By.Name("button"));
                Assert.IsNotNull(prihlasitButton, "Nenašlo se tlačítko pro přihlášení");
                prihlasitButton.Click();

                wait.Until(e => e.FindElements(By.TagName("button")).FirstOrDefault(f => f.Text == "Zobrazit všechny projekty"));

                var poPrihlaseni = driver.FindElements(By.TagName("button")).First(e => e.Text == "Zobrazit všechny projekty");
                Assert.IsTrue(poPrihlaseni.Displayed, "Nenašel se element pro tlačítka (Zobrazit všechny projekty)");

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Tato třída se přihlašuje správným loginem (bez assertu)
        /// </summary>
        /// <param name="driver">Používá se pro ChromeDriver využívá funkce chromu</param>
        /// <param name="wait">Čekací parametr nastavený na 5 vteřin</param>
        /// <returns>Pokud je vše v pořádku vrátí hodnotu driver v tomto případě jde o hodnotu že se přihlásil</returns>
        public static ChromeDriver Login(ChromeDriver driver, WebDriverWait wait)
        {
            try
            {
                var username = SearchHelper.WaitForElementById("Username", driver, 5, 250);

                var password = driver.FindElement(By.Id("Password"));

                username.SendKeys(UserHelper.CorrectUser.Email);
                password.SendKeys(UserHelper.CorrectUser.Password);

                var prihlasitButton = driver.FindElement(By.Name("button"));
                prihlasitButton.Click();

                wait.Until(e => e.FindElements(By.TagName("button")).FirstOrDefault(f => f.Text == "Zobrazit všechny projekty"));

                var poPrihlaseni = driver.FindElements(By.TagName("button")).First(e => e.Text == "Zobrazit všechny projekty");

                return driver;
            }
            catch
            {
                return null;
            }
        }
    }
}
