using System;
using System.Linq;
using System.Threading;
using automatic_web_testing.Helper.Predefined;
using automatic_web_testing.Helper.Setup;
using automatic_web_testing.Helper.Tests;
using automatic_web_testing.Page;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using screen_recorder;
using SeleniumExtras.WaitHelpers;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace automatic_web_testing.AutomaticTests.ZakladniFunkce
{
    public class PrihlaseniOdhlaseni
    {
        private ChromeDriver driver { get; set; }
        private ScreenRecorder Recorder { get; set; }
        private WebDriverWait wait { get; set; }
        private LoginPage loginPage { get; set; }

        [SetUp]
        public void Setup()
        {
            WebDriverWait _wait;
            driver = ChromeDriverSetup.Setup("https://dv1.aspehub.cz/Account", out _wait);
            Recorder = new ScreenRecorder();
            

            wait = _wait;
        }

        [TestCase(TestName = "Přihlášení chybný email",
            Description = "Test kontroluje přihlášení s chybným uživatelským emailem")]
        [Order(1)]
        public void PrihlaseniChybneJmeno()
        {
            Recorder.StartRecording(TestContext.CurrentContext.Test.Name, "Základní funkce", "", "AspeHub");
            var status = BadLoginUsername(driver, wait);
            Assert.IsTrue(status);
        }

        [TestCase(TestName = "Přihlášení chybné heslo", Description = "Test kontroluje přihlášení s chybným heslem")]
        [Order(2)]
        public void PrihlaseniChybneHeslo()
        {
            Recorder.StartRecording(TestContext.CurrentContext.Test.Name, "Základní funkce", "", "AspeHub");
            var status = BadLoginPassword(driver, wait);
            Assert.IsTrue(status);
        }

        [TestCase(TestName = "Přihlášení chybné heslo a email",
            Description = "Test kontroluje přihlášení s chybným heslem a mailem")]
        [Order(3)]
        public void PrihlaseniObeChybne()
        {
            Recorder.StartRecording(TestContext.CurrentContext.Test.Name, "Základní funkce", "", "AspeHub");
            var status = BadLoginBoth(driver, wait);
            Assert.IsTrue(status);
        }

        [TestCase(TestName = "Přihlášení správnými údaji", Description = "Test se přihlásí správnými údaji")]
        [Order(4)]
        public void PrihlaseniSpravne()
        {
            Recorder.StartRecording(TestContext.CurrentContext.Test.Name, "Základní funkce", "", "AspeHub");
            //var status = LoginHelper.TryLogin(driver, wait);
            //Assert.IsTrue(status);
            loginPage = new LoginPage(driver);
            loginPage.Login(UserHelper.CorrectUser.Email, UserHelper.CorrectUser.Password);
        }

        [TestCase(TestName = "Externí přihlášení přes Microsoft",
            Description =
                "Test kontroluje externí přihlášení přes microsoft přidá ho, přihlásí se přes něj a potom ho i odebere")]
        [Order(5)]
        public void PrihlaseniMicrosoft()
        {
            Recorder.StartRecording(TestContext.CurrentContext.Test.Name, "Základní funkce", "", "AspeHub");
            driver = LoginHelper.Login(driver, wait);
            Assert.NotNull(driver);
            driver = ExternalLogin(driver, wait);
            Assert.NotNull(driver);

            var deleteMicrosoftsearch = driver.FindElements(By.ClassName("btn-primary")).FirstOrDefault(e =>
                e.GetAttribute("title") == "Remove this Microsoft login from your account");
            if (deleteMicrosoftsearch is { Displayed: true })
                deleteMicrosoftsearch.Click();

            var addMicrosoft = driver.FindElements(By.ClassName("btn-primary"))
                .First(e => e.GetAttribute("title") == "Log in using your Microsoft account");
            addMicrosoft.Click();

            var usernameMicrosoft = SearchHelper.WaitForElementById("i0116", driver, 5, 250);
            usernameMicrosoft.SendKeys(UserHelper.MicrosoftUser.Email);
            Assert.AreEqual(UserHelper.MicrosoftUser.Email, usernameMicrosoft.GetAttribute("value"));

            var confirmMicrosoft =
                driver.FindElements(By.TagName("input")).First(e => e.GetAttribute("type") == "submit");
            confirmMicrosoft.Click();

            SearchHelper.WaitForElementById("idA_PWD_ForgotPassword", driver, 5, 250);
            var passwordMicrosoft = driver.FindElement(By.ClassName("ext-text-box"));

            Assert.NotNull(passwordMicrosoft, "heslo neni");
            passwordMicrosoft.SendKeys(UserHelper.MicrosoftUser.Password);
            Assert.AreEqual(UserHelper.MicrosoftUser.Password, passwordMicrosoft.GetAttribute("value"));
            confirmMicrosoft = driver.FindElements(By.TagName("input")).First(e => e.GetAttribute("type") == "submit");
            confirmMicrosoft.Click();

            var noRemember = driver.FindElement(By.Id("idBtn_Back"));
            noRemember?.Click();

            var logoutIdentity = driver.FindElements(By.TagName("a")).FirstOrDefault(e => e.Text == "Odhlásit se");
            logoutIdentity?.Click();
            var logoutConfirm = driver.FindElements(By.TagName("button")).FirstOrDefault(e => e.Text == "Ano");
            logoutConfirm?.Click();

            driver.Navigate().GoToUrl("https://dv1.aspehub.cz/Account");
            var checkMicrosoft = SearchHelper.WaitForElementByTagName("a", driver, 5, 250);
            Console.WriteLine(checkMicrosoft.Text);
            var exterLoginMicrosoft = driver.FindElements(By.TagName("a")).FirstOrDefault(e => e.Text == "Microsoft");
            exterLoginMicrosoft?.Click();

            driver = ExternalLogin(driver, wait);
            Assert.NotNull(driver);

            var deleteMicrosoft = driver.FindElements(By.ClassName("btn-primary")).First(e =>
                e.GetAttribute("title") == "Remove this Microsoft login from your account");
            deleteMicrosoft.Click();
        }

        [TestCase(TestName = "Externí přihlášení přes Google",
            Description =
                "Test kontroluje externí přihlášení přes google přidá ho, přihlásí se přes něj a potom ho i odebere")]
        [Order(6)]
        public void PrihlaseniGoogle()
        {
            Recorder.StartRecording(TestContext.CurrentContext.Test.Name, "Základní funkce", "", "AspeHub");
            driver = LoginHelper.Login(driver, wait);
            Assert.NotNull(driver);
            driver = ExternalLogin(driver, wait);
            Assert.NotNull(driver);

            var deleteGooglesearch = driver.FindElements(By.ClassName("btn-primary"))
                .FirstOrDefault(e => e.GetAttribute("title") == "Remove this Google login from your account");
            if (deleteGooglesearch is { Displayed: true })
                deleteGooglesearch.Click();

            var addGoogle = driver.FindElements(By.ClassName("btn-primary"))
                .FirstOrDefault(e => e.GetAttribute("title") == "Log in using your Google account");
            addGoogle?.Click();

            wait.Until(e => e.FindElement(By.Id("identifierId")));
            var usernameGoogle = driver.FindElement(By.Id("identifierId"));
            usernameGoogle.SendKeys(UserHelper.GoogleUser.Email);
            Assert.AreEqual(UserHelper.GoogleUser.Email, usernameGoogle.GetAttribute("value"));

            var nextLogin = driver.FindElements(By.TagName("button")).FirstOrDefault(e => e.Text == "Další");
            nextLogin?.Click();

            wait.Until(e => e.FindElements(By.TagName("span")).Where(f => f.Text == "Zapomněli jste heslo?"));
            Thread.Sleep(1500);
            var passwordGoogle = driver.FindElement(By.Name("password"));
            passwordGoogle.SendKeys(UserHelper.GoogleUser.Password);
            Assert.AreEqual(UserHelper.GoogleUser.Password, passwordGoogle.GetAttribute("value"));

            passwordGoogle.SendKeys(Keys.Enter);

            //TODO: Dodělej Asserty
            var logoutIdentity = SearchHelper.WaitForElementByClassName("btn-outline-primary", driver, 5, 250);
            logoutIdentity?.Click();
            var logoutConfirm = driver.FindElement(By.XPath("//div[@class='form-group']//button"));
            logoutConfirm?.Click();

            driver.Navigate().GoToUrl("https://dv1.aspehub.cz/Account");
            var username = SearchHelper.WaitForElementById("Username", driver, 5, 250);
            Assert.NotNull(username);

            var exterLoginGoogle = driver.FindElements(By.TagName("a")).FirstOrDefault(e => e.Text == "Google");
            exterLoginGoogle?.Click();

            driver = ExternalLogin(driver, wait);
            Assert.NotNull(driver);

            var deleteGoogle = driver.FindElements(By.ClassName("btn-primary")).FirstOrDefault(e =>
                e.GetAttribute("title") == "Remove this Google login from your account");
            deleteGoogle?.Click();
        }

        [TearDown]
        public void TearDown()
        {
            Recorder.StopRecording();
            ChromeDriverSetup.Dispose(driver);
        }


        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        ///     Tato třída jde do nastavení uživatele => externího přihlášení
        /// </summary>
        /// <param name="driver">Využívá funkce Chromu (ChromeDriver)</param>
        /// <param name="wait">Vyčkává než se určitá věc objeví defaultně 5 vteřin</param>
        /// <returns>Vrací hodnotu driver. Kde tato hodnota obsahuje jestli se dostal test k nastavení externího přihlášení</returns>
        private static ChromeDriver ExternalLogin(ChromeDriver driver, WebDriverWait wait)
        {
            try
            {
                wait.Until(e =>
                    e.FindElements(By.ClassName("MainModule.Header_action__1YiP5"))
                        .FirstOrDefault(f => f.GetAttribute("title") == UserHelper.CorrectUser.Email));
                var profile = driver.FindElements(By.ClassName("MainModule.Header_action__1YiP5"))
                    .First(e => e.GetAttribute("title") == UserHelper.CorrectUser.Email);
                Assert.NotNull(profile);
                profile.Click();
                var profiler = driver.FindElement(By.ClassName("anticon-setting"));
                Assert.NotNull(profiler);
                profiler.Click();
                var extraLogin = driver.FindElement(By.LinkText("Moje externí přihlášení"));
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
        ///     Tato třída se přihlašuje špatným emailem (username).
        ///     Kontroluje se zde chybová hláška.
        /// </summary>
        /// <param name="driver">Používá se pro ChromeDriver využívá funkce chromu</param>
        /// <param name="wait">Čekací parametr nastavený na 5 vteřin</param>
        /// <returns>Pokud je vše v pořádku vrací hodnotu true </returns>
        private static bool BadLoginUsername(ChromeDriver driver, WebDriverWait wait)
        {
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Username")));
                var username = driver.FindElement(By.Id("Username"));
                Assert.IsNotNull(username, "Nenašel se element pro username");
                var password = driver.FindElement(By.Id("Password"));
                Assert.IsNotNull(password, "Nenašel se element pro password");
                username.SendKeys(UserHelper.IncorrectUser.Email);
                Assert.AreEqual(UserHelper.IncorrectUser.Email, username.GetAttribute("value"),
                    "Zadané hodnoty nesouhlasí s obdrženými");
                password.SendKeys(UserHelper.CorrectUser.Password);
                Assert.AreEqual(UserHelper.CorrectUser.Password, password.GetAttribute("value"),
                    "Zadané hodnoty nesouhlasí s obdrženými");
                var prihlasitButton = driver.FindElement(By.Name("button"));
                Assert.IsNotNull(prihlasitButton, "Nenašlo se tlačítko pro přihlášení");
                prihlasitButton.Click();
                //Pokud bude problém udělá se třída
                var errorMessage = driver.FindElement(By.ClassName("alert-danger"));
                Assert.IsTrue(errorMessage.Displayed, "Nenašla se zpráva o chybě");
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Tato třída se přihlašuje špatným heslem.
        ///     Taky se kontroluje chybová hláška.
        /// </summary>
        /// <param name="driver">Používá se pro ChromeDriver využívá funkce chromu</param>
        /// <param name="wait">Čekací parametr nastavený na 5 vteřin</param>
        /// <returns>Pokud je vše v pořádku vrátí hodnotu true</returns>
        private static bool BadLoginPassword(ChromeDriver driver, WebDriverWait wait)
        {
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Username")));
                var username = driver.FindElement(By.Id("Username"));
                Assert.IsNotNull(username, "Nenašel se element pro username");
                var password = driver.FindElement(By.Id("Password"));
                Assert.IsNotNull(password, "Nenašel se element pro password");
                username.SendKeys(UserHelper.CorrectUser.Email);
                Assert.AreEqual(UserHelper.CorrectUser.Email, username.GetAttribute("value"),
                    "Zadané hodnoty nesouhlasí s obdrženými");
                password.SendKeys(UserHelper.IncorrectUser.Password);
                Assert.AreEqual(UserHelper.IncorrectUser.Password, password.GetAttribute("value"),
                    "Zadané hodnoty nesouhlasí s obdrženými");
                var prihlasitButton = driver.FindElement(By.Name("button"));
                Assert.IsNotNull(prihlasitButton, "Nenašlo se tlačítko pro přihlášení");
                prihlasitButton.Click();
                //Pokud bude problém udělá se třída
                var errorMessage = driver.FindElement(By.ClassName("alert-danger"));
                Assert.IsTrue(errorMessage.Displayed, "Nenašla se zpráva o chybě");
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Tato třída zadává oba údaje špatně.
        ///     Kontroluje se zde opět chybová hláška.
        /// </summary>
        /// <param name="driver">Používá se pro ChromeDriver využívá funkce chromu</param>
        /// <param name="wait">Čekací parametr nastavený na 5 vteřin</param>
        /// <returns>Pokud vše projde vrátí hodnotu true</returns>
        private static bool BadLoginBoth(ChromeDriver driver, WebDriverWait wait)
        {
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Username")));
                var username = driver.FindElement(By.Id("Username"));
                Assert.IsNotNull(username, "Nenašel se element pro username");
                var password = driver.FindElement(By.Id("Password"));
                Assert.IsNotNull(password, "Nenašel se element pro password");
                username.SendKeys(UserHelper.IncorrectUser.Email);
                Assert.AreEqual(UserHelper.IncorrectUser.Email, username.GetAttribute("value"),
                    "Zadané hodnoty nesouhlasí s obdrženými");
                password.SendKeys(UserHelper.IncorrectUser.Password);
                Assert.AreEqual(UserHelper.IncorrectUser.Password, password.GetAttribute("value"),
                    "Zadané hodnoty nesouhlasí s obdrženými");
                var prihlasitButton = driver.FindElement(By.Name("button"));
                Assert.IsNotNull(prihlasitButton, "Nenašlo se tlačítko pro přihlášení");
                prihlasitButton.Click();
                //Pokud bude problém udělá se třída
                var errorMessage = driver.FindElement(By.ClassName("alert-danger"));
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