using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using automatic_web_testing.Helper.Predefined;
using automatic_web_testing.Helper.Setup;
using automatic_web_testing.Helper.Tests;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using screen_recorder;

namespace automatic_web_testing.AutomaticTests.ZakladniFunkce
{
    public class ZalozeniRole
    {
        private ChromeDriver driver { get; set; }
        private ScreenRecorder Recorder { get; set; }
        private WebDriverWait wait { get; set; }

        [SetUp]
        public void Setup()
        {
            WebDriverWait _wait;
            driver = ChromeDriverSetup.Setup("https://dv1.aspehub.cz/Account", out _wait);
            Recorder = new ScreenRecorder();

            wait = _wait;
        }

        [TestCase(TestName = "Kontrola role + smazání",
            Description =
                "Kontroluje jestli role kterou chceme založit existuje pokud existuje tak ji smaže, potom ji založí a pokud neexistuje založí ji")]
        public void KontrolaRole()
        {
            Recorder.StartRecording(TestContext.CurrentContext.Test.Name, "Vytvoření role", "", "AspeHub");
            driver = LoginHelper.Login(driver, wait);
            Assert.NotNull(driver);

            var roleStatus = RoleHelper.VytvoreniRole(driver, wait, out var indexOfRole, RoleFunkce.Vytvoreni);
            Assert.True(roleStatus);
            //Thread.Sleep(2000);
        }

        [TestCase(TestName = "Úprava role",
            Description = "Upraví roli = přidá do popisku další text. Také pokud role není založí ji")]
        public void UpravaRole()
        {
            Recorder.StartRecording(TestContext.CurrentContext.Test.Name, "Úprava role", "", "AspeHub");
            driver = LoginHelper.Login(driver, wait);
            Assert.NotNull(driver);

            var roleStatus = RoleHelper.VytvoreniRole(driver, wait, out var indexOfRole, RoleFunkce.Editace);
            Thread.Sleep(200);
            Assert.True(roleStatus);
        }

        [TestCase(TestName = "Aktivita na roli",
            Description = "Aktivita na roli = Podívá se na aktivitu na roli a porovná ji jestli je vše v pořádku.")]
        public void AktivitaNaRoli()
        {
            Recorder.StartRecording(TestContext.CurrentContext.Test.Name, "Aktivita v roli", "", "AspeHub");
            driver = LoginHelper.Login(driver, wait);
            Assert.NotNull(driver);

            var roleStatus = RoleHelper.VytvoreniRole(driver, wait, out var indexOfRole, RoleFunkce.Vytvoreni);
            Assert.True(roleStatus);
            Thread.Sleep(2000);

            var isCreated = false;
            var kontrolaRole = SearchHelper.WaitForElementsByClassName("RoleInfo_roleName__3xBnw", driver, 5, 250)
                .FirstOrDefault(e => e.Text == "Autonom");
            PripravaProAktivituRole(ref isCreated, ref kontrolaRole);

            TestContext.WriteLine(isCreated);

            if (isCreated)
            {
                kontrolaRole.Click();
                Assert.NotNull(kontrolaRole);

                var aktivita = driver.FindElements(By.ClassName("anticon-history")).ElementAt(3);
                Assert.NotNull(aktivita);
                Thread.Sleep(200);
                aktivita.Click();

                var itemCollection = SearchHelper.WaitForElementsByClassName("ant-collapse", driver, 5, 250);
                var zmenaKlik = itemCollection.ElementAt(0).FindElements(By.TagName("div")).ElementAt(0);

                Assert.NotNull(zmenaKlik);
                zmenaKlik.Click();

                IList<IWebElement> historieAktivit = driver.FindElements(By.TagName("tbody"));
                Assert.NotNull(historieAktivit);

                var popisZmeny = historieAktivit[0].FindElements(By.TagName("td"));
                var autorZmeny = historieAktivit[1].FindElements(By.TagName("td"));
                Thread.Sleep(500);

                var task = Task.Run(() =>
                {
                    Assert.AreEqual("Popis", popisZmeny[0].Text, "Špatný parametr | Parametr se nenašel");
                    Assert.AreEqual("Jsem automatický test který zapisuje do popisku!", popisZmeny[1].Text,
                        "Špatný parametr | Parametr se nenašel");
                    Assert.AreEqual("Jsem automatický test který zapisuje do popisku! Hele dopsal jsem ti to :)",
                        popisZmeny[2].Text, "Špatný parametr | Parametr se nenašel");
                });
                Assert.AreEqual("Email", autorZmeny[0].Text, "Špatný parametr | Parametr se nenašel");
                Assert.AreEqual(UserHelper.CorrectUser.Email, autorZmeny[1].Text,
                    "Špatný parametr | Parametr se nenašel");

                task.Wait();
            }

            TestContext.WriteLine("Kontrola hodnot proběhla úspěšně");
        }


        [TearDown]
        public void TearDown()
        {
            Recorder.StopRecording();
            ChromeDriverSetup.Dispose(driver);
        }
        private void PripravaProAktivituRole(ref bool isCreated, ref IWebElement kontrolaRole )
        {
            var role = SearchHelper.WaitForElementsByClassName("RoleInfo_roleName__3xBnw", driver, 5, 250);
            var index = 0;

            for (var i = 0; i < role.Count; i++)
            {
                if (role[i].Text == "Autonom")
                    isCreated = true;
                index = i;
            }

            if (!isCreated)
            {
                var vytvoritRole = driver.FindElements(By.TagName("button"))
                    .FirstOrDefault(e => e.Text == "Přidat roli");
                Assert.NotNull(vytvoritRole);
                vytvoritRole.Click();

                var nazevRole = SearchHelper.WaitForElementById("name", driver, 5, 250);
                nazevRole.SendKeys("Autonom");
                Assert.AreEqual("Autonom", nazevRole.GetAttribute("value"));

                var popisRole = SearchHelper.WaitForElementById("description", driver, 5, 250);
                popisRole.SendKeys("Jsem automatický test který zapisuje do popisku!");
                Assert.AreEqual("Jsem automatický test který zapisuje do popisku!", popisRole.GetAttribute("value"));

                var uzivatelRole = SearchHelper.WaitForElementById("userId", driver, 5, 250);
                Assert.NotNull(uzivatelRole);
                uzivatelRole.Click();

                var vyberUzivatele = driver.FindElements(By.TagName("li"))
                    .FirstOrDefault(e => e.Text == "Testerprohub@protonmail.com");
                Assert.NotNull(vyberUzivatele);
                vyberUzivatele.Click();

                var ulozitVytvor = driver.FindElements(By.TagName("button")).FirstOrDefault(e => e.Text == "Uložit");
                Assert.NotNull(ulozitVytvor);
                ulozitVytvor.Click();

                var roleVytvorena = SearchHelper.WaitForElementsByClassName("RoleInfo_roleName__3xBnw", driver, 5, 250)
                    .FirstOrDefault(e => e.Text == "Autonom");
                Assert.NotNull(roleVytvorena);
                roleVytvorena.Click();

                var upravit = driver.FindElements(By.ClassName("anticon-edit")).ElementAt(index - 1);
                Assert.NotNull(upravit);
                upravit.Click();

                var dopsani = SearchHelper.WaitForElementById("description", driver, 5, 250);
                dopsani.SendKeys(" Hele dopsal jsem ti to :)");
                Assert.AreEqual("Jsem automatický test který zapisuje do popisku! Hele dopsal jsem ti to :)",
                    dopsani.GetAttribute("value"));

                var ulozitVytvorPopis =
                    driver.FindElements(By.TagName("button")).FirstOrDefault(e => e.Text == "Uložit");
                Assert.NotNull(ulozitVytvorPopis);
                ulozitVytvor.Click();
            }
            else
            {
                kontrolaRole?.Click();
                Assert.NotNull(kontrolaRole);

                var upravit = driver.FindElements(By.ClassName("anticon-edit")).ElementAt(index - 1);
                Assert.NotNull(upravit);
                upravit.Click();

                var dopsani = SearchHelper.WaitForElementById("description", driver, 5, 250);
                dopsani.SendKeys(" Hele dopsal jsem ti to :)");
                Assert.AreEqual("Jsem automatický test který zapisuje do popisku! Hele dopsal jsem ti to :)",
                    dopsani.GetAttribute("value"), "Pokud se objevila tady chyba tak je v popisku něco navíc");

                var ulozitVytvor = driver.FindElements(By.TagName("button")).FirstOrDefault(e => e.Text == "Uložit");
                Assert.NotNull(ulozitVytvor);
                ulozitVytvor.Click();
            }
        }
    }
}