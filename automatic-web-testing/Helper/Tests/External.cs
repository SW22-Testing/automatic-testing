using automatic_web_testing.Helper.Predefined;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Linq;

public class External
{
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
}
