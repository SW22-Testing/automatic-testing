using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace automatic_testing.Helpers.Elements
{
    //todo: would be good to add highlight mode | false; true / 0; 1
    public static class ButtonHelper
    {
        #region Click by Name

        /// <summary>
        ///     Finds button with corresponding 'name' and clicks on it.
        /// </summary>
        /// <param name="driver">Session, which will be searched</param>
        /// <param name="name">Button 'name'</param>
        public static void ClickByName(WindowsDriver<WindowsElement> driver, string name)
        {
            var el = driver.FindElementByName(name);
            //Probably add more ErrorHandlers
            try
            {
                el.Click();
            }
            catch (ElementClickInterceptedException clickError)
            {
                Assert.Fail($"Failed to click on button '{name}' \n\n\n {clickError}");
            }
            catch (NullReferenceException nullError)
            {
                Assert.Fail($"Button '{name}' not found \n\n\n {nullError}");
            }
        }

        /// <summary>
        ///     Finds button with corresponding 'name' and clicks on it.
        /// </summary>
        /// <param name="window">Window (Parent Element), which will be searched</param>
        /// <param name="name">Button 'name'</param>
        public static void ClickByName(AppiumWebElement window, string name)
        {
            var el = window.FindElementByName(name);
            //Probably add more ErrorHandlers
            try
            {
                el.Click();
            }
            catch (ElementClickInterceptedException clickError)
            {
                Assert.Fail($"Failed to click on button '{name}' \n\n\n {clickError}");
            }
            catch (NullReferenceException nullError)
            {
                Assert.Fail($"Button '{name}' not found \n\n\n {nullError}");
            }
        }

        #endregion

        #region Click by AccessibilityId

        /// <summary>
        ///     Finds button with corresponding 'accessibilityId' and clicks on it.
        /// </summary>
        /// <param name="driver">Session, which will be searched</param>
        /// <param name="accessibilityId">Button 'id'</param>
        public static void ClickByAccessibilityId(WindowsDriver<WindowsElement> driver, string accessibilityId)
        {
            var el = driver.FindElementByAccessibilityId(accessibilityId);
            //Probably add more ErrorHandlers
            try
            {
                el.Click();
            }
            catch (ElementClickInterceptedException clickError)
            {
                Assert.Fail($"Failed to click on button '{accessibilityId}'. \n\n\n {clickError}");
            }
            catch (NullReferenceException nullError)
            {
                Assert.Fail($"Button '{accessibilityId}' not found. \n\n\n {nullError}");
            }
        }

        /// <summary>
        ///     Finds button with corresponding 'accessibilityId' and clicks on it.
        /// </summary>
        /// <param name="window">Window (Parent Element), which will be searched</param>
        /// <param name="accessibilityId">Button 'id'</param>
        public static void ClickByAccessibilityId(AppiumWebElement window, string accessibilityId)
        {
            var el = window.FindElementByAccessibilityId(accessibilityId);
            //Probably add more ErrorHandlers
            try
            {
                el.Click();
            }
            catch (ElementClickInterceptedException clickError)
            {
                Assert.Fail($"Failed to click on button '{accessibilityId}'. \n\n\n {clickError}");
            }
            catch (NullReferenceException nullError)
            {
                Assert.Fail($"Button '{accessibilityId}' not found. \n\n\n {nullError}");
            }
        }

        #endregion

        #region Get By Name
        /// <summary>
        ///     Finds button with corresponding 'name'
        /// </summary>
        /// <param name="driver">Session, which will be searched</param>
        /// <param name="name">Button 'name'</param>
        /// <returns>Button with the same 'name'</returns>
        public static AppiumWebElement GetElementByName(WindowsDriver<WindowsElement> driver, string name)
        {
            var el = driver.FindElementByName(name);
            try
            {
                return el;
            }
            catch (Exception error)
            {
                Assert.Fail($"Button '{name}' not found \n\n\n {error}");
                throw;
            }
        }
        
        /// <summary>
        ///     Finds button with corresponding 'name'
        /// </summary>
        /// <param name="window">Window (Parent Element), which will be searched</param>
        /// <param name="name">Button 'name'</param>
        /// <returns>Button with the same 'name'</returns>
        public static AppiumWebElement GetElementByName(AppiumWebElement window, string name)
        {
            var el = window.FindElementByName(name);
            try
            {
                return el;
            }
            catch (Exception error)
            {
                Assert.Fail($"Button '{name}' not found \n\n\n {error}");
                throw;
            }
        }

        #endregion

        #region Get By AccessibilityId

        /// <summary>
        ///     Finds button with corresponding 'accessibilityId'
        /// </summary>
        /// <param name="driver">Session, which will be searched</param>
        /// <param name="accessibilityId">Button 'accessibilityId'</param>
        /// <returns>Button with the same 'accessibilityId'</returns>
        public static AppiumWebElement GetElementByAccessibilityId(WindowsDriver<AppiumWebElement> driver,
            string accessibilityId)
        {
            var el = driver.FindElementByAccessibilityId(accessibilityId);
            try
            {
                return el;
            }
            catch (Exception error)
            {
                Assert.Fail($"Button '{accessibilityId}' not found \n\n\n {error}");
                throw;
            }
        }

        /// <summary>
        ///     Finds button with corresponding 'accessibilityId'
        /// </summary>
        /// <param name="window">Window (Parent Element), which will be searched</param>
        /// <param name="accessibilityId">Button 'accessibilityId'</param>
        /// <returns>Button with the same 'accessibilityId'</returns>
        public static AppiumWebElement GetElementByAccessibilityId(AppiumWebElement window, string accessibilityId)
        {
            var el = window.FindElementByAccessibilityId(accessibilityId);
            try
            {
                return el;
            }
            catch (Exception error)
            {
                Assert.Fail($"Button '{accessibilityId}' not found \n\n\n {error}");
                throw;
            }
        }

        #endregion
    }
}