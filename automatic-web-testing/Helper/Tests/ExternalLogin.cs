using automatic_web_testing.Helper.Predefined;
using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

public class ExternalLogin
{
	public static ChromeDriver ExternalLogin(driver, wait)
	{
        try
        {
            wait.Until(e => e.FindElements(By.ClassName("MainModule.Header_action__1YiP5")).Where(e => e.GetAttribute("title") == UserHelper.CorrectUser.Email).FirstOrDefault());

            IWebElement profile = driver.FindElements(By.ClassName("MainModule.Header_action__1YiP5")).Where(e => e.GetAttribute("title") == UserHelper.CorrectUser.Email).First();
            profile.Click();

            IWebElement profiler = driver.FindElement(By.ClassName("anticon-setting"));
            profiler.Click();

            IWebElement extraLogin = driver.FindElement(By.LinkText("Moje externí přihlášení"));
            extraLogin.Click();

            return driver;
        }
        catch 
        {
            return null;
        }
    }
}
