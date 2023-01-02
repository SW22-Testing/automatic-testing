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
using automatic_testing.Helpers.Elements;
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
        public void KontrolaKopirovaniHlavniOkno()
        {
            LoginHelper.Login(EsticonSession, UserHelper.EsticonUser.Login,
                UserHelper.EsticonUser.Password);

            EsticonSession = Setup.ConnectToRunningProcess(RootSession, "AspeEsticon");


            //TODO: v InteractableElement.cs přidat Double Click
            //TODO: Přidat metody pro vrácení listu elementů
            var versionLabel = InteractableElement.GetElementByAccessibilityId(EsticonSession, "lblVersionPrefix");
            MouseActionsHelper.DoubleClick(versionLabel);

            TestContext.WriteLine(Clipboard.GetText());
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
            EsticonSession.Quit();


            RootSession.Quit();

            Setup.WinAppDriverProcessClose();
            Setup.KillEveryInstance();

            ScreenRecorder.StopRecording();
        }


        /*-------------------------------------------------------------------------------------------------------*/

        private void Logout()
        {
            if (TestContext.CurrentContext.Test.Name == "Kontrola kopírování verze v okně nápověda před přihlášením") return;
            InteractableElement.ClickByName(EsticonSession, "Účet");
            InteractableElement.ClickByName(EsticonSession, "Odhlásit");
        }
        /// <summary>
        ///
        /// </summary>
        private void KontrolaNapoveda()
        {
            InteractableElement.ClickByName(EsticonSession, "Nápověda");

            var napovedaOkno = ParentElement.GetElementByName(RootSession, "Nápověda");
            //SearchHelper.GetParentElementByName(RootSession, "Nápověda", "Nepovedlo se najít okno Nápověda");

            var versionLabel = napovedaOkno.FindElementsByClassName("TextBox").FirstOrDefault(e => e.GetAttribute("HelpText") == "Dvojklikem zkopírujete číslo verze");
            MouseActionsHelper.DoubleClick(versionLabel);

            TestContext.WriteLine(Clipboard.GetText());
            Assert.AreEqual(versionLabel?.Text.Replace("ver. ", ""), Clipboard.GetText());

            var okButton = SearchHelper.GetClickableElementByName(napovedaOkno, "OK", "Nepovedlo se najít Tlačítko OK");
            okButton.Click();
        }
    }
}