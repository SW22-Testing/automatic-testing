using automatic_web_testing.Helper.Predefined;
using automatic_web_testing.Helper.Setup;
using automatic_web_testing.Helper.Tests;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
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
            bool status = LoginHelper.BadLoginUsername(driver,wait);
            Assert.IsTrue(status);
        }
        [TestCase(TestName = "Přihlášení chybné heslo", Description = "Test kontroluje přihlášení s chybným heslem"), Order(2)]
        public void PrihlaseniChybneHeslo()
        {
            bool status = LoginHelper.BadLoginPassword(driver, wait);
            Assert.IsTrue(status);
        }
        [TestCase(TestName = "Přihlášení chybné heslo a email", Description = "Test kontroluje přihlášení s chybným heslem a mailem"), Order(3)]
        public void PrihlaseniObeChybne()
        {
            bool status = LoginHelper.BadLoginBoth(driver, wait);
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

            driver = External.ExternalLogin(driver, wait);
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

            driver = External.ExternalLogin(driver, wait);
            Assert.NotNull(driver);

            IWebElement deleteMicrosoft = driver.FindElements(By.ClassName("btn-primary")).Where(e => e.GetAttribute("title") == "Remove this Microsoft login from your account").First();
            deleteMicrosoft.Click();
        }
        [TestCase(TestName = "Externí přihlášení přes Google", Description = "Test kontroluje externí přihlášení přes google přidá ho, přihlásí se přes něj a potom ho i odebere"), Order(6)]
        public void PrihlaseniGoogle()
        {
            driver = LoginHelper.Login(driver, wait);
            Assert.NotNull(driver);

            driver = External.ExternalLogin(driver, wait);
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

            driver = External.ExternalLogin(driver, wait);
            Assert.NotNull(driver);

            IWebElement deleteGoogle = driver.FindElements(By.ClassName("btn-primary")).Where(e => e.GetAttribute("title") == "Remove this Google login from your account").FirstOrDefault();
            deleteGoogle.Click();
        }

        [TearDown]
        public void TearDown()
        {
            ChromeDriverSetup.Dispose(driver);
        }
    }
}
