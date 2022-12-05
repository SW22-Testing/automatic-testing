using System;
using automatic_testing.Classes;
using automatic_testing.Helpers.Predefined;
using screen_recorder;
using automatic_testing.Helpers.Setup;
using automatic_testing.Helpers.SpecialActions;
using automatic_testing.Helpers.Tests;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using System.Threading.Tasks;
using System.Linq;
using OpenQA.Selenium;
using System.Windows;
using TextCopy;


namespace automatic_testing.AutomaticTests.ZakladniFunkce
{
    [TestFixture]
    public class KopirovaniVerze
    {
        private WinAppDriverSetup Setup { get; set; }
        /// <summary>
        /// Root session se všemi okny zařízení
        /// </summary>
        private WindowsDriver<WindowsElement> RootSession { get; set; }
        /// <summary>
        /// session pro AspeEsticon => Jen otevře esticon.exe
        /// </summary>
        private WindowsDriver<WindowsElement> EsticonSession { get; set; }
        /// <summary>
        /// Nahrávání obrazovky
        /// </summary>
        private ScreenRecorder ScreenRecorder { get; set; }
        //private string Version { get; set; }
        private AppiumWebElement Version { get; set; }

        [SetUp]
        public void SetUp()
        {
            Setup = new WinAppDriverSetup();
            Setup.KillEveryInstance();
            Setup.WinAppDriverProcessStart();

            ScreenRecorder = new ScreenRecorder();
            RootSession = Setup.GetRootSession();
            EsticonSession = Setup.GetEsticonSession();

            Version = SearchHelper.WaitForElementByAccessibilityId("lblVersion", EsticonSession, 5, 100);
        }

        [TestCase(TestName = "Kontrola kopírování verze v hlavním okně", Description = "Automatický test kontroluje kopírování"), Timeout(60000)]
        [STAThread]
        //[Ignore("Nefunguje nalezení okna AspeEsticon")]
        public void KontrolaKopirovaniHlavniOkno()
        {
            LoginHelper.Login(EsticonSession, UserHelper.EsticonUser.Login,
                UserHelper.EsticonUser.Password);

            EsticonSession = Setup.ConnectToRunningProcess(RootSession, "AspeEsticon");

            var versionLabel = SearchHelper.WaitForElementByAccessibilityId("lblVersionPrefix", EsticonSession, 5, 100);
            MouseActionsHelper.DoubleClick(versionLabel);

            Assert.AreEqual(Version.Text.Replace("ver. ", ""), Clipboard.GetText());
        }

        [TestCase(TestName = "Kontrola kopírování verze v okně nápověda", Description = "Automatický test kontroluje kopírování"), Timeout(60000)]
        [STAThread]
        public void KontrolaKopirovaniNapoveda()
        {
            LoginHelper.Login(EsticonSession, UserHelper.EsticonUser.Login,
                UserHelper.EsticonUser.Password);

            EsticonSession = Setup.ConnectToRunningProcess(RootSession, "AspeEsticon");

            KontrolaNapoveda();
        }

        [TestCase(TestName = "Kontrola kopírování verze v okně nápověda před přihlášením", Description = "Automatický test kontroluje kopírování"), Timeout(60000)]
        [STAThread]
        [Ignore("Prozatím se nekontroluje, jelikož nenachází text Nápověda")]
        public void KontrolaKopirovaniNapovedaZPrihlaseni()
        {
            KontrolaNapoveda();
        }

        [TearDown]
        public void TearDown()
        {
            Logout();
            //EsticonSession.Quit();
            EsticonSession?.Dispose();


            RootSession.Quit();
            RootSession.Dispose();

            Setup.WinAppDriverProcessClose();
            Setup.KillEveryInstance();

            ScreenRecorder.StopRecording();
        }


        /*-------------------------------------------------------------------------------------------------------*/

        private void Logout()
        {
            if (TestContext.CurrentContext.Test.Name == "Kontrola kopírování verze v okně nápověda před přihlášením") return;
            var ucetButton = SearchHelper.WaitForElementByName("Účet", EsticonSession, 5, 100);
            Assert.NotNull(ucetButton, "Tlačítko Účet");
            ucetButton.Click();
            AppiumWebElement odhlasitButton = SearchHelper.GetClickableElementByName(EsticonSession, "Odhlásit", "Nepovedlo se najít tlačítko Odhlásit");
            odhlasitButton.Click();
        }
        /// <summary>
        ///
        /// </summary>
        private void KontrolaNapoveda()
        {
            var napovedaButton = SearchHelper.WaitForElementByName("Nápověda", EsticonSession, 10, 250);
            napovedaButton?.Click();

            var napovedaOkno =
                SearchHelper.GetParentElementByName(EsticonSession, "Nápověda", "Nepovedlo se najít okno Nápověda");

            var versionLabel = SearchHelper.GetClickableElementsByClassName(napovedaOkno, "TextBox", "Nepovedlo se najít verzi")
                .FirstOrDefault(e => e.GetAttribute("HelpText") == "Dvojklikem zkopírujete číslo verze");
            MouseActionsHelper.DoubleClick(versionLabel);

            TestContext.WriteLine(versionLabel?.GetAttribute("Value.Value"));
            Assert.AreEqual(versionLabel?.Text.Replace("ver. ", ""), Clipboard.GetText());

            var okButton = SearchHelper.GetClickableElementByName(napovedaOkno, "OK", "Nepovedlo se najít Tlačítko OK");
            okButton.Click();
        }
    }
}