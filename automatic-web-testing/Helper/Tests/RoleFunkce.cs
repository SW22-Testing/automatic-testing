using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace automatic_web_testing.Helper.Tests
{
    public enum RoleFunkce
    {
        Smazani = 0,
        Editace = 1,
        Vytvoreni = 2,
        Aktivita = 3
    }
    public class RoleHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="wait"></param>
        /// <param name="index"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static bool VytvoreniRole(ChromeDriver? driver, WebDriverWait wait, out int index, RoleFunkce func)
        {
            index = 0;
            if (driver == null) return false;

            try
            {
                IWebElement projekty = driver.FindElement(By.LinkText("Projekty"));
                Assert.NotNull(projekty);
                projekty.Click();

                IWebElement automatizovanyProjekt = SearchHelper.WaitForElementsByTagName("span", driver, 5, 250).Where(e => e.Text == "Projekt pro automatizované testy").FirstOrDefault();
                Assert.NotNull(automatizovanyProjekt);
                automatizovanyProjekt.Click();

                IWebElement nastaveniProjektu = SearchHelper.WaitForElementsByTagName("li", driver, 5, 250).Where(e => e.Text == "Nastavení projektu").FirstOrDefault();
                Assert.NotNull(nastaveniProjektu);
                nastaveniProjektu.Click();

                IWebElement role = SearchHelper.WaitForElementsByTagName("li", driver, 5, 250).Where(e => e.Text == "Role").FirstOrDefault();
                Assert.NotNull(role);
                role.Click();

                bool isCreated = false;
                IWebElement kontrolaRole = SearchHelper.WaitForElementsByClassName("RoleInfo_roleName__3xBnw", driver, 5, 250).Where(e => e.Text == "Autonom").FirstOrDefault();

                var _role = SearchHelper.WaitForElementsByClassName("RoleInfo_roleName__3xBnw", driver, 5, 250);

                for (int i = 0; i < _role.Count; i++)
                {
                    if (_role[i].Text == "Autonom")
                        isCreated = true;
                    index = i;
                }
                switch (func)
                {
                    case RoleFunkce.Vytvoreni:
                        // Zkontroluje jestli role je nebo neni
                        if (!isCreated)
                        {
                            IWebElement vytvoritRole = driver.FindElements(By.TagName("button")).Where(e => e.Text == "Přidat roli").FirstOrDefault();
                            Assert.NotNull(vytvoritRole);
                            vytvoritRole.Click();

                            IWebElement nazevRole = SearchHelper.WaitForElementById("name", driver, 5, 250);
                            nazevRole.SendKeys("Autonom");
                            Assert.AreEqual("Autonom", nazevRole.GetAttribute("value"));

                            IWebElement popisRole = SearchHelper.WaitForElementById("description", driver, 5, 250);
                            popisRole.SendKeys("Jsem automatický test který zapisuje do popisku!");
                            Assert.AreEqual("Jsem automatický test který zapisuje do popisku!", popisRole.GetAttribute("value"));

                            IWebElement uzivatelRole = SearchHelper.WaitForElementById("userId", driver, 5, 250);
                            Assert.NotNull(uzivatelRole);
                            uzivatelRole.Click();

                            IWebElement vyberUzivatele = driver.FindElements(By.TagName("li")).Where(e => e.Text == "Testerprohub@protonmail.com").FirstOrDefault();
                            Assert.NotNull(vyberUzivatele);
                            vyberUzivatele.Click();

                            IWebElement ulozitVytvor = driver.FindElements(By.TagName("button")).Where(e => e.Text == "Uložit").FirstOrDefault();
                            Assert.NotNull(ulozitVytvor);
                            ulozitVytvor.Click();
                            return true;
                        }
                        else
                        {
                            kontrolaRole.Click();
                            Assert.NotNull(kontrolaRole);
                            //! Thread nemusí být, je spíš pro jistotu
                            Thread.Sleep(500);
                            //? Pokud bude dělat problémy, zkusit napsat index + 1
                            IWebElement odstranit = driver.FindElements(By.ClassName("anticon-delete")).ElementAt(index);
                            Assert.NotNull(odstranit);
                            odstranit.Click();

                            IWebElement odstranitPotvrdit = driver.FindElements(By.TagName("button")).Where(e => e.Text == "Ano").First();
                            Assert.NotNull(odstranitPotvrdit);
                            odstranitPotvrdit.Click();

                            IWebElement vytvoritRole = driver.FindElements(By.TagName("button")).Where(e => e.Text == "Přidat roli").FirstOrDefault();
                            Assert.NotNull(vytvoritRole);
                            vytvoritRole.Click();

                            IWebElement nazevRole = SearchHelper.WaitForElementById("name", driver, 5, 250);
                            nazevRole.SendKeys("Autonom");
                            Assert.AreEqual("Autonom", nazevRole.GetAttribute("value"));

                            IWebElement popisRole = SearchHelper.WaitForElementById("description", driver, 5, 250);
                            popisRole.SendKeys("Jsem automatický test který zapisuje do popisku!");
                            Assert.AreEqual("Jsem automatický test který zapisuje do popisku!", popisRole.GetAttribute("value"), "Pokud se tohle stalo tak je to proto že je v popisku napsáno něco navíc");

                            IWebElement uzivatelRole = SearchHelper.WaitForElementById("userId", driver, 5, 250);
                            Assert.NotNull(uzivatelRole);
                            uzivatelRole.Click();

                            IWebElement vyberUzivatele = driver.FindElements(By.TagName("li")).Where(e => e.Text == "Testerprohub@protonmail.com").FirstOrDefault();
                            Assert.NotNull(vyberUzivatele);
                            vyberUzivatele.Click();

                            IWebElement ulozitVytvor = driver.FindElements(By.TagName("button")).Where(e => e.Text == "Uložit").FirstOrDefault();
                            Assert.NotNull(ulozitVytvor);
                            ulozitVytvor.Click();
                            return true;
                        }

                    case RoleFunkce.Editace:

                        if (!isCreated)
                        {
                            IWebElement vytvoritRole = driver.FindElements(By.TagName("button")).Where(e => e.Text == "Přidat roli").FirstOrDefault();
                            Assert.NotNull(vytvoritRole);
                            vytvoritRole.Click();

                            IWebElement nazevRole = SearchHelper.WaitForElementById("name", driver, 5, 250);
                            nazevRole.SendKeys("Autonom");
                            Assert.AreEqual("Autonom", nazevRole.GetAttribute("value"));

                            IWebElement popisRole = SearchHelper.WaitForElementById("description", driver, 5, 250);
                            popisRole.SendKeys("Jsem automatický test který zapisuje do popisku!");
                            Assert.AreEqual("Jsem automatický test který zapisuje do popisku!", popisRole.GetAttribute("value"));

                            IWebElement uzivatelRole = SearchHelper.WaitForElementById("userId", driver, 5, 250);
                            Assert.NotNull(uzivatelRole);
                            uzivatelRole.Click();

                            IWebElement vyberUzivatele = driver.FindElements(By.TagName("li")).Where(e => e.Text == "Testerprohub@protonmail.com").FirstOrDefault();
                            Assert.NotNull(vyberUzivatele);
                            vyberUzivatele.Click();

                            IWebElement ulozitVytvor = driver.FindElements(By.TagName("button")).Where(e => e.Text == "Uložit").FirstOrDefault();
                            Assert.NotNull(ulozitVytvor);
                            ulozitVytvor.Click();

                            IWebElement roleVytvorena = SearchHelper.WaitForElementsByClassName("RoleInfo_roleName__3xBnw", driver, 5, 250).Where(e => e.Text == "Autonom").FirstOrDefault();
                            Assert.NotNull(roleVytvorena);
                            roleVytvorena.Click();

                            IWebElement upravit = driver.FindElements(By.ClassName("anticon-edit")).ElementAt(index - 1);
                            Assert.NotNull(upravit);
                            upravit.Click();

                            IWebElement dopsani = SearchHelper.WaitForElementById("description", driver, 5, 250);
                            dopsani.SendKeys(" Hele dopsal jsem ti to :)");
                            Assert.AreEqual("Jsem automatický test který zapisuje do popisku! Hele dopsal jsem ti to :)", dopsani.GetAttribute("value"));

                            IWebElement ulozitVytvorPopis = driver.FindElements(By.TagName("button")).Where(e => e.Text == "Uložit").FirstOrDefault();
                            Assert.NotNull(ulozitVytvorPopis);
                            ulozitVytvor.Click();
                            return true;
                        }
                        else
                        {
                            kontrolaRole.Click();
                            Assert.NotNull(kontrolaRole);

                            IWebElement upravit = driver.FindElements(By.ClassName("anticon-edit")).ElementAt(index - 1);
                            Assert.NotNull(upravit);
                            upravit.Click();

                            IWebElement dopsani = SearchHelper.WaitForElementById("description", driver, 5, 250);
                            dopsani.SendKeys(" Hele dopsal jsem ti to :)");
                            Assert.AreEqual("Jsem automatický test který zapisuje do popisku! Hele dopsal jsem ti to :)", dopsani.GetAttribute("value"),"Pokud se objevila tady chyba tak je v popisku něco navíc");

                            IWebElement ulozitVytvor = driver.FindElements(By.TagName("button")).Where(e => e.Text == "Uložit").FirstOrDefault();
                            Assert.NotNull(ulozitVytvor);
                            ulozitVytvor.Click();
                            return true;
                        }

                    case RoleFunkce.Aktivita:
                        if (true)
                        {
                        }
                        //!Tohle pak vymaž
                        break;
                        
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
