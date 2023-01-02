using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace automatic_web_testing.Helper.Tests
{
    public static class SearchHelper
    {
        //!Uprav celý pro asserty
        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="driver"></param>
        /// <param name="timeout"></param>
        /// ///
        /// <param name="pollinginterval"></param>
        /// <returns></returns>
        public static IWebElement WaitForElementById(string id, ChromeDriver driver, int timeout, int pollinginterval)
        {
            if (driver == null) return null;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.PollingInterval = TimeSpan.FromMilliseconds(pollinginterval);
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
            return driver.FindElement(By.Id(id));
        }

        /// <summary>
        /// </summary>
        /// <param name="className"></param>
        /// <param name="driver"></param>
        /// <param name="timeout"></param>
        /// <param name="pollinginterval"></param>
        /// <returns></returns>
        public static IWebElement WaitForElementByClassName(string className, ChromeDriver driver, int timeout,
            int pollinginterval)
        {
            if (driver == null) return null;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout))
            {
                PollingInterval = TimeSpan.FromMilliseconds(pollinginterval)
            };
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(className)));
            return driver.FindElement(By.ClassName(className));
        }

        public static IList<IWebElement> WaitForElementsByClassName(string className, IWebElement driver, int timeout,
            int pollinginterval)
        {
            if (driver == null) return null;
            IWebElement tmp = null;
            while (tmp == null) tmp = driver.FindElement(By.ClassName(className));
            return driver.FindElements(By.ClassName(className));
        }

        /// <summary>
        /// </summary>
        /// <param name="className"></param>
        /// <param name="driver"></param>
        /// <param name="timeout"></param>
        /// <param name="pollinginterval"></param>
        /// <returns></returns>
        public static IList<IWebElement> WaitForElementsByClassName(string className, ChromeDriver driver, int timeout,
            int pollinginterval)
        {
            if (driver == null) return null;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.PollingInterval = TimeSpan.FromMilliseconds(pollinginterval);
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(className)));
            return driver.FindElements(By.ClassName(className));
        }

        /// <summary>
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="driver"></param>
        /// <param name="timeout"></param>
        /// <param name="pollinginterval"></param>
        /// <returns></returns>
        public static IWebElement WaitForElementByTagName(string tagName, ChromeDriver driver, int timeout,
            int pollinginterval)
        {
            if (driver == null) return null;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.PollingInterval = TimeSpan.FromMilliseconds(pollinginterval);
            wait.Until(ExpectedConditions.ElementIsVisible(By.TagName(tagName)));
            return driver.FindElement(By.TagName(tagName));
        }

        /// <summary>
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="driver"></param>
        /// <param name="timeout"></param>
        /// <param name="pollinginterval"></param>
        /// <returns></returns>
        public static IList<IWebElement> WaitForElementsByTagName(string tagName, ChromeDriver driver, int timeout,
            int pollinginterval)
        {
            if (driver == null) return null;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.PollingInterval = TimeSpan.FromMilliseconds(pollinginterval);
            wait.Until(ExpectedConditions.ElementIsVisible(By.TagName(tagName)));
            return driver.FindElements(By.TagName(tagName));
        }
    }
}