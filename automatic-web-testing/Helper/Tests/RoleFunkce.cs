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
    // public enum RoleFunkce
    // {
    //     Smazani = 0,
    //     Editace = 1,
    //     Vytvoreni = 2,
    //     Aktivita = 3
    // }
    // public class RoleHelper
    // {
    //     /// <summary>
    //     /// 
    //     /// </summary>
    //     /// <param name="driver"></param>
    //     /// <param name="wait"></param>
    //     /// <param name="index"></param>
    //     /// <param name="func"></param>
    //     /// <returns></returns>
    //     public static bool VytvoreniRole(ChromeDriver? driver, WebDriverWait wait, out int index, RoleFunkce func)
    //     {
    //         index = 0;
    //         if (driver == null) return false;
    //
    //         try
    //         {
    //             var projekty = driver.FindElement(By.LinkText("Projekty"));
    //             Assert.NotNull(projekty);
    //             projekty.Click();
    //
    //             var automatizovanyProjekt = SearchHelper.WaitForElementsByTagName("span", driver, 5, 250).FirstOrDefault(e => e.Text == "Projekt pro automatizované testy");
    //             Assert.NotNull(automatizovanyProjekt);
    //             automatizovanyProjekt.Click();
    //
    //             var nastaveniProjektu = SearchHelper.WaitForElementsByTagName("li", driver, 5, 250).FirstOrDefault(e => e.Text == "Nastavení projektu");
    //             Assert.NotNull(nastaveniProjektu);
    //             nastaveniProjektu.Click();
    //
    //             var role = SearchHelper.WaitForElementsByTagName("li", driver, 5, 250).FirstOrDefault(e => e.Text == "Role");
    //             Assert.NotNull(role);
    //             role.Click();
    //
    //             var isCreated = false;
    //             var kontrolaRole = SearchHelper.WaitForElementsByClassName("RoleInfo_roleName__3xBnw", driver, 5, 250).FirstOrDefault(e => e.Text == "Autonom");
    //
    //             var _role = SearchHelper.WaitForElementsByClassName("RoleInfo_roleName__3xBnw", driver, 5, 250);
    //
    //             for (int i = 0; i < _role.Count; i++)
    //             {
    //                 if (_role[i].Text == "Autonom")
    //                     isCreated = true;
    //                 index = i;
    //             }
    //             switch (func)
    //             {
    //                 case RoleFunkce.Vytvoreni:
    //                     // Zkontroluje jestli role je nebo neni
    //                     if (!isCreated)
    //                     {
    //                         var vytvoritRole = driver.FindElements(By.TagName("button")).FirstOrDefault(e => e.Text == "Přidat roli");
    //                         Assert.NotNull(vytvoritRole);
    //                         vytvoritRole.Click();
    //
    //                         var nazevRole = SearchHelper.WaitForElementById("name", driver, 5, 250);
    //                         nazevRole.SendKeys("Autonom");
    //                         Assert.AreEqual("Autonom", nazevRole.GetAttribute("value"));
    //
    //                         var popisRole = SearchHelper.WaitForElementById("description", driver, 5, 250);
    //                         popisRole.SendKeys("Jsem automatický test který zapisuje do popisku!");
    //                         Assert.AreEqual("Jsem automatický test který zapisuje do popisku!", popisRole.GetAttribute("value"));
    //
    //                         var uzivatelRole = SearchHelper.WaitForElementById("userId", driver, 5, 250);
    //                         Assert.NotNull(uzivatelRole);
    //                         uzivatelRole.Click();
    //
    //                         var vyberUzivatele = driver.FindElements(By.TagName("li")).FirstOrDefault(e => e.Text == "Testerprohub@protonmail.com");
    //                         Assert.NotNull(vyberUzivatele);
    //                         vyberUzivatele.Click();
    //
    //                         var ulozitVytvor = driver.FindElements(By.TagName("button")).FirstOrDefault(e => e.Text == "Uložit");
    //                         Assert.NotNull(ulozitVytvor);
    //                         ulozitVytvor.Click();
    //                         return true;
    //                     }
    //                     else
    //                     {
    //                         kontrolaRole?.Click();
    //                         Assert.NotNull(kontrolaRole);
    //                         //! Thread nemusí být, je spíš pro jistotu
    //                         Thread.Sleep(500);
    //                         //? Pokud bude dělat problémy, zkusit napsat index + 1
    //                         var odstranit = driver.FindElements(By.ClassName("anticon-delete")).ElementAt(index);
    //                         Assert.NotNull(odstranit);
    //                         odstranit.Click();
    //
    //                         var odstranitPotvrdit = driver.FindElements(By.TagName("button")).First(e => e.Text == "Ano");
    //                         Assert.NotNull(odstranitPotvrdit);
    //                         odstranitPotvrdit.Click();
    //
    //                         var vytvoritRole = driver.FindElements(By.TagName("button")).FirstOrDefault(e => e.Text == "Přidat roli");
    //                         Assert.NotNull(vytvoritRole);
    //                         vytvoritRole.Click();
    //
    //                         var nazevRole = SearchHelper.WaitForElementById("name", driver, 5, 250);
    //                         nazevRole.SendKeys("Autonom");
    //                         Assert.AreEqual("Autonom", nazevRole.GetAttribute("value"));
    //
    //                         var popisRole = SearchHelper.WaitForElementById("description", driver, 5, 250);
    //                         popisRole.SendKeys("Jsem automatický test který zapisuje do popisku!");
    //                         Assert.AreEqual("Jsem automatický test který zapisuje do popisku!", popisRole.GetAttribute("value"), "Pokud se tohle stalo tak je to proto že je v popisku napsáno něco navíc");
    //
    //                         var uzivatelRole = SearchHelper.WaitForElementById("userId", driver, 5, 250);
    //                         Assert.NotNull(uzivatelRole);
    //                         uzivatelRole.Click();
    //
    //                         var vyberUzivatele = driver.FindElements(By.TagName("li")).FirstOrDefault(e => e.Text == "Testerprohub@protonmail.com");
    //                         Assert.NotNull(vyberUzivatele);
    //                         vyberUzivatele.Click();
    //
    //                         var ulozitVytvor = driver.FindElements(By.TagName("button")).FirstOrDefault(e => e.Text == "Uložit");
    //                         Assert.NotNull(ulozitVytvor);
    //                         ulozitVytvor.Click();
    //                         return true;
    //                     }
    //
    //                 case RoleFunkce.Editace:
    //
    //                     if (!isCreated)
    //                     {
    //                         var vytvoritRole = driver.FindElements(By.TagName("button")).FirstOrDefault(e => e.Text == "Přidat roli");
    //                         Assert.NotNull(vytvoritRole);
    //                         vytvoritRole.Click();
    //
    //                         var nazevRole = SearchHelper.WaitForElementById("name", driver, 5, 250);
    //                         nazevRole.SendKeys("Autonom");
    //                         Assert.AreEqual("Autonom", nazevRole.GetAttribute("value"));
    //
    //                         var popisRole = SearchHelper.WaitForElementById("description", driver, 5, 250);
    //                         popisRole.SendKeys("Jsem automatický test který zapisuje do popisku!");
    //                         Assert.AreEqual("Jsem automatický test který zapisuje do popisku!", popisRole.GetAttribute("value"));
    //
    //                         var uzivatelRole = SearchHelper.WaitForElementById("userId", driver, 5, 250);
    //                         Assert.NotNull(uzivatelRole);
    //                         uzivatelRole.Click();
    //
    //                         var vyberUzivatele = driver.FindElements(By.TagName("li")).FirstOrDefault(e => e.Text == "Testerprohub@protonmail.com");
    //                         Assert.NotNull(vyberUzivatele);
    //                         vyberUzivatele.Click();
    //
    //                         var ulozitVytvor = driver.FindElements(By.TagName("button")).FirstOrDefault(e => e.Text == "Uložit");
    //                         Assert.NotNull(ulozitVytvor);
    //                         ulozitVytvor.Click();
    //
    //                         var roleVytvorena = SearchHelper.WaitForElementsByClassName("RoleInfo_roleName__3xBnw", driver, 5, 250).FirstOrDefault(e => e.Text == "Autonom");
    //                         Assert.NotNull(roleVytvorena);
    //                         roleVytvorena.Click();
    //
    //                         var upravit = driver.FindElements(By.ClassName("anticon-edit")).ElementAt(index - 1);
    //                         Assert.NotNull(upravit);
    //                         upravit.Click();
    //
    //                         var dopsani = SearchHelper.WaitForElementById("description", driver, 5, 250);
    //                         dopsani.SendKeys(" Hele dopsal jsem ti to :)");
    //                         Assert.AreEqual("Jsem automatický test který zapisuje do popisku! Hele dopsal jsem ti to :)", dopsani.GetAttribute("value"));
    //
    //                         var ulozitVytvorPopis = driver.FindElements(By.TagName("button")).FirstOrDefault(e => e.Text == "Uložit");
    //                         Assert.NotNull(ulozitVytvorPopis);
    //                         ulozitVytvor.Click();
    //                         return true;
    //                     }
    //                     else
    //                     {
    //                         kontrolaRole?.Click();
    //                         Assert.NotNull(kontrolaRole);
    //
    //                         var upravit = driver.FindElements(By.ClassName("anticon-edit")).ElementAt(index - 1);
    //                         Assert.NotNull(upravit);
    //                         upravit.Click();
    //
    //                         var dopsani = SearchHelper.WaitForElementById("description", driver, 5, 250);
    //                         dopsani.SendKeys(" Hele dopsal jsem ti to :)");
    //                         Assert.AreEqual("Jsem automatický test který zapisuje do popisku! Hele dopsal jsem ti to :)", dopsani.GetAttribute("value"), "Pokud se objevila tady chyba tak je v popisku něco navíc");
    //
    //                         var ulozitVytvor = driver.FindElements(By.TagName("button")).FirstOrDefault(e => e.Text == "Uložit");
    //                         Assert.NotNull(ulozitVytvor);
    //                         ulozitVytvor.Click();
    //                         return true;
    //                     }
    //
    //                 case RoleFunkce.Aktivita:
    //                     if (true)
    //                     {
    //                     }
    //                     //!Tohle pak vymaž
    //                     break;
    //             }
    //             return true;
    //         }
    //         catch
    //         {
    //             return false;
    //         }
    //     }
    // }
}
