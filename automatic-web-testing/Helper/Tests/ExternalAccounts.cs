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
            var deleteMicrosoftsearch = driver.FindElements(By.ClassName("btn-primary")).FirstOrDefault(e => e.GetAttribute("title") == "Remove this Microsoft login from your account");
            if (deleteMicrosoftsearch is { Displayed: true })
                deleteMicrosoftsearch.Click();
        }
        public static void GoogleEdit(ChromeDriver driver)
        {
            var deleteGooglesearch = driver.FindElements(By.ClassName("btn-primary")).FirstOrDefault(e => e.GetAttribute("title") == "Remove this Google login from your account");
            if (deleteGooglesearch is { Displayed: true})
                deleteGooglesearch.Click();
        }
    }
}