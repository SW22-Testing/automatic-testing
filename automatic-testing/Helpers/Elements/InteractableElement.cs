using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace automatic_testing.Helpers.Elements
{
    public static class InteractableElement
    {
        #region Click by Name

        /// <summary>
        ///     Finds element with corresponding 'name' and clicks on it.
        /// </summary>
        /// <param name="driver">Session, which will be searched</param>
        /// <param name="name">Element 'name'</param>
        public static void ClickByName(WindowsDriver<WindowsElement> driver, string name)
        {
            //var el = driver.FindElementByName(name);
            var el = driver.FindElementByName(name);

            //Probably add more ErrorHandlers
            try
            {
                el.Click();
            }
            catch (ElementClickInterceptedException)
            {
                Assert.Fail($"Failed to click on element'{name}'");
            }
            catch (NullReferenceException)
            {
                Assert.Fail($"Element'{name}' not found");
            }
            catch (ElementNotInteractableException)
            {
                Assert.Fail($"Element'{name}' not interactable");
            }
        }

        /// <summary>
        ///     Finds element with corresponding 'name' and clicks on it.
        /// </summary>
        /// <param name="window">Window (Parent Element), which will be searched</param>
        /// <param name="name">Element 'name'</param>
        public static void ClickByName(AppiumWebElement window, string name)
        {
            var el = window.FindElementByName(name);
            //Probably add more ErrorHandlers
            try
            {
                el.Click();
            }
            catch (ElementClickInterceptedException)
            {
                Assert.Fail($"Failed to click on element'{name}'");
            }
            catch (NullReferenceException)
            {
                Assert.Fail($"Element'{name}' not found");
            }
            catch (ElementNotInteractableException)
            {
                Assert.Fail($"Element'{name}' not interactable");
            }
        }

        #endregion

        #region Click by AccessibilityId

        /// <summary>
        ///     Finds element with corresponding 'accessibilityId' and clicks on it.
        /// </summary>
        /// <param name="driver">Session, which will be searched</param>
        /// <param name="accessibilityId">Element 'id'</param>
        public static void ClickByAccessibilityId(WindowsDriver<WindowsElement> driver, string accessibilityId)
        {
            var el = driver.FindElementByAccessibilityId(accessibilityId);
            //Probably add more ErrorHandlers
            try
            {
                el.Click();
            }
            catch (ElementClickInterceptedException)
            {
                Assert.Fail($"Failed to click on element'{accessibilityId}'");
            }
            catch (NullReferenceException)
            {
                Assert.Fail($"Element'{accessibilityId}' not found");
            }
            catch (ElementNotInteractableException)
            {
                Assert.Fail($"Element'{accessibilityId}' not interactable");
            }
        }

        /// <summary>
        ///     Finds element with corresponding 'accessibilityId' and clicks on it.
        /// </summary>
        /// <param name="window">Window (Parent Element), which will be searched</param>
        /// <param name="accessibilityId">Element 'id'</param>
        public static void ClickByAccessibilityId(AppiumWebElement window, string accessibilityId)
        {
            var el = window.FindElementByAccessibilityId(accessibilityId);
            //Probably add more ErrorHandlers
            try
            {
                el.Click();
            }
            catch (ElementClickInterceptedException)
            {
                Assert.Fail($"Failed to click on element '{accessibilityId}'");
            }
            catch (NullReferenceException)
            {
                Assert.Fail($"Element'{accessibilityId}' not found");
            }
            catch (ElementNotInteractableException)
            {
                Assert.Fail($"Element'{accessibilityId}' not interactable");
            }
        }

        #endregion

        #region Check by Name

        /// <summary>
        ///     Finds element with corresponding 'name' and clicks on it.
        /// </summary>
        /// <param name="driver">Session, which will be searched</param>
        /// <param name="name">Element 'name'</param>
        public static void CheckByName(WindowsDriver<WindowsElement> driver, string name)
        {
            //var el = driver.FindElementByName(name);
            var el = driver.FindElementByName(name);

            //Probably add more ErrorHandlers
            try
            {
                el.Click();
            }
            catch (NullReferenceException)
            {
                Assert.Fail($"Element'{name}' not found");
            }
            catch (ElementNotInteractableException)
            {
                Assert.Fail($"Element'{name}' not interactable");
            }
        }

        /// <summary>
        ///     Finds element with corresponding 'name' and clicks on it.
        /// </summary>
        /// <param name="window">Window (Parent Element), which will be searched</param>
        /// <param name="name">Element 'name'</param>
        public static void CheckByName(AppiumWebElement window, string name)
        {
            var el = window.FindElementByName(name);
            //Probably add more ErrorHandlers
            try
            {
                el.Click();
            }
            catch (NullReferenceException)
            {
                Assert.Fail($"Element'{name}' not found");
            }
            catch (ElementNotInteractableException)
            {
                Assert.Fail($"Element'{name}' not interactable");
            }
        }

        #endregion
        #region Write by Name

        /// <summary>
        ///     Finds element with corresponding 'name' and clicks on it.
        /// </summary>
        /// <param name="driver">Session, which will be searched</param>
        /// <param name="name">Element 'name'</param>
        /// <param name="text">Written text</param>
        public static void WriteByName(WindowsDriver<WindowsElement> driver, string name, string text)
        {
            var el = driver.FindElementByName(name);

            //Probably add more ErrorHandlers
            try
            {
                el.SendKeys(text);
            }
            catch (ElementClickInterceptedException)
            {
                Assert.Fail($"Failed to click on element '{name}'");
            }
            catch (NullReferenceException)
            {
                Assert.Fail($"Element'{name}' not found");
            }
            catch (ElementNotInteractableException)
            {
                Assert.Fail($"Element'{name}' not interactable");
            }
        }

        /// <summary>
        ///     Finds element with corresponding 'name' and clicks on it.
        /// </summary>
        /// <param name="window">Window (Parent Element), which will be searched</param>
        /// <param name="name">Element 'name'</param>
        /// <param name="text">Written text</param>
        public static void WriteByName(AppiumWebElement window, string name, string text)
        {
            var el = window.FindElementByName(name);
            //Probably add more ErrorHandlers
            try
            {
                el.SendKeys(text);
            }
            catch (ElementClickInterceptedException)
            {
                Assert.Fail($"Failed to click on element '{name}'");
            }
            catch (NullReferenceException)
            {
                Assert.Fail($"Element'{name}' not found");
            }
            catch (ElementNotInteractableException)
            {
                Assert.Fail($"Element'{name}' not interactable");
            }
        }
        #endregion

        #region Write By AccessibilityId

/// <summary>
        ///     Finds element with corresponding 'name' and clicks on it.
        /// </summary>
        /// <param name="driver">Session, which will be searched</param>
        /// <param name="accessibilityId">Element 'accessibilityId'</param>
        /// <param name="text">Written text</param>
        public static void WriteByAccessibilityId(WindowsDriver<WindowsElement> driver, string accessibilityId, string text)
        {
            var el = driver.FindElementByAccessibilityId(accessibilityId);

            //Probably add more ErrorHandlers
            try
            {
                el.SendKeys(text);
            }
            catch (ElementClickInterceptedException)
            {
                Assert.Fail($"Failed to click on element '{accessibilityId}'");
            }
            catch (NullReferenceException)
            {
                Assert.Fail($"Element'{accessibilityId}' not found");
            }
            catch (ElementNotInteractableException)
            {
                Assert.Fail($"Element'{accessibilityId}' not interactable");
            }
        }

        /// <summary>
        ///     Finds element with corresponding 'name' and clicks on it.
        /// </summary>
        /// <param name="window">Window (Parent Element), which will be searched</param>
        /// <param name="accessibilityId">Element 'accessibilityId'</param>
        /// <param name="text">Written text</param>
        public static void WriteByAccessibilityId(AppiumWebElement window, string accessibilityId, string text)
        {
            var el = window.FindElementByAccessibilityId(accessibilityId);
            //Probably add more ErrorHandlers
            try
            {
                el.SendKeys(text);
            }
            catch (ElementClickInterceptedException)
            {
                Assert.Fail($"Failed to click on element '{accessibilityId}'");
            }
            catch (NullReferenceException)
            {
                Assert.Fail($"Element'{accessibilityId}' not found");
            }
            catch (ElementNotInteractableException)
            {
                Assert.Fail($"Element'{accessibilityId}' not interactable");
            }
        }
        #endregion

        #region Get By Name
        /// <summary>
        ///     Finds element with corresponding 'name'
        /// </summary>
        /// <param name="driver">Session, which will be searched</param>
        /// <param name="name">Element 'name'</param>
        /// <returns>Element with the same 'name'</returns>
        public static AppiumWebElement GetElementByName(WindowsDriver<WindowsElement> driver, string name)
        {
            var el = driver.FindElementByName(name);
            try
            {
                return el;
            }
            catch (Exception error)
            {
                Assert.Fail($"Element'{name}' not found \n\n\n {error}");
                throw;
            }
        }

        /// <summary>
        ///     Finds element with corresponding 'name'
        /// </summary>
        /// <param name="window">Window (Parent Element), which will be searched</param>
        /// <param name="name">Element 'name'</param>
        /// <returns>Element with the same 'name'</returns>
        public static AppiumWebElement GetElementByName(AppiumWebElement window, string name)
        {
            var el = window.FindElementByName(name);
            try
            {
                return el;
            }
            catch (Exception error)
            {
                Assert.Fail($"Element'{name}' not found \n\n\n {error}");
                throw;
            }
        }

        #endregion

        #region Get By AccessibilityId

        /// <summary>
        ///     Finds element with corresponding 'accessibilityId'
        /// </summary>
        /// <param name="driver">Session, which will be searched</param>
        /// <param name="accessibilityId">Element 'accessibilityId'</param>
        /// <returns>Element with the same 'accessibilityId'</returns>
        public static AppiumWebElement GetElementByAccessibilityId(WindowsDriver<WindowsElement> driver,
            string accessibilityId)
        {
            var el = driver.FindElementByAccessibilityId(accessibilityId);
            try
            {
                return el;
            }
            catch (Exception error)
            {
                Assert.Fail($"Element'{accessibilityId}' not found \n\n\n {error}");
                throw;
            }
        }

        /// <summary>
        ///     Finds element with corresponding 'accessibilityId'
        /// </summary>
        /// <param name="window">Window (Parent Element), which will be searched</param>
        /// <param name="accessibilityId">Element 'accessibilityId'</param>
        /// <returns>Element with the same 'accessibilityId'</returns>
        public static AppiumWebElement GetElementByAccessibilityId(AppiumWebElement window, string accessibilityId)
        {
            var el = window.FindElementByAccessibilityId(accessibilityId);
            try
            {
                return el;
            }
            catch (Exception error)
            {
                Assert.Fail($"Element'{accessibilityId}' not found \n\n\n {error}");
                throw;
            }
        }

        #endregion
    }
}