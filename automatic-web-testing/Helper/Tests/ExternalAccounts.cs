using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace automatic_web_testing.Helper.Tests
{
    public static class ExternalAccounts
    {
        public static void MicrosoftEdit(ChromeDriver driver)
        {
            var deleteMicrosoftsearch = driver.FindElements(By.ClassName("btn-primary")).First(e => e.GetAttribute("title") == "Remove this Microsoft login from your account");
            if (deleteMicrosoftsearch.Displayed)
                deleteMicrosoftsearch.Click();
        }
        

        public static void GoogleEdit(ChromeDriver driver)
        {
            var deleteGooglesearch = driver.FindElements(By.ClassName("btn-primary")).First(e => e.GetAttribute("title") == "Remove this Google login from your account");
            if (deleteGooglesearch.Displayed)
                deleteGooglesearch.Click();
        }
    }
}