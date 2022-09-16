using automatic_web_testing.Helper.Predefined;
using automatic_web_testing.Helper.Setup;
using automatic_web_testing.Helper.Tests;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace automatic_web_testing.AutomaticTests.ZakladniFunkce
{
    public class ZalozeniRole
    {
        // ChromeDriver driver { get; set; }
        // private WebDriverWait wait { get; set; }
        // [SetUp]
        // public void Setup()
        // {
        //     WebDriverWait _wait;
        //     driver = ChromeDriverSetup.Setup("https://dv1.aspehub.cz/Account", out _wait);
        //     wait = _wait;
        // }
        //
        // [TestCase(TestName = "Kontrola role + smazání", Description = "Kontroluje jestli role kterou chceme založit existuje pokud existuje tak ji smaže, potom ji založí a pokud neexistuje založí ji"), Order(1)]
        // public void KontrolaRole()
        // {
        //     driver = LoginHelper.Login(driver, wait);
        //     Assert.NotNull(driver);
        //     bool roleStatus = RoleHelper.VytvoreniRole(driver, wait, out int indexOfRole, RoleFunkce.Vytvoreni);
        //     Assert.True(roleStatus);
        //     //Thread.Sleep(2000);
        // }
        //
        // [TestCase(TestName = "Úprava role", Description = "Upraví roli = přidá do popisku další text. Také pokud role není založí ji"), Order(2)]
        // public void UpravaRole()
        // {
        //     driver = LoginHelper.Login(driver, wait);
        //     Assert.NotNull(driver);
        //
        //     bool roleStatus = RoleHelper.VytvoreniRole(driver, wait, out int indexOfRole, RoleFunkce.Editace);
        //     Thread.Sleep(200);
        //     Assert.True(roleStatus);
        //
        // }
        //
        // [TestCase(TestName = "Aktivita na roli", Description = "Aktivita na roli = Podívá se na aktivitu na roli a porovná ji jestli je vše v pořádku."), Order(3)]
        // public void AktivitaNaRoli()
        // {
        //     driver = LoginHelper.Login(driver, wait);
        //     Assert.NotNull(driver);
        //
        //     IWebElement projekty = driver.FindElement(By.LinkText("Projekty"));
        //     Assert.NotNull(projekty);
        //     projekty.Click();
        //
        //     IWebElement automatizovanyProjekt = SearchHelper.WaitForElementsByTagName("span", driver, 5, 250).Where(e => e.Text == "Projekt pro automatizované testy").FirstOrDefault();
        //     Assert.NotNull(automatizovanyProjekt);
        //     automatizovanyProjekt.Click();
        //
        //     IWebElement nastaveniProjektu = SearchHelper.WaitForElementsByTagName("li", driver, 5, 250).Where(e => e.Text == "Nastavení projektu").FirstOrDefault();
        //     Assert.NotNull(nastaveniProjektu);
        //     nastaveniProjektu.Click();
        //
        //     IWebElement role = SearchHelper.WaitForElementsByTagName("li", driver, 5, 250).Where(e => e.Text == "Role").FirstOrDefault();
        //     Assert.NotNull(role);
        //     role.Click();
        //
        //     bool isCreated = false;
        //     IWebElement kontrolaRole = SearchHelper.WaitForElementsByClassName("RoleInfo_roleName__3xBnw", driver, 5, 250).Where(e => e.Text == "Autonom").FirstOrDefault();
        //
        //     var _role = SearchHelper.WaitForElementsByClassName("RoleInfo_roleName__3xBnw", driver, 5, 250);
        //     int index = 0;
        //     for (int i = 0; i < _role.Count; i++)
        //     {
        //         if (_role[i].Text == "Autonom")
        //             isCreated = true;
        //         index = i;
        //     }
        //
        //     TestContext.WriteLine(isCreated);
        //
        //     if (isCreated == true)
        //     {
        //         kontrolaRole.Click();
        //         Assert.NotNull(kontrolaRole);
        //
        //         IWebElement aktivita = driver.FindElements(By.ClassName("anticon-history")).ElementAt(3);
        //         Assert.NotNull(aktivita);
        //         Thread.Sleep(200);
        //         aktivita.Click();
        //
        //         var itemCollection = SearchHelper.WaitForElementsByClassName("ant-collapse", driver, 5, 250);
        //         IWebElement zmenaKlik = itemCollection.ElementAt(0).FindElements(By.TagName("div")).ElementAt(0);
        //
        //         Assert.NotNull(zmenaKlik);
        //         zmenaKlik.Click();
        //
        //         IList<IWebElement> historieAktivit = driver.FindElements(By.TagName("tbody"));
        //         Assert.NotNull(historieAktivit);
        //
        //         var popisZmeny = historieAktivit[0].FindElements(By.TagName("td"));
        //         var autorZmeny = historieAktivit[1].FindElements(By.TagName("td"));
        //         Thread.Sleep(500);
        //
        //         var task = Task.Run(() =>
        //         {
        //             Assert.AreEqual("Popis", popisZmeny[0].Text, "Špatný parametr | Parametr se nenašel");
        //             Assert.AreEqual("Jsem automatický test který zapisuje do popisku!", popisZmeny[1].Text, "Špatný parametr | Parametr se nenašel");
        //             Assert.AreEqual("Jsem automatický test který zapisuje do popisku! Hele dopsal jsem ti to :)", popisZmeny[2].Text, "Špatný parametr | Parametr se nenašel");
        //         });
        //         Assert.AreEqual("Email", autorZmeny[0].Text, "Špatný parametr | Parametr se nenašel");
        //         Assert.AreEqual(UserHelper.CorrectUser.Email, autorZmeny[1].Text, "Špatný parametr | Parametr se nenašel");
        //
        //         task.Wait();
        //     }
        //     TestContext.WriteLine("Kontrola hodnot proběhla úspěšně");
        // }
        //
        // [TearDown]
        // public void TearDown()
        // {
        //     ChromeDriverSetup.Dispose(driver);
        // }
    }
}
