using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace automatic_testing.Helpers.Elements
{
    public static class ParentElement
    {
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

        #region Wait for element by Name

        /// <summary>
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="name">Name of element</param>
        /// <param name="timeOut">Time out in <c>s</c></param>
        /// <param name="pollingInterval">Polling interval in <c>ms</c></param>
        /// <returns></returns>
        public static AppiumWebElement WaitForElementByName(WindowsDriver<WindowsElement> driver, string name,
            int timeOut, int pollingInterval)
        {
            if (driver == null) return null;

            var timer = new Stopwatch();
            var iterate = true;
            var timeOutSpan = TimeSpan.FromSeconds(timeOut);
            _ = TimeSpan.FromMilliseconds(pollingInterval);
            timer.Start();
            try
            {
                while (timer.Elapsed <= timeOutSpan && iterate)
                    try
                    {
                        _ = driver.FindElementByName(name);
                        iterate = false;
                    }
                    catch (WebDriverException)
                    {
                        Assert.Fail("Element not found in given time");
                    }

                timer.Stop();
                timer.Reset();

                //? Zřejmě není potřeba, ale radši nechávám, kdyby v budoucnu byly potíže
                Thread.Sleep(1000);
                return driver.FindElementByName(name);
            }
            catch
            {
                TestContext.WriteLine("Nepovedlo se najít element");
                return null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="name">Name of element</param>
        /// <param name="timeOut">Time out in <c>s</c></param>
        /// <param name="pollingInterval">Polling interval in <c>ms</c></param>
        /// <returns></returns>
        public static AppiumWebElement WaitForElementByName(WindowsElement driver, string name, int timeOut,
            int pollingInterval)
        {
            if (driver == null) return null;

            var timer = new Stopwatch();
            var iterate = true;
            var timeOutSpan = TimeSpan.FromSeconds(timeOut);
            _ = TimeSpan.FromMilliseconds(pollingInterval);
            timer.Start();
            try
            {
                while (timer.Elapsed <= timeOutSpan && iterate)
                    try
                    {
                        _ = driver.FindElementByName(name);
                        iterate = false;
                    }
                    catch (WebDriverException)
                    {
                        //LogSearchError(ex, automationId, controlName);
                    }

                timer.Stop();
                timer.Reset();
                //? Zřejmě není potřeba, ale radši nechávám, kdyby v budoucnu byly potíže
                Thread.Sleep(1000);
                return driver.FindElementByName(name);
            }
            catch
            {
                TestContext.WriteLine("Nepovedlo se najít element");
                return null;
            }
        }

        #endregion

        #region Wait for element by AccessibilityId

        /// <summary>
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="id"></param>
        /// <param name="timeOut"></param>
        /// <param name="pollingInterval"></param>
        /// <returns></returns>
        public static AppiumWebElement WaitForElementByAccessibilityId(WindowsDriver<WindowsElement> driver, string id,
            int timeOut, int pollingInterval)
        {
            if (driver == null) return null;

            var timer = new Stopwatch();
            var iterate = true;
            var timeOutSpan = TimeSpan.FromSeconds(timeOut);
            _ = TimeSpan.FromMilliseconds(pollingInterval);
            timer.Start();
            try
            {
                while (timer.Elapsed <= timeOutSpan && iterate)
                    try
                    {
                        _ = driver.FindElementByAccessibilityId(id);
                        iterate = false;
                    }
                    catch (WebDriverException)
                    {
                        //LogSearchError(ex, automationId, controlName);
                    }

                timer.Stop();
                timer.Reset();
                //? Zřejmě není potřeba, ale radši nechávám, kdyby v budoucnu byly potíže
                //Thread.Sleep(1000);
                return driver.FindElementByAccessibilityId(id);
            }
            catch
            {
                TestContext.WriteLine("Nepovedlo se najít element");
                return null;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="id"></param>
        /// <param name="timeOut"></param>
        /// <param name="pollingInterval"></param>
        /// <returns></returns>
        public static AppiumWebElement WaitForElementByAccessibilityId(WindowsElement driver, string id, int timeOut,
            int pollingInterval)
        {
            if (driver == null) return null;

            var timer = new Stopwatch();
            var iterate = true;
            var timeOutSpan = TimeSpan.FromSeconds(timeOut);
            _ = TimeSpan.FromMilliseconds(pollingInterval);
            timer.Start();
            try
            {
                while (timer.Elapsed <= timeOutSpan && iterate)
                    try
                    {
                        _ = driver.FindElementByAccessibilityId(id);
                        iterate = false;
                    }
                    catch (WebDriverException)
                    {
                        //LogSearchError(ex, automationId, controlName);
                    }

                timer.Stop();
                timer.Reset();
                //? Zřejmě není potřeba, ale radši nechávám, kdyby v budoucnu byly potíže
                //Thread.Sleep(1000);
                return driver.FindElementByAccessibilityId(id);
            }
            catch
            {
                TestContext.WriteLine("Nepovedlo se najít element");
                return null;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="id"></param>
        /// <param name="timeOut"></param>
        /// <param name="pollingInterval"></param>
        /// <returns></returns>
        public static ICollection<AppiumWebElement> WaitForElementsByAccessibilityId(WindowsElement driver, string id,
            int timeOut, int pollingInterval)
        {
            if (driver == null) return null;

            var timer = new Stopwatch();
            var iterate = true;
            var timeOutSpan = TimeSpan.FromSeconds(timeOut);
            _ = TimeSpan.FromMilliseconds(pollingInterval);
            timer.Start();
            try
            {
                while (timer.Elapsed <= timeOutSpan && iterate)
                    try
                    {
                        _ = driver.FindElementsByAccessibilityId(id);
                        iterate = false;
                    }
                    catch (WebDriverException)
                    {
                        //LogSearchError(ex, automationId, controlName);
                    }

                timer.Stop();
                timer.Reset();
                //? Zřejmě není potřeba, ale radši nechávám, kdyby v budoucnu byly potíže
                //Thread.Sleep(1000);
                return driver.FindElementsByAccessibilityId(id) as ICollection<AppiumWebElement>;
            }
            catch
            {
                TestContext.WriteLine("Nepovedlo se najít element");
                return null;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="id"></param>
        /// <param name="timeOut"></param>
        /// <param name="pollingInterval"></param>
        /// <returns></returns>
        public static ICollection<AppiumWebElement> WaitForElementsByAccessibilityId(
            WindowsDriver<AppiumWebElement> driver, string id, int timeOut, int pollingInterval)
        {
            if (driver == null) return null;

            var timer = new Stopwatch();
            var iterate = true;
            var timeOutSpan = TimeSpan.FromSeconds(timeOut);
            _ = TimeSpan.FromMilliseconds(pollingInterval);
            timer.Start();
            try
            {
                while (timer.Elapsed <= timeOutSpan && iterate)
                    try
                    {
                        _ = driver.FindElementsByAccessibilityId(id);
                        iterate = false;
                    }
                    catch (WebDriverException)
                    {
                        //LogSearchError(ex, automationId, controlName);
                    }

                timer.Stop();
                timer.Reset();
                //? Zřejmě není potřeba, ale radši nechávám, kdyby v budoucnu byly potíže
                //Thread.Sleep(1000);
                return driver.FindElementsByAccessibilityId(id) as ICollection<AppiumWebElement>;
            }
            catch
            {
                TestContext.WriteLine("Nepovedlo se najít element");
                return null;
            }
        }

        #endregion
    }
}