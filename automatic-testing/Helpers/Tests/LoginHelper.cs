using NUnit.Framework;
using OpenQA.Selenium.Appium.Windows;
using System;
using automatic_testing.Helpers.Elements;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Appium;

namespace automatic_testing.Helpers.Tests
{
    public static class LoginHelper
    {
        #region Přihlášení s Asserts

        /// <summary>
        /// Kontrola přihlášení s Asserty
        /// </summary>
        /// <param name="driver">Session, ve které se bude hledat přihlašovací okno pro AspeEsticon</param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static bool TryLogin(WindowsDriver<WindowsElement> driver, string username, string password)
        {
            try
            {
                // Kontrola jestli driver není null
                Assert.IsNotNull(driver);

                // Přepne na login s heslem
                InteractableElement.CheckByName(driver, "Přihlásit pomocí jména a hesla");
                //var userCheckBox = SearchHelper.GetClickableElementByName(driver, "Přihlásit pomocí jména a hesla", "Nepovedlo se kliknout na checkbox pro přihlášení heslem");
                //userCheckBox.Click();
                //Assert.True(userCheckBox.GetAttribute("SelectionItem.IsSelected").Equals("True"));

                InteractableElement.WriteByAccessibilityId(driver, "PART_Editor", username);

                //var userNameBox = SearchHelper.GetClickableElementByAccessibilityId(driver, "PART_Editor", "Nepovedlo se najít input pro UserName");
                //userNameBox.SendKeys(username);
                //Assert.AreEqual(username, userNameBox.Text);

                InteractableElement.WriteByAccessibilityId(driver, "passwordBox", password);
                //var passwordBox = SearchHelper.GetClickableElementByAccessibilityId(driver, "passwordBox", "Nepovedlo se najít input pro heslo");
                //passwordBox.SendKeys(password);
                //Assert.AreEqual(password.Length, passwordBox.Text.Length);

                InteractableElement.ClickByAccessibilityId(driver, "btnOk");
                //var login = SearchHelper.GetClickableElementByAccessibilityId(driver, "btnOk", "Nepovedlo se najít tlačítko pro login");
                //login.Click();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool TryLogin(WindowsDriver<WindowsElement> driver)
        {
            try
            {
                InteractableElement.CheckByName(driver, "Přihlásit pomocí Windows ověření");

                //Assert.True(userCheckBox.GetAttribute("SelectionItem.IsSelected").Equals("True"));
                InteractableElement.ClickByAccessibilityId(driver, "btnOk");

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
        #region Přihlášení

        /// <summary>
        /// Kontrola přihlášení bez Assertu
        /// </summary>
        /// <param name="driver">Session, ve které se bude hledat přihlašovací okno pro AspeEsticon</param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void Login(WindowsDriver<WindowsElement> driver, string username, string password)
        {
            try
            {
                InteractableElement.CheckByName(driver, "Přihlásit pomocí jména a hesla");

                InteractableElement.WriteByAccessibilityId(driver, "PART_Editor", username);

                InteractableElement.WriteByAccessibilityId(driver, "passwordBox", password);

                InteractableElement.ClickByAccessibilityId(driver, "btnOk");
            }
            catch (Exception)
            {
                Assert.Fail("Login failed");
            }
        }
        public static WindowsDriver<WindowsElement> Login(WindowsDriver<WindowsElement> driver)
        {
            try
            {
                InteractableElement.CheckByName(driver, "Přihlásit pomocí Windows ověření");

                InteractableElement.ClickByAccessibilityId(driver, "btnOk");

                return driver;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}
