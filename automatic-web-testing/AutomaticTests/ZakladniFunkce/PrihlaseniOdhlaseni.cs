using automatic_web_testing.Helper.Predefined;
using automatic_web_testing.Helper.Setup;
using automatic_web_testing.Helper.Tests;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Linq;
using System.Threading;

namespace automatic_web_testing.AutomaticTests.ZakladniFunkce
{
    public class PrihlaseniOdhlaseni
    {
        ChromeDriver driver { get; set; }
        private WebDriverWait wait { get; set; }
        [SetUp]
        public void Setup()
        {
            WebDriverWait _wait;
            driver = ChromeDriverSetup.Setup("https://dv1.aspehub.cz/Account", out _wait);
            wait = _wait;
        }
        [TestCase(TestName = "Přihlášení chybný email", Description = "Test kontroluje přihlášení s chybným uživatelským emailem"), Order(1)]
        public void PrihlaseniChybneJmeno()
        {
            bool status = BadLoginUsername(driver,wait);
            Assert.IsTrue(status);
        }
        [TestCase(TestName = "Přihlášení chybné heslo", Description = "Test kontroluje přihlášení s chybným heslem"), Order(2)]
        public void PrihlaseniChybneHeslo()
        {
            bool status = BadLoginPassword(driver, wait);
            Assert.IsTrue(status);
        }
        [TestCase(TestName = "Přihlášení chybné heslo a email", Description = "Test kontroluje přihlášení s chybným heslem a mailem"), Order(3)]
        public void PrihlaseniObeChybne()
        {
            bool status = BadLoginBoth(driver, wait);
            Assert.IsTrue(status);
        }
        [TestCase(TestName = "Přihlášení správnými údaji", Description = "Test se přihlásí správnými údaji"), Order(4)]
        public void PrihlaseniSpravne()
        {
            bool status = LoginHelper.TryLogin(driver, wait);
            Assert.IsTrue(status);
        }
        [TestCase(TestName = "Externí přihlášení přes Microsoft", Description = "Test kontroluje externí přihlášení přes microsoft přidá ho, přihlásí se přes něj a potom ho i odebere"), Order(5)]
        public void PrihlaseniMicrosoft()
        {
            driver = LoginHelper.Login(driver, wait);
            Assert.NotNull(driver);

            driver = ExternalLogin(driver, wait);
            Assert.NotNull(driver);

            IWebElement addMicrosoft = driver.FindElements(By.ClassName("btn-primary")).Where(e => e.GetAttribute("title") == "Log in using your Microsoft account").First();
            addMicrosoft.Click();

            IWebElement usernameMicrosoft = SearchHelper.WaitForElementById("i0116", driver, 5, 250);

            usernameMicrosoft.SendKeys(UserHelper.MicrosoftUser.Email);
            Assert.AreEqual(UserHelper.MicrosoftUser.Email, usernameMicrosoft.GetAttribute("value"));

            IWebElement confirmMicrosoft = driver.FindElements(By.TagName("input")).Where(e => e.GetAttribute("type") == "submit").First();
            confirmMicrosoft.Click();

            SearchHelper.WaitForElementById("idA_PWD_ForgotPassword", driver, 5, 250);

            IWebElement passwordMicrosoft = driver.FindElement(By.ClassName("ext-text-box"));
            Assert.NotNull(passwordMicrosoft, "heslo neni");
            passwordMicrosoft.SendKeys(UserHelper.MicrosoftUser.Password);
            Assert.AreEqual(UserHelper.MicrosoftUser.Password, passwordMicrosoft.GetAttribute("value"));

            confirmMicrosoft = driver.FindElements(By.TagName("input")).Where(e => e.GetAttribute("type") == "submit").First();
            confirmMicrosoft.Click();

            IWebElement noRemember = driver.FindElement(By.Id("idBtn_Back"));
            noRemember.Click();

            IWebElement logoutIdentity = driver.FindElements(By.TagName("a")).Where(e => e.Text == "Odhlásit se").FirstOrDefault();
            logoutIdentity.Click();

            IWebElement logoutConfirm = driver.FindElements(By.TagName("button")).Where(e => e.Text == "Ano").FirstOrDefault();
            logoutConfirm.Click();

            driver.Navigate().GoToUrl("https://dv1.aspehub.cz/Account");

            IWebElement username = SearchHelper.WaitForElementById("Username", driver, 5, 250);

            IWebElement exterLoginMicrosoft = driver.FindElements(By.TagName("a")).Where(e => e.Text == "Microsoft").FirstOrDefault();
            exterLoginMicrosoft.Click();

            driver = ExternalLogin(driver, wait);
            Assert.NotNull(driver);

            IWebElement deleteMicrosoft = driver.FindElements(By.ClassName("btn-primary")).Where(e => e.GetAttribute("title") == "Remove this Microsoft login from your account").First();
            deleteMicrosoft.Click();
        }
        [TestCase(TestName = "Externí přihlášení přes Google", Description = "Test kontroluje externí přihlášení přes google přidá ho, přihlásí se přes něj a potom ho i odebere"), Order(6)]
        public void PrihlaseniGoogle()
        {
            driver = LoginHelper.Login(driver, wait);
            Assert.NotNull(driver);

            driver = ExternalLogin(driver, wait);
            Assert.NotNull(driver);

            IWebElement addGoogle = driver.FindElements(By.ClassName("btn-primary")).Where(e => e.GetAttribute("title") == "Log in using your Google account").FirstOrDefault();
            addGoogle.Click();

            wait.Until(e => e.FindElement(By.Id("identifierId")));
            IWebElement usernameGoogle = driver.FindElement(By.Id("identifierId"));
            usernameGoogle.SendKeys(UserHelper.GoogleUser.Email);
            Assert.AreEqual(UserHelper.GoogleUser.Email, usernameGoogle.GetAttribute("value"));

            IWebElement nextLogin = driver.FindElements(By.TagName("button")).Where(e => e.Text == "Další").FirstOrDefault();
            nextLogin.Click();

            wait.Until(e => e.FindElements(By.TagName("span")).Where(e => e.Text == "Zapomněli jste heslo?")).FirstOrDefault();

            IWebElement passwordGoogle = driver.FindElement(By.Name("password"));
            passwordGoogle.SendKeys(UserHelper.GoogleUser.Password);
            Assert.AreEqual(UserHelper.GoogleUser.Password, passwordGoogle.GetAttribute("value"));

            passwordGoogle.SendKeys(Keys.Enter);

            Thread.Sleep(1500);

            IWebElement logoutIdentity = driver.FindElements(By.TagName("a")).Where(e => e.Text == "Odhlásit se").FirstOrDefault();
            logoutIdentity.Click();

            IWebElement logoutConfirm = driver.FindElements(By.TagName("button")).Where(e => e.Text == "Ano").FirstOrDefault();
            logoutConfirm.Click();

            driver.Navigate().GoToUrl("https://dv1.aspehub.cz/Account");

            IWebElement username = SearchHelper.WaitForElementById("Username", driver, 5, 250);

            IWebElement exterLoginGoogle = driver.FindElements(By.TagName("a")).Where(e => e.Text == "Google").FirstOrDefault();
            exterLoginGoogle.Click();

            driver = ExternalLogin(driver, wait);
            Assert.NotNull(driver);

            IWebElement deleteGoogle = driver.FindElements(By.ClassName("btn-primary")).Where(e => e.GetAttribute("title") == "Remove this Google login from your account").FirstOrDefault();
            deleteGoogle.Click();
        }

        [TearDown]
        public void TearDown()
        {
            ChromeDriverSetup.Dispose(driver);
        }
        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        
        /// <summary>
        /// Tato třída jde do nastavení uživatele => externího přihlášení
        /// </summary>
        /// <param name="driver">Využívá funkce Chromu (ChromeDriver)</param>
        /// <param name="wait">Vyčkává než se určitá věc objeví defaultně 5 vteřin</param>
        /// <returns>Vrací hodnotu driver. Kde tato hodnota obsahuje jestli se dostal test k nastavení externího přihlášení</returns>
        public static ChromeDriver ExternalLogin(ChromeDriver driver, WebDriverWait wait)
        {
            try
            {
                wait.Until(e => e.FindElements(By.ClassName("MainModule.Header_action__1YiP5")).Where(e => e.GetAttribute("title") == UserHelper.CorrectUser.Email).FirstOrDefault());

                IWebElement profile = driver.FindElements(By.ClassName("MainModule.Header_action__1YiP5")).Where(e => e.GetAttribute("title") == UserHelper.CorrectUser.Email).First();
                Assert.NotNull(profile);
                profile.Click();

                IWebElement profiler = driver.FindElement(By.ClassName("anticon-setting"));
                Assert.NotNull(profiler);
                profiler.Click();

                IWebElement extraLogin = driver.FindElement(By.LinkText("Moje externí přihlášení"));
                Assert.NotNull(extraLogin);
                extraLogin.Click();

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
