using NUnit.Framework;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium.Support.UI;

namespace automatic_testing.Helpers.Tests
{
    public class SearchHelper
    {
        //TODO: Doplnit <returns> pro Summary

        /// <summary>
        /// Vyhledá element podle parametru Name a zkontroluje stav tlačítka
        /// </summary>
        /// <param name="driver">Objekt, ve kterém se bude vyhledávat</param>
        /// <param name="name">Parametr Name hledaného tlačítka</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav tlačítka</param>
        /// <returns></returns>
        public static AppiumWebElement GetClickableElementByName(WindowsDriver<WindowsElement> driver, string name, string errorDesc, bool enabled = true)
        {
            WindowsElement el = driver.FindElementByName(name);
            
            Assert.IsNotNull(el);
            Assert.True(el.Displayed);
            if (enabled)
                Assert.True(el.Enabled, errorDesc);
            else
                Assert.False(el.Enabled, errorDesc);

            return el;
        }
        /// <summary>
        /// Vyhledá element podle parametru Name a zkontroluje stav tlačítka
        /// </summary>
        /// <param name="driver">Element, ve kterém se bude vyhledávat</param>
        /// <param name="name">Parametr Name hledaného tlačítka</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav tlačítka</param>
        /// <returns></returns>
        public static AppiumWebElement GetClickableElementByName(AppiumWebElement driver, string name, string errorDesc, bool enabled = true)
        {
            AppiumWebElement el = driver.FindElementByName(name);

            Assert.IsNotNull(el);
            Assert.True(el.Displayed);
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
        /// <param name="name">Parametr Name hledaného tlačítka</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav tlačítka</param>
        /// <returns></returns>
        public static ICollection<AppiumWebElement> GetClickableElementsByName(WindowsDriver<WindowsElement> driver, string name, string errorDesc, bool enabled = true)
        {
            ICollection<AppiumWebElement> els = (ICollection<AppiumWebElement>)driver.FindElementsByName(name);
            foreach (AppiumWebElement el in els)
            {
                Assert.IsNotNull(el);
                Assert.True(el.Displayed);
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
        /// <param name="name">Parametr Name hledaného tlačítka</param>
        /// <param name="errorDesc">Vypsaná chyba, pokud neprojde kontrola</param>
        /// <param name="enabled">Stav tlačítka</param>
        /// <returns></returns>
        public static ICollection<AppiumWebElement> GetClickableElementsByName(AppiumWebElement driver, string name, string errorDesc, bool enabled = true)
        {
            ICollection<AppiumWebElement> els = (ICollection<AppiumWebElement>)driver.FindElementsByName(name);
            foreach (AppiumWebElement el in els)
            {
                Assert.IsNotNull(el);
                Assert.True(el.Displayed);
                if (enabled)
                    Assert.True(el.Enabled, errorDesc);
                else
                    Assert.False(el.Enabled, errorDesc);
            }
            return els;
        }


        #region Hledání pomocí Name
        
        /// <summary>
        /// Vrátí řadu elementů, které mají stejný název jako parametr: <c>name</c>
        /// </summary>
        /// <param name="name">Název elementu</param>
        /// <param name="driver">Session, ve které se bude hledat</param>
        /// <returns></returns>
        public static IEnumerable<AppiumWebElement> FindElementsByName(string name, WindowsDriver<WindowsElement> driver)
        {
            if (driver == null)
                return null;

            try
            {
                return driver.FindElementsByName(name);
            }
            catch
            {
                return null;
            }
        }
        #endregion
        #region Hledání pomocí AutomationId
        /// <summary>
        /// Vratí 1 element, který má stejný název jako parametr: <c>name</c>
        /// </summary>
        /// <param name="automationId">AutomationId elementu</param>
        /// <param name="session">Session, ve které se bude hledat</param>
        /// <returns></returns>
        public static WindowsElement FindElementByAccessibilityId(string automationId, WindowsDriver<WindowsElement> session)
        {
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
        public static AppiumWebElement FindElementByAccessibilityId(string automationId, AppiumWebElement? session)
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
        public static IEnumerable<AppiumWebElement> FindElementsByAccessibilityId(string automationId, WindowsElement? driver)
        {
            if (driver == null)
                return null;
            try
            {
                return driver.FindElementsByAccessibilityId(automationId);
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
        public static IEnumerable<AppiumWebElement> FindElementsByAccessibilityId(string automationId, WindowsDriver<WindowsElement> driver)
        {
            if (driver == null)
                return null;
            try
            {
                return driver.FindElementsById(automationId);
            }
            catch
            {
                return null;
            }
        }
        #endregion
        #region Hledání pomocí ClassName
        /// <summary>
        /// Vratí 1 element, který má stejný název jako parametr: <c>className</c>
        /// </summary>
        /// <param name="className">ClassName elementu</param>
        /// <param name="session">Session, ve které se bude hledat</param>
        /// <returns></returns>
        public static WindowsElement FindElementByClassName(string className, WindowsDriver<WindowsElement> session)
        {
            try
            {
                return session.FindElementByClassName(className);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Vratí řadu element, které mají stejný AutomationId jako parametr: <c>className</c>
        /// </summary>
        /// <param name="className">ClassName elementu</param>
        /// <param name="session">Session, ve které se bude hledat</param>
        /// <returns></returns>
        public static AppiumWebElement FindElementByClassName(string className, WindowsElement? session)
        {
            if (session == null)
                return null;
            try
            {
                return session.FindElementByClassName(className);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Vratí řadu element, které mají stejný AutomationId jako parametr: <c>className</c>
        /// </summary>
        /// <param name="className">ClassName elementu</param>
        /// <param name="driver">Session, ve které se bude hledat</param>
        /// <returns></returns>
        public static IEnumerable<AppiumWebElement> FindElementsByClassName(string className, WindowsElement? driver)
        {
            if (driver == null)
                return null;
            try
            {
                return driver.FindElementsByClassName(className);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Vratí řadu element, které mají stejný AutomationId jako parametr: <c>className</c>
        /// </summary>
        /// <param name="className">ClassName elementu</param>
        /// <param name="driver">Session, ve které se bude hledat</param>
        /// <returns></returns>
        public static IEnumerable<AppiumWebElement> FindElementsByClassName(string className, WindowsDriver<WindowsElement> driver)
        {
            if (driver == null)
                return null;
            try
            {
                return driver.FindElementsByClassName(className);
            }
            catch
            {
                return null;
            }
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

            Stopwatch timer = new Stopwatch();
            bool iterate = true;
            var _timeOut = TimeSpan.FromSeconds(timeOut);
            _ = TimeSpan.FromMilliseconds(pollingInterval);
            timer.Start();
            try
            {
                while (timer.Elapsed <= _timeOut && iterate == true)
                {
                    try
                    {
                        AppiumWebElement tmp = driver.FindElementByName(name);
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

            Stopwatch timer = new Stopwatch();
            bool iterate = true;
            var _timeOut = TimeSpan.FromSeconds(timeOut);
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
                while (timer.Elapsed <= _timeOut && iterate == true)
                {
                    try
                    {
                        AppiumWebElement tmp = driver.FindElementByName(name);
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

            Stopwatch timer = new Stopwatch();
            bool iterate = true;
            var _timeOut = TimeSpan.FromSeconds(timeOut);
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
                while (timer.Elapsed <= _timeOut && iterate == true)
                {
                    try
                    {
                        AppiumWebElement tmp = driver.FindElementByAccessibilityId(id);
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

            Stopwatch timer = new Stopwatch();
            bool iterate = true;
            var _timeOut = TimeSpan.FromSeconds(timeOut);
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
                while (timer.Elapsed <= _timeOut && iterate == true)
                {
                    try
                    {
                        AppiumWebElement tmp = driver.FindElementByAccessibilityId(id);
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
