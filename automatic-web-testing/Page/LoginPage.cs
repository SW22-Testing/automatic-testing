using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Threading;

namespace automatic_web_testing.Page
{
    public class LoginPage
    {
        [FindsBy(How = How.Id, Using = "Username")]
        private IWebElement UsernameInput { get; set; }
        [FindsBy(How = How.Id, Using = "Password")]
        private IWebElement PasswordInput { get; set; }
        [FindsBy(How = How.Name, Using = "button")]
        private IWebElement SubmitButton { get; set; }

        public LoginPage(ChromeDriver _driver)
        {
            _ = _driver.Manage().Timeouts().ImplicitWait;

            new WebDriverWait(_driver, new TimeSpan(5000)).Until(
            d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

            PageFactory.InitElements(_driver, this);
        }

        public void Login(string username, string password)
        {
            UsernameInput.SendKeys(username);
            PasswordInput.SendKeys(password);

            Assert.AreEqual(username, UsernameInput.GetAttribute("value"));
            Assert.AreEqual(password, PasswordInput.GetAttribute("value"));

            try
            {
                SubmitButton.Click();
            }
            catch(ElementNotInteractableException ) { TestContext.WriteLine($"Element {SubmitButton.GetAttribute("Name")} is not interactable"); }
            catch(NoSuchElementException) { TestContext.WriteLine($"Element {SubmitButton.GetAttribute("Name")} not found"); }
        }
    }
}
