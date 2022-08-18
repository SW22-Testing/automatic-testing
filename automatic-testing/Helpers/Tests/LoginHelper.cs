using NUnit.Framework;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace automatic_testing.Helpers.Tests
{
    public class LoginHelper
    {
        #region Přihlášení s Asserts
        /// <summary>
        /// Kontrola přihlášení s Asserty
        /// </summary>
        /// <param name="session">Session, ve které se bude hledat přihlašovací okno pro AspeEsticon</param>
        public static bool TryLogin(WindowsDriver<WindowsElement> session, string username, string password)
        {
            try
            {
                // Kontrola jestli session není null
                Assert.IsNotNull(session);

                // Přepne na login s heslem
                var userCheckBox = SearchHelper.FindElementByName("Přihlásit pomocí jména a hesla", session);
                Assert.IsNotNull(userCheckBox);


                userCheckBox.Click();
                Assert.True(userCheckBox.GetAttribute("SelectionItem.IsSelected").Equals("True"));

                var userNameBox = SearchHelper.FindElementByAccessibilityId("PART_Editor", session);
                Assert.IsNotNull(userNameBox);

                userNameBox.SendKeys(username);
                Assert.AreEqual(username, userNameBox.Text);

                var passwordBox = SearchHelper.FindElementByAccessibilityId("passwordBox", session);
                Assert.IsNotNull(passwordBox);

                passwordBox.SendKeys(password);
                Assert.AreEqual(password.Length, passwordBox.Text.Length);

                var login = SearchHelper.FindElementByAccessibilityId("btnOk", session);
                Assert.IsNotNull(login);
                login.Click();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool TryLogin(WindowsDriver<WindowsElement> session)
        {
            try
            {
                var userCheckBox = SearchHelper.FindElementByName("Přihlásit pomocí Windows ověření", session);
                Assert.IsNotNull(userCheckBox);

                userCheckBox.Click();
                Assert.True(userCheckBox.GetAttribute("SelectionItem.IsSelected").Equals("True"));

                var login = SearchHelper.FindElementByAccessibilityId("btnOk", session);
                Assert.IsNotNull(login);
                login.Click();

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
        /// <param name="session">Session, ve které se bude hledat přihlašovací okno pro AspeEsticon</param>
        public static WindowsDriver<WindowsElement> Login(WindowsDriver<WindowsElement> session, string username, string password)
        {
            try
            {
                var userCheckBox = session.FindElementByName("Přihlásit pomocí jména a hesla");
                userCheckBox.Click();

                var userNameBox = session.FindElementByAccessibilityId("PART_Editor");
                userNameBox.SendKeys(username);

                var passwordBox = session.FindElementByAccessibilityId("passwordBox");
                passwordBox.SendKeys(password);

                var login = session.FindElementByAccessibilityId("btnOk");
                login.Click();

                return session;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static WindowsDriver<WindowsElement> Login(WindowsDriver<WindowsElement> session)
        {
            try
            {
                var userCheckBox = SearchHelper.FindElementByName("Přihlásit pomocí Windows ověření", session);
                Assert.IsNotNull(userCheckBox);

                userCheckBox.Click();
                Assert.True(userCheckBox.GetAttribute("SelectionItem.IsSelected").Equals("True"));

                var login = SearchHelper.FindElementByAccessibilityId("btnOk", session);
                Assert.IsNotNull(login);
                login.Click();

                return session;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}
