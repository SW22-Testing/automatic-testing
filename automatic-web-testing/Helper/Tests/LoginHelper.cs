using automatic_web_testing.Helper.Predefined;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Linq;

namespace automatic_web_testing.Helper.Tests
{
    public class LoginHelper
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
                IWebElement username = SearchHelper.WaitForElementById("Username", driver, 5, 250);

                Assert.IsNotNull(username, "Nenašel se input pro uživatelské jméno");
                IWebElement password = driver.FindElement(By.Id("Password"));
                Assert.IsNotNull(password, "Nenašel se input pro heslo míra ocas");

                username.SendKeys(UserHelper.CorrectUser.Email);
                Assert.AreEqual(UserHelper.CorrectUser.Email, username.GetAttribute("value"), "Zadané hodnoty nesouhlasí s obdrženými");
                password.SendKeys(UserHelper.CorrectUser.Password);
                Assert.AreEqual(UserHelper.CorrectUser.Password, password.GetAttribute("value"), "Zadané hodnoty nesouhlasí s obdrženými");

                IWebElement prihlasitButton = driver.FindElement(By.Name("button"));
                Assert.IsNotNull(prihlasitButton, "Nenašlo se tlačítko pro přihlášení");
                prihlasitButton.Click();

                wait.Until(e => e.FindElements(By.TagName("button")).Where(e => e.Text == "Zobrazit všechny projekty").FirstOrDefault());

                IWebElement poPrihlaseni = driver.FindElements(By.TagName("button")).Where(e => e.Text == "Zobrazit všechny projekty").First();
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
                IWebElement username = SearchHelper.WaitForElementById("Username", driver, 5, 250);

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
        /// <summary>
        /// Tato třída se přihlašuje špatným emailem (username).
        /// Kontroluje se zde chybová hláška.
        /// </summary>
        /// <param name="driver">Používá se pro ChromeDriver využívá funkce chromu</param>
        /// <param name="wait">Čekací parametr nastavený na 5 vteřin</param>
        /// <returns>Pokud je vše v pořádku vrací hodnotu true </returns>
        public static bool BadLoginUsername(ChromeDriver driver, WebDriverWait wait)
        {
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Username")));

                IWebElement username = driver.FindElement(By.Id("Username"));
                Assert.IsNotNull(username, "Nenašel se element pro username");
                IWebElement password = driver.FindElement(By.Id("Password"));
                Assert.IsNotNull(password, "Nenašel se element pro password");

                username.SendKeys(UserHelper.IncorrectUser.Email);
                Assert.AreEqual(UserHelper.IncorrectUser.Email, username.GetAttribute("value"), "Zadané hodnoty nesouhlasí s obdrženými");
                password.SendKeys(UserHelper.CorrectUser.Password);
                Assert.AreEqual(UserHelper.CorrectUser.Password, password.GetAttribute("value"), "Zadané hodnoty nesouhlasí s obdrženými");

                IWebElement prihlasitButton = driver.FindElement(By.Name("button"));
                Assert.IsNotNull(prihlasitButton, "Nenašlo se tlačítko pro přihlášení");
                prihlasitButton.Click();

                //Pokud bude problém udělá se třída
                IWebElement errorMessage = driver.FindElement(By.ClassName("alert-danger"));
                Assert.IsTrue(errorMessage.Displayed, "Nenašla se zpráva o chybě");

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Tato třída se přihlašuje špatným heslem.
        /// Taky se kontroluje chybová hláška.
        /// </summary>
        /// <param name="driver">Používá se pro ChromeDriver využívá funkce chromu</param>
        /// <param name="wait">Čekací parametr nastavený na 5 vteřin</param>
        /// <returns>Pokud je vše v pořádku vrátí hodnotu true</returns>
        public static bool BadLoginPassword(ChromeDriver driver, WebDriverWait wait)
        {
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Username")));

                IWebElement username = driver.FindElement(By.Id("Username"));
                Assert.IsNotNull(username, "Nenašel se element pro username");
                IWebElement password = driver.FindElement(By.Id("Password"));
                Assert.IsNotNull(password, "Nenašel se element pro password");

                username.SendKeys(UserHelper.CorrectUser.Email);
                Assert.AreEqual(UserHelper.CorrectUser.Email, username.GetAttribute("value"), "Zadané hodnoty nesouhlasí s obdrženými");
                password.SendKeys(UserHelper.IncorrectUser.Password);
                Assert.AreEqual(UserHelper.IncorrectUser.Password, password.GetAttribute("value"), "Zadané hodnoty nesouhlasí s obdrženými");

                IWebElement prihlasitButton = driver.FindElement(By.Name("button"));
                Assert.IsNotNull(prihlasitButton, "Nenašlo se tlačítko pro přihlášení");
                prihlasitButton.Click();

                //Pokud bude problém udělá se třída
                IWebElement errorMessage = driver.FindElement(By.ClassName("alert-danger"));
                Assert.IsTrue(errorMessage.Displayed, "Nenašla se zpráva o chybě");

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Tato třída zadává oba údaje špatně.
        /// Kontroluje se zde opět chybová hláška.
        /// </summary>
        /// <param name="driver">Používá se pro ChromeDriver využívá funkce chromu</param>
        /// <param name="wait">Čekací parametr nastavený na 5 vteřin</param>
        /// <returns>Pokud vše projde vrátí hodnotu true</returns>
        public static bool BadLoginBoth(ChromeDriver driver, WebDriverWait wait)
        {
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Username")));

                IWebElement username = driver.FindElement(By.Id("Username"));
                Assert.IsNotNull(username, "Nenašel se element pro username");
                IWebElement password = driver.FindElement(By.Id("Password"));
                Assert.IsNotNull(password, "Nenašel se element pro password");

                username.SendKeys(UserHelper.IncorrectUser.Email);
                Assert.AreEqual(UserHelper.IncorrectUser.Email, username.GetAttribute("value"), "Zadané hodnoty nesouhlasí s obdrženými");
                password.SendKeys(UserHelper.IncorrectUser.Password);
                Assert.AreEqual(UserHelper.IncorrectUser.Password, password.GetAttribute("value"), "Zadané hodnoty nesouhlasí s obdrženými");

                IWebElement prihlasitButton = driver.FindElement(By.Name("button"));
                Assert.IsNotNull(prihlasitButton, "Nenašlo se tlačítko pro přihlášení");
                prihlasitButton.Click();

                //Pokud bude problém udělá se třída
                IWebElement errorMessage = driver.FindElement(By.ClassName("alert-danger"));
                Assert.IsTrue(errorMessage.Displayed, "Nenašla se zpráva o chybě");

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
