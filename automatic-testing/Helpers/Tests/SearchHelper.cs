using NUnit.Framework;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace automatic_testing.Helpers.Tests
{
    public static class SearchHelper
    {
        //TODO: Doplnit <returns> pro Summary

        #region Hledání pomocí Name
        /// <summary>
        /// Vyhledá element podle parametru Name a zkontroluje stav elementu
        /// </summary>
        /// <param name="driver">Objekt, ve kterém se bude vyhledávat</param>
        /// <param name="name">Parametr Name hledaného elementu</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav elementu</param>
        /// <returns></returns>
        public static AppiumWebElement GetClickableElementByName(WindowsDriver<WindowsElement> driver, string name, string errorDesc, bool enabled = true)
        {
            var el = driver.FindElementByName(name);
            
            Assert.IsNotNull(el, errorDesc);
            Assert.True(el.Displayed, errorDesc);
            if (enabled)
                Assert.True(el.Enabled, errorDesc);
            else
                Assert.False(el.Enabled, errorDesc);

            return el;
        }
        /// <summary>
        /// Vyhledá element podle parametru Name a zkontroluje stav elementu
        /// </summary>
        /// <param name="driver">Element, ve kterém se bude vyhledávat</param>
        /// <param name="name">Parametr Name hledaného elementu</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav elementu</param>
        /// <returns></returns>
        public static AppiumWebElement GetClickableElementByName(AppiumWebElement driver, string name, string errorDesc, bool enabled = true)
        {
            var el = driver.FindElementByName(name);

            Assert.IsNotNull(el, errorDesc);
            Assert.True(el.Displayed, errorDesc);
            if (enabled)
                Assert.True(el.Enabled, errorDesc);
            else
                Assert.False(el.Enabled, errorDesc);
            return el;
        }
        /// <summary>
        /// Vyhledá elementy podle parametru Name a zkontroluje stav tlačítek
        /// </summary>
        /// <param name="driver">Objekt, ve kterém se bude vyhledávat</param>
        /// <param name="name">Parametr Name hledaného elementu</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav elementu</param>
        /// <returns></returns>
        public static ICollection<AppiumWebElement> GetClickableElementsByName(WindowsDriver<WindowsElement> driver, string name, string errorDesc, bool enabled = true)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            var els = driver.FindElementsByName(name) as ICollection<AppiumWebElement>;
            foreach (var el in els!)
            {
                Assert.IsNotNull(el, errorDesc);
                Assert.True(el.Displayed, errorDesc);
                if (enabled)
                    Assert.True(el.Enabled, errorDesc);
                else
                    Assert.False(el.Enabled, errorDesc);
            }
            return els;
        }
        /// <summary>
        /// Vyhledá elementy podle parametru Name a zkontroluje stav tlačítek
        /// </summary>
        /// <param name="driver">Objekt, ve kterém se bude vyhledávat</param>
        /// <param name="name">Parametr Name hledaných elementů</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav elementu</param>
        /// <returns></returns>
        public static ICollection<AppiumWebElement> GetClickableElementsByName(AppiumWebElement driver, string name, string errorDesc, bool enabled = true)
        {
            ICollection<AppiumWebElement> els = driver.FindElementsByName(name);
            foreach (var el in els)
            {
                Assert.IsNotNull(el, errorDesc);
                Assert.True(el.Displayed, errorDesc);
                if (enabled)
                    Assert.True(el.Enabled, errorDesc);
                else
                    Assert.False(el.Enabled, errorDesc);
            }
            return els;
        }

        /// <summary>
        /// Vyhledá element podle parametru Name a zkontroluje stav elementu
        /// </summary>
        /// <param name="driver">Objekt, ve kterém se bude vyhledávat</param>
        /// <param name="name">Parametr Name hledaného elementu</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav elementu</param>
        /// <returns></returns>
        public static AppiumWebElement GetParentElementByName(WindowsDriver<WindowsElement> driver, string name, string errorDesc, bool enabled = true)
        {
            var el = driver.FindElementByName(name);

            Assert.IsNotNull(el, errorDesc);
            Assert.True(el.Displayed, errorDesc);
            if (enabled)
                Assert.True(el.Enabled, errorDesc);
            else
                Assert.False(el.Enabled, errorDesc);

            return el;
        }
        /// <summary>
        /// Vyhledá element podle parametru Name a zkontroluje stav elementu
        /// </summary>
        /// <param name="driver">Element, ve kterém se bude vyhledávat</param>
        /// <param name="name">Parametr Name hledaného elementu</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav elementu</param>
        /// <returns></returns>
        public static AppiumWebElement GetParentElementByName(AppiumWebElement driver, string name, string errorDesc, bool enabled = true)
        {
            var el = driver.FindElementByName(name);

            Assert.IsNotNull(el, errorDesc);
            Assert.True(el.Displayed, errorDesc);
            if (enabled)
                Assert.True(el.Enabled, errorDesc);
            else
                Assert.False(el.Enabled, errorDesc);
            return el;
        }
        /// <summary>
        /// Vyhledá elementy podle parametru Name a zkontroluje stav tlačítek
        /// </summary>
        /// <param name="driver">Objekt, ve kterém se bude vyhledávat</param>
        /// <param name="name">Parametr Name hledaného elementu</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav elementu</param>
        /// <returns></returns>
        public static ICollection<AppiumWebElement> GetParentElementsByName(WindowsDriver<WindowsElement> driver, string name, string errorDesc, bool enabled = true)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            var els = driver.FindElementsByName(name) as ICollection<AppiumWebElement>;
            foreach (var el in els!)
            {
                Assert.IsNotNull(el, errorDesc);
                Assert.True(el.Displayed, errorDesc);
                if (enabled)
                    Assert.True(el.Enabled, errorDesc);
                else
                    Assert.False(el.Enabled, errorDesc);
            }
            return els;
        }
        /// <summary>
        /// Vyhledá elementy podle parametru Name a zkontroluje stav tlačítek
        /// </summary>
        /// <param name="driver">Objekt, ve kterém se bude vyhledávat</param>
        /// <param name="name">Parametr Name hledaných elementů</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav elementu</param>
        /// <returns></returns>
        public static ICollection<AppiumWebElement> GetParentElementsByName(AppiumWebElement driver, string name, string errorDesc, bool enabled = true)
        {
            ICollection<AppiumWebElement> els = driver.FindElementsByName(name);
            foreach (var el in els)
            {
                Assert.IsNotNull(el, errorDesc);
                Assert.True(el.Displayed, errorDesc);
                if (enabled)
                    Assert.True(el.Enabled, errorDesc);
                else
                    Assert.False(el.Enabled, errorDesc);
            }
            return els;
        }
        #endregion

        #region Hledání pomocí AutomationId
        /// <summary>
        /// Vyhledá element podle parametru AccessibiliyId a zkontroluje stav element
        /// </summary>
        /// <param name="driver">Objekt, ve kterém se bude vyhledávat</param>
        /// <param name="accessibilityId">Parametr Accessibility Id hledaného elementu</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav elementu</param>
        /// <returns></returns>
        public static AppiumWebElement GetClickableElementByAccessibilityId(WindowsDriver<WindowsElement> driver, string accessibilityId, string errorDesc, bool enabled = true)
        {
            var el = driver.FindElementByAccessibilityId(accessibilityId);

            Assert.IsNotNull(el, errorDesc);
            Assert.True(el.Displayed, errorDesc);
            if (enabled)
                Assert.True(el.Enabled, errorDesc);
            else
                Assert.False(el.Enabled, errorDesc);

            return el;
        }
        /// <summary>
        /// Vyhledá element podle parametru AccessibiliyId a zkontroluje stav element
        /// </summary>
        /// <param name="driver">Objekt, ve kterém se bude vyhledávat</param>
        /// <param name="accessibilityId">Parametr Accessibility Id hledaného elementu</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav elementu</param>
        /// <returns></returns>
        public static AppiumWebElement GetClickableElementByAccessibilityId(AppiumWebElement driver, string accessibilityId, string errorDesc, bool enabled = true)
        {
            var el = driver.FindElementByAccessibilityId(accessibilityId);

            Assert.IsNotNull(el, errorDesc);
            Assert.True(el.Displayed, errorDesc);
            if (enabled)
                Assert.True(el.Enabled, errorDesc);
            else
                Assert.False(el.Enabled, errorDesc);
            return el;
        }
        /// <summary>
        /// Vyhledá element podle parametru AccessibiliyId a zkontroluje stav element
        /// </summary>
        /// <param name="driver">Objekt, ve kterém se bude vyhledávat</param>
        /// <param name="accessibilityId">Parametr Accessibility Id hledaných elementů</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav elementu</param>
        /// <returns></returns>
        public static ICollection<AppiumWebElement> GetClickableElementsByAccessibilityId(AppiumWebElement driver, string accessibilityId, string errorDesc, bool enabled = true)
        {
            var els = (ICollection<AppiumWebElement>)driver.FindElementsByAccessibilityId(accessibilityId);
            foreach (var el in els)
            {
                Assert.IsNotNull(el, errorDesc);
                Assert.True(el.Displayed, errorDesc);
                if (enabled)
                    Assert.True(el.Enabled, errorDesc);
                else
                    Assert.False(el.Enabled, errorDesc);
            }
            return els;
        }
        /// <summary>
        /// Vyhledá element podle parametru AccessibiliyId a zkontroluje stav element
        /// </summary>
        /// <param name="driver">Objekt, ve kterém se bude vyhledávat</param>
        /// <param name="accessibilityId">Parametr Accessibility Id hledaných elementů</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav elementů</param>
        /// <returns></returns>
        public static ICollection<AppiumWebElement> GetClickableElementsByAccessibilityId(WindowsDriver<WindowsElement> driver, string accessibilityId, string errorDesc, bool enabled = true)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            var els = driver.FindElementsByAccessibilityId(accessibilityId) as ICollection<AppiumWebElement>;
            foreach (var el in els!)
            {
                Assert.IsNotNull(el, errorDesc);
                Assert.True(el.Displayed, errorDesc);
                if (enabled)
                    Assert.True(el.Enabled, errorDesc);
                else
                    Assert.False(el.Enabled, errorDesc);
            }
            return els;
        }



        /// <summary>
        /// Vratí řadu element, které mají stejný AutomationId jako parametr: <c>automationId</c>
        /// </summary>
        /// <param name="automationId">AutomationId elementu</param>
        /// <param name="session">Session, ve které se bude hledat</param>
        /// <returns></returns>
        public static AppiumWebElement FindElementByAccessibilityId(string automationId, AppiumWebElement session)
        {
            if (session == null)
                return null;
            try
            {
                return session.FindElementByAccessibilityId(automationId);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Vratí řadu element, které mají stejný AutomationId jako parametr: <c>automationId</c>
        /// </summary>
        /// <param name="automationId">AutomationId elementu</param>
        /// <param name="session">Session, ve které se bude hledat</param>
        /// <returns></returns>
        public static IEnumerable<AppiumWebElement> FindElementsByAccessibilityId(string automationId, WindowsElement session)
        {
            if (session == null)
                return null;
            try
            {
                return session.FindElementsByAccessibilityId(automationId);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Vratí řadu element, které mají stejný AutomationId jako parametr: <c>automationId</c>
        /// </summary>
        /// <param name="automationId">AutomationId elementu</param>
        /// <param name="session">Session, ve které se bude hledat</param>
        /// <returns></returns>
        public static IEnumerable<AppiumWebElement> FindElementsByAccessibilityId(string automationId, WindowsDriver<WindowsElement> session)
        {
            if (session == null)
                return null;
            try
            {
                return session.FindElementsById(automationId);
            }
            catch
            {
                return null;
            }
        }
        #endregion
        #region Hledání pomocí ClassName
        /// <summary>
        /// Vyhledá element podle parametru Name a zkontroluje stav elementu
        /// </summary>
        /// <param name="driver">Objekt, ve kterém se bude vyhledávat</param>
        /// <param name="name">Parametr Name hledaného elementu</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav elementu</param>
        /// <returns></returns>
        public static AppiumWebElement GetClickableElementByClassName(WindowsDriver<WindowsElement> driver, string name, string errorDesc, bool enabled = true)
        {
            var el = driver.FindElementByClassName(name);

            Assert.IsNotNull(el, errorDesc);
            Assert.True(el.Displayed, errorDesc);
            if (enabled)
                Assert.True(el.Enabled, errorDesc);
            else
                Assert.False(el.Enabled, errorDesc);

            return el;
        }
        /// <summary>
        /// Vyhledá element podle parametru Name a zkontroluje stav elementu
        /// </summary>
        /// <param name="driver">Element, ve kterém se bude vyhledávat</param>
        /// <param name="name">Parametr Name hledaného elementu</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav elementu</param>
        /// <returns></returns>
        public static AppiumWebElement GetClickableElementByClassName(AppiumWebElement driver, string name, string errorDesc, bool enabled = true)
        {
            var el = driver.FindElementByClassName(name);

            Assert.IsNotNull(el, errorDesc);
            Assert.True(el.Displayed, errorDesc);
            if (enabled)
                Assert.True(el.Enabled, errorDesc);
            else
                Assert.False(el.Enabled, errorDesc);
            return el;
        }
        /// <summary>
        /// Vyhledá elementy podle parametru Name a zkontroluje stav tlačítek
        /// </summary>
        /// <param name="driver">Objekt, ve kterém se bude vyhledávat</param>
        /// <param name="name">Parametr Name hledaného elementu</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav elementu</param>
        /// <returns></returns>
        public static ICollection<AppiumWebElement> GetClickableElementsByClassName(WindowsDriver<WindowsElement> driver, string name, string errorDesc, bool enabled = true)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            var els = driver.FindElementsByClassName(name) as ICollection<AppiumWebElement>;
            foreach (var el in els!)
            {
                Assert.IsNotNull(el, errorDesc);
                Assert.True(el.Displayed, errorDesc);
                if (enabled)
                    Assert.True(el.Enabled, errorDesc);
                else
                    Assert.False(el.Enabled, errorDesc);
            }
            return els;
        }
        /// <summary>
        /// Vyhledá elementy podle parametru Name a zkontroluje stav tlačítek
        /// </summary>
        /// <param name="driver">Objekt, ve kterém se bude vyhledávat</param>
        /// <param name="name">Parametr Name hledaných elementů</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav elementu</param>
        /// <returns></returns>
        public static ICollection<AppiumWebElement> GetClickableElementsByClassName(AppiumWebElement driver, string name, string errorDesc, bool enabled = true)
        {
            ICollection<AppiumWebElement> els = driver.FindElementsByClassName(name);
            foreach (var el in els)
            {
                Assert.IsNotNull(el, errorDesc);
                Assert.True(el.Displayed, errorDesc);
                if (enabled)
                    Assert.True(el.Enabled, errorDesc);
                else
                    Assert.False(el.Enabled, errorDesc);
            }
            return els;
        }

        /// <summary>
        /// Vyhledá element podle parametru Name a zkontroluje stav elementu
        /// </summary>
        /// <param name="driver">Objekt, ve kterém se bude vyhledávat</param>
        /// <param name="name">Parametr Name hledaného elementu</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav elementu</param>
        /// <returns></returns>
        public static AppiumWebElement GetParentElementByClassName(WindowsDriver<WindowsElement> driver, string name, string errorDesc, bool enabled = true)
        {
            var el = driver.FindElementByClassName(name);

            Assert.IsNotNull(el, errorDesc);
            Assert.True(el.Displayed, errorDesc);
            if (enabled)
                Assert.True(el.Enabled, errorDesc);
            else
                Assert.False(el.Enabled, errorDesc);

            return el;
        }
        /// <summary>
        /// Vyhledá element podle parametru Name a zkontroluje stav elementu
        /// </summary>
        /// <param name="driver">Element, ve kterém se bude vyhledávat</param>
        /// <param name="name">Parametr Name hledaného elementu</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav elementu</param>
        /// <returns></returns>
        public static AppiumWebElement GetParentElementByClassName(AppiumWebElement driver, string name, string errorDesc, bool enabled = true)
        {
            var el = driver.FindElementByClassName(name);

            Assert.IsNotNull(el, errorDesc);
            Assert.True(el.Displayed, errorDesc);
            if (enabled)
                Assert.True(el.Enabled, errorDesc);
            else
                Assert.False(el.Enabled, errorDesc);
            return el;
        }
        /// <summary>
        /// Vyhledá elementy podle parametru Name a zkontroluje stav tlačítek
        /// </summary>
        /// <param name="driver">Objekt, ve kterém se bude vyhledávat</param>
        /// <param name="name">Parametr Name hledaného elementu</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav elementu</param>
        /// <returns></returns>
        public static ICollection<AppiumWebElement> GetParentsElementsByClassName(WindowsDriver<WindowsElement> driver, string name, string errorDesc, bool enabled = true)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            var els = driver.FindElementsByClassName(name) as ICollection<AppiumWebElement>;
            foreach (var el in els!)
            {
                Assert.IsNotNull(el, errorDesc);
                Assert.True(el.Displayed, errorDesc);
                if (enabled)
                    Assert.True(el.Enabled, errorDesc);
                else
                    Assert.False(el.Enabled, errorDesc);
            }
            return els;
        }
        /// <summary>
        /// Vyhledá elementy podle parametru Name a zkontroluje stav tlačítek
        /// </summary>
        /// <param name="driver">Objekt, ve kterém se bude vyhledávat</param>
        /// <param name="name">Parametr Name hledaných elementů</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav elementu</param>
        /// <returns></returns>
        public static ICollection<AppiumWebElement> GetParentElementsByClassName(AppiumWebElement driver, string name, string errorDesc, bool enabled = true)
        {
            ICollection<AppiumWebElement> els = driver.FindElementsByClassName(name);
            foreach (var el in els)
            {
                Assert.IsNotNull(el, errorDesc);
                Assert.True(el.Displayed, errorDesc);
                if (enabled)
                    Assert.True(el.Enabled, errorDesc);
                else
                    Assert.False(el.Enabled, errorDesc);
            }
            return els;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Název elementu</param>
        /// <param name="driver">Session nebo element, ve které se bude hledat</param>
        /// <param name="timeOut">Timeout v sekundách</param>
        /// <param name="pollingInterval">Interval, ve kterém se bude pokoušet hledat v milisekundách</param>
        /// <returns></returns>
        public static AppiumWebElement WaitForElementByName(string name, WindowsDriver<WindowsElement> driver, int timeOut, int pollingInterval)
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
                {
                    try
                    {
                         _ = driver.FindElementByName(name);
                        iterate = false;
                    }
                    catch (WebDriverException)
                    {
                        //LogSearchError(ex, automationId, controlName);
                    }
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
        public static AppiumWebElement WaitForElementByName(string name, WindowsElement driver, int timeOut, int pollingInterval)
        {
            if (driver == null) return null;

            var timer = new Stopwatch();
            var iterate = true;
            var timeOutSpan = TimeSpan.FromSeconds(timeOut);
            _ = TimeSpan.FromMilliseconds(pollingInterval);
            timer.Start();
            try
            {
                //while(timer.ElapsedMilliseconds < timeOut)
                //{
                //    tmp = FindElementByName(name, driver);
                //    if (tmp?.Displayed == true) return tmp;
                //    Thread.Sleep(_polling);
                //}
                while (timer.Elapsed <= timeOutSpan && iterate)
                {
                    try
                    {
                        _ = driver.FindElementByName(name);
                        iterate = false;
                    }
                    catch (WebDriverException)
                    {
                        //LogSearchError(ex, automationId, controlName);
                    }
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

        public static AppiumWebElement WaitForElementByAccessibilityId(string id, WindowsDriver<WindowsElement> driver, int timeOut, int pollingInterval)
        {
            if (driver == null) return null;

            var timer = new Stopwatch();
            var iterate = true;
            var timeOutSpan = TimeSpan.FromSeconds(timeOut);
            _ = TimeSpan.FromMilliseconds(pollingInterval);
            timer.Start();
            try
            {
                //while(timer.ElapsedMilliseconds < timeOut)
                //{
                //    tmp = FindElementByName(name, driver);
                //    if (tmp?.Displayed == true) return tmp;
                //    Thread.Sleep(_polling);
                //}
                while (timer.Elapsed <= timeOutSpan && iterate)
                {
                    try
                    {
                        _ = driver.FindElementByAccessibilityId(id);
                        iterate = false;
                    }
                    catch (WebDriverException)
                    {
                        //LogSearchError(ex, automationId, controlName);
                    }
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
        public static AppiumWebElement WaitForElementByAccessibilityId(string id, WindowsElement driver, int timeOut, int pollingInterval)
        {
            if (driver == null) return null;

            var timer = new Stopwatch();
            var iterate = true;
            var timeOutSpan = TimeSpan.FromSeconds(timeOut);
            _ = TimeSpan.FromMilliseconds(pollingInterval);
            timer.Start();
            try
            {
                //while(timer.ElapsedMilliseconds < timeOut)
                //{
                //    tmp = FindElementByName(name, driver);
                //    if (tmp?.Displayed == true) return tmp;
                //    Thread.Sleep(_polling);
                //}
                while (timer.Elapsed <= timeOutSpan && iterate)
                {
                    try
                    {
                        _ = driver.FindElementByAccessibilityId(id);
                        iterate = false;
                    }
                    catch (WebDriverException)
                    {
                        //LogSearchError(ex, automationId, controlName);
                    }
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
    }
}
