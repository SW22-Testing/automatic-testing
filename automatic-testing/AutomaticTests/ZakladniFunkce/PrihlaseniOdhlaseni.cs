using automatic_testing.Classes;
using automatic_testing.Helpers.Predefined;
using automatic_testing.Helpers.Recording;
using automatic_testing.Helpers.Setup;
using automatic_testing.Helpers.SpecialActions;
using automatic_testing.Helpers.Tests;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using System.Threading.Tasks;
using System.Linq;

namespace automatic_testing.AutomaticTests.ZakladniFunkce
{
    [TestFixture]
    public class PrihlaseniOdhlaseni
    {
        public WinAppDriverSetup Setup { get; set; }
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
        private string Version { get; set; }

        [SetUp]
        public void SetUp()
        {
            Setup = new WinAppDriverSetup();
            Setup.KillEveryInstance();
            Setup.WinAppDriverProcessStart();

            ScreenRecorder = new ScreenRecorder();
            RootSession = Setup.GetRootSession();
            EsticonSession = Setup.GetEsticonSession();
            Version = SearchHelper.WaitForElementByAccessibilityId("lblVersion", EsticonSession, 5, 100).Text;
        }

        // Hotovo
        [TestCase(TestName = "Přihlášení pomocí správného účtu a správného hesla", Description = "Automatický test kontroluje přihlášení pomocí hesla"), Timeout(60000)]
        //[Ignore("Nefunguje nalezení okna AspeEsticon")]
        public void SpravnePrihlaseniSHeslem()
        {
            Assert.NotNull(EsticonSession, "Esticon session byla prázdná");
            ScreenRecorder.StartRecording(TestContext.CurrentContext.Test.Name, "Základní funkce", Version, "AspeEsticon");
            #region Testovací metodu a data
            LoginHelper.TryLogin(EsticonSession, UserHelper.EsticonUser.Login, UserHelper.EsticonUser.Password);

            EsticonSession = Setup.ConnectToRunningProcess(RootSession, "AspeEsticon");
            Assert.NotNull(EsticonSession, "Nenašlo se okno AspeEsticon");

            Logout();
            #endregion
        }


        [TestCase(TestName = "Přihlášení pomocí správného Windows účtu", Description = "Automatický test kontroluje přihlášení pomocí Windows účtu"), Timeout(60000)]
        //[Ignore("Nefunguje nalezení okna AspeEsticon")]
        public void SpravnePrihlaseniWindowsUctem()
        {
            ScreenRecorder.StartRecording(TestContext.CurrentContext.Test.Name, "Základní funkce", Version, "AspeEsticon");
            Assert.NotNull(EsticonSession, "Esticon session byla prázdná");

            bool login = LoginHelper.TryLogin(EsticonSession, UserHelper.EsticonUser.Login, UserHelper.EsticonUser.Password);
            Assert.IsTrue(login);

            EsticonSession = Setup.ConnectToRunningProcess(RootSession, "AspeEsticon");
            Assert.NotNull(EsticonSession, "Nepovedlo se napojit na instance AspeEsticon");

            var adminButton = EsticonSession.FindElementByName("Admin");
            Assert.NotNull(adminButton);
            adminButton.Click();

            var adminWindow = SearchHelper.WaitForElementByName("Administrátor", EsticonSession, 5, 100);
            Assert.NotNull(adminWindow);

            var uzivateleTab = SearchHelper.FindElementByName("Uživatelé", adminWindow);
            Assert.NotNull(uzivateleTab);
            uzivateleTab.Click();

            AppiumWebElement createNewUserButton = null;
            WindowsElement gridData = (WindowsElement)SearchHelper.FindElementByAccessibilityId("gcGrid", adminWindow);
            bool? isCreated = null;

            TestContext.WriteLine(SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).Where(e => e.GetAttribute("Value.Value") == UserHelper.WindowsUser.Login).Count());
            var parallelTask = Parallel.For(0, SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).Count(), e =>
            {
                isCreated = SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).Where(e => e.GetAttribute("Value.Value") == UserHelper.WindowsUser.Login).Count() != 0;
                if (isCreated == true) return;
            });
            //! Pokud se nenajde uživatel, tak se vytvoří, jinak otevře detail a zkontroluje stav offline
            if (isCreated == false)
            {
                createNewUserButton = EsticonSession.FindElementsByAccessibilityId("BarButtonItemLink").Where(e => e.GetAttribute("HelpText") == "Nový záznam").FirstOrDefault();
                Assert.NotNull(createNewUserButton, "Nenašlo se tlačítko Nový záznam");
                createNewUserButton.Click();

                var newUserWindow = SearchHelper.FindElementByName("Uživatel: < >", EsticonSession);
                Assert.NotNull(newUserWindow, "Okno pro nový účet se neotevřelo");
                CreateUser(newUserWindow, UserType.WindowsAuth, UserHelper.WindowsUser);

                var createButton = SearchHelper.FindElementByName("OK", EsticonSession);
                createButton.Click();
            }
            else
            {
                var windowsUser = SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).Where(e => e.GetAttribute("Value.Value") == UserHelper.WindowsUser.Login).First();
                Assert.NotNull(windowsUser, "Nenašel se správně záznam");
                MouseActionsHelper.DoubleClick(windowsUser);

                var newUserWindow = SearchHelper.FindElementByName($"Uživatel: <{UserHelper.NewUserProfile.LastName} {UserHelper.NewUserProfile.FirstName}>", RootSession);
                Assert.NotNull(newUserWindow, "Okno pro nový účet se neotevřelo");

                var offlineCheckBox = newUserWindow.FindElementByName("Aktivní");
                Assert.NotNull(offlineCheckBox);

                if (bool.Parse(offlineCheckBox.GetAttribute("Value.Value")) == true) offlineCheckBox.Click();

                var createButton = SearchHelper.FindElementByName("OK", EsticonSession);
                createButton.Click();
            }
            var adminOkButton = SearchHelper.FindElementByName("OK", adminWindow);
            Assert.NotNull(adminOkButton, "Nenašlo se tlačítko OK u okna Administrátor");
            adminOkButton.Click();

            Logout();

            var loginWindow = Setup.ConnectToRunningProcess(RootSession, "Přihlášení");
            login = LoginHelper.TryLogin(loginWindow);
            Assert.IsTrue(login);

            DisabledDataDialog(loginWindow);

            //TODO: Přidat kontrolu pro vytvořeného uživatele


            //esticonWindow = (WindowsElement)SearchHelper.WaitForElementByName("AspeEsticon", rootSession, 20, 100);
            ////while (esticonWindow == null)
            ////{
            ////    esticonWindow = SearchHelper.FindElementByName("AspeEsticon", rootSession);
            ////}
            ////esticonWindow = SearchHelper.FindElementByName("AspeEsticon", rootSession);

            //var ucetButton = SearchHelper.FindElementByName("Účet", esticonWindow);
            //Assert.NotNull(ucetButton, "Tlačítko Účet");
            //ucetButton.Click();
            ////Finder.GetScreenshot("Kliknutí na tlačítko Účet.png", rootSession);

            //WindowsElement odhlasitButton = (WindowsElement)SearchHelper.FindElementByName("Odhlásit", esticonWindow);
            //odhlasitButton.Click();
            //Assert.NotNull(odhlasitButton);
        }

        // Hotovo
        [TestCase(TestName = "Přihlášení pomocí správného účtu a špatného hesla", Description = "Automatický test kontroluje přihlášení pomocí hesla"), Timeout(20000)]
        public void PrihlaseniSeSpatnymHeslem()
        {
            ScreenRecorder.StartRecording(TestContext.CurrentContext.Test.Name, "Základní funkce", "AspeEsticon");

            Assert.NotNull(EsticonSession, "Esticon session byla prázdná");
            #region Testovací metodu a data
            var login = LoginHelper.TryLogin(EsticonSession, UserHelper.EsticonUser.Login, UserHelper.WrongUserProfile.Password);
            Assert.IsTrue(login);
            WrongDataDialog();
            #endregion
        }

        //Hotovo
        [TestCase(TestName = "Přihlášení pomocí špatného účtu a správného hesla", Description = "Automatický test kontroluje přihlášení pomocí hesla"), Timeout(20000)]
        public void PrihlaseniSeSpatnymLoginem()
        {
            ScreenRecorder.StartRecording(TestContext.CurrentContext.Test.Name, "Základní funkce", "AspeEsticon");

            Assert.NotNull(EsticonSession, "Esticon session byla prázdná");
            #region Přihlášení
            var login = LoginHelper.TryLogin(EsticonSession, UserHelper.WrongUserProfile.Login, UserHelper.EsticonUser.Password);
            Assert.IsTrue(login);
            WrongDataDialog();

            #endregion

        }
        [TestCase(TestName = "Přihlášení pomocí špatného Windows účtu", Description = "Automatický test kontroluje přihlášení pomocí hesla"), Timeout(180000)]
        /*TODO: Přihlásit -> Admin -> Zkontrolovat uživatele    -> Pokud není -> Odhlásit a Přihlásit se pomocí Windows ověření
         *                                                      -> Pokud je -> Odstranit uživatele -> Odhlásit a Přihlásit se pomocí Windows ověření
         */
        public void PrihlaseniSeSpatnymWindowsUctem()
        {

            ScreenRecorder.StartRecording(TestContext.CurrentContext.Test.Name, "Základní funkce", "AspeEsticon");
            Assert.NotNull(EsticonSession, "Esticon session byla prázdná");
            #region Přihlášení   
            LoginHelper.TryLogin(EsticonSession, UserHelper.EsticonUser.Login, UserHelper.EsticonUser.Password);
            #endregion
            //esticonWindow = (WindowsElement)SearchHelper.WaitForElementByName("AspeEsticon", rootSession, 20, 100);
            EsticonSession = Setup.ConnectToRunningProcess(RootSession, "AspeEsticon");

            var adminButton = SearchHelper.FindElementByName("Admin", EsticonSession);
            Assert.NotNull(adminButton, "Uživatel není admin, nebo tlačítko Admin není vidět");
            adminButton.Click();

            var adminWindow = SearchHelper.FindElementByName("Administrátor", EsticonSession);
            while (adminWindow == null)
            {
                adminWindow = SearchHelper.FindElementByName("Administrátor", EsticonSession);
            }
            adminWindow = SearchHelper.FindElementByName("Administrátor", EsticonSession);

            var uzivateleTab = SearchHelper.FindElementByName("Uživatelé", adminWindow);
            Assert.NotNull(uzivateleTab);
            uzivateleTab.Click();

            var createNewUserButton = SearchHelper.FindElementByAccessibilityId("BarButtonItemLink", adminWindow);
            while (createNewUserButton == null)
            {
                createNewUserButton = SearchHelper.FindElementByAccessibilityId("BarButtonItemLink", adminWindow);
            }
            createNewUserButton = SearchHelper.FindElementByAccessibilityId("BarButtonItemLink", adminWindow);
            Assert.NotNull(createNewUserButton, "Nenašlo se tlačítko Nový záznam");
            createNewUserButton.Click();

            var newUserWindow = SearchHelper.FindElementByName("Uživatel: < >", EsticonSession);
            Assert.NotNull(newUserWindow, "Okno pro nový účet se neotevřelo");

            var checkboxWindowLogin = SearchHelper.FindElementByName("povolit přihlášení Windows účtem", newUserWindow);
            if (bool.Parse(checkboxWindowLogin.GetAttribute("Value.Value")) == false)
                checkboxWindowLogin.Click();

            var userSearchButton = SearchHelper.FindElementByName("...", newUserWindow);
            Assert.NotNull(userSearchButton, "Nenašlo se tlačítko ...");
            userSearchButton.Click();

            var usersWindow = SearchHelper.FindElementByName("Výběr Windows uživatelů", EsticonSession);
            Assert.NotNull(usersWindow, "Nenašlo se okno, nebo se neotevřelo");

            //TODO: Předělat hledání sloupce pro login

            var loginColumn = SearchHelper.FindElementByName("RowData.Row.LoginName", usersWindow);
            while (loginColumn == null)
            {
                loginColumn = SearchHelper.FindElementByName("RowData.Row.LoginName", usersWindow);
            }
            loginColumn = SearchHelper.FindElementByName("RowData.Row.LoginName", usersWindow);
            Assert.NotNull(loginColumn, "Nenašel se element");

            //var usernameFilter = Finder.FindElementByName("FilterButton", (WindowsElement)loginColumn);
            //usernameFilter.Click();
            //usernameFilter.SendKeys(Environment.UserName);

            //var ucetButton = Finder.FindElementByName("Účet", esticonWindow);
            //Assert.NotNull(ucetButton, "Tlačítko Účet");
            //ucetButton.Click();
            ////Finder.GetScreenshot("Kliknutí na tlačítko Účet.png", rootSession);

            //WindowsElement odhlasitButton = (WindowsElement)Finder.FindElementByName("Odhlásit", esticonWindow);
            //odhlasitButton.Click();
            //Assert.NotNull(odhlasitButton);
        }
        [TestCase(TestName = "Přihlášení pomocí deaktovaného Windows účtu", Description = "Automatický test kontroluje přihlášení pomocí hesla"), Timeout(180000)]
        [Ignore("Nefunguje, jelikož nemám zapnutou VPN")]
        /*TODO: Přihlásit -> Admin -> Zkontrolovat uživatele    -> Pokud není -> Vytvořit nového uživatele s účtem, na kterém je přihlášen -> Odhlásit a Přihlásit se pomocí Windows ověření
         *                                                      -> Pokud je -> Zkontrolovat stav (online/offline)   -> Pokud online -> Zavřít -> Odhlásit a Přihlásit se pomocí Windows ověření
         *                                                                                                          -> Pokud offlie -> Změnit stav na online -> Odhlásit a Přihlásit se pomocí Windows ověření
         */
        public void PrihlaseniDeaktivovanymUctemWindows()
        {

            ScreenRecorder.StartRecording(TestContext.CurrentContext.Test.Name, "Základní funkce", "AspeEsticon");
            Assert.NotNull(EsticonSession, "Esticon session byla prázdná");
            #region Přihlášení   
            LoginHelper.TryLogin(EsticonSession, UserHelper.EsticonUser.Login, UserHelper.EsticonUser.Password);
            #endregion
            //esticonWindow = (WindowsElement)SearchHelper.WaitForElementByName("AspeEsticon", rootSession, 20, 100);
            EsticonSession = Setup.ConnectToRunningProcess(RootSession, "AspeEsticon");

            var adminButton = SearchHelper.FindElementByName("Admin", EsticonSession);
            Assert.NotNull(adminButton, "Uživatel není admin, nebo tlačítko Admin není vidět");
            adminButton.Click();

            var adminWindow = (WindowsElement)SearchHelper.WaitForElementByName("Administrátor", EsticonSession, 5, 500);

            var uzivateleTab = SearchHelper.FindElementByName("Uživatelé", adminWindow);
            Assert.NotNull(uzivateleTab);
            uzivateleTab.Click();

            AppiumWebElement createNewUserButton = null;
            WindowsElement gridData = (WindowsElement)SearchHelper.FindElementByAccessibilityId("gcGrid", adminWindow);
            bool? isCreated = null;

            TestContext.WriteLine(SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).Where(e => e.GetAttribute("Value.Value") == UserHelper.NewUserProfile.Login).Count());
            var parallelTask = Parallel.For(0, SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).Count(), e =>
            {
                isCreated = SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).Where(e => e.GetAttribute("Value.Value") == UserHelper.NewUserProfile.Login).Count() != 0;
                if (isCreated == true) return;
            });
            //! Pokud se nenajde uživatel, tak se vytvoří, jinak otevře detail a zkontroluje stav offline
            if (isCreated == false)
            {
                createNewUserButton = SearchHelper.FindElementsByAccessibilityId("BarButtonItemLink", adminWindow).Where(e => e.GetAttribute("HelpText") == "Nový záznam").FirstOrDefault();
                Assert.NotNull(createNewUserButton, "FindElementByAccessibilityId se tlačítko Nový záznam");
                createNewUserButton.Click();

                var newUserWindow = SearchHelper.FindElementByName("Uživatel: < >", EsticonSession);
                Assert.NotNull(newUserWindow, "Okno pro nový účet se neotevřelo");
                CreateUser(newUserWindow, UserType.PasswordAuth, UserHelper.NewUserProfile);

                var createButton = SearchHelper.FindElementByName("OK", EsticonSession);
                createButton.Click();
            }
            else
            {
                var windowsUser = SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).Where(e => e.GetAttribute("Value.Value") == UserHelper.NewUserProfile.Login).First();
                Assert.NotNull(windowsUser, "FindElementByAccessibilityId se tlačítko Nový záznam");
                MouseActionsHelper.DoubleClick(windowsUser);

                var newUserWindow = SearchHelper.FindElementByName($"Uživatel: <{UserHelper.NewUserProfile.LastName} {UserHelper.NewUserProfile.FirstName}>", RootSession);
                Assert.NotNull(newUserWindow, "Okno pro nový účet se neotevřelo");

                var offlineCheckBox = newUserWindow.FindElementByName("Aktivní");
                Assert.NotNull(offlineCheckBox);

                if (bool.Parse(offlineCheckBox.GetAttribute("Value.Value")) == true) offlineCheckBox.Click();

                var createButton = SearchHelper.FindElementByName("OK", EsticonSession);
                createButton.Click();
            }
            var adminOkButton = SearchHelper.FindElementByName("OK", adminWindow);
            Assert.NotNull(adminOkButton, "Nenašlo se tlačítko OK u okna Administrátor");
            adminOkButton.Click();

            Logout();

            var loginWindow = Setup.ConnectToRunningProcess(RootSession, "Přihlášení");
            var login = LoginHelper.TryLogin(loginWindow, UserHelper.NewUserProfile.Login, UserHelper.NewUserProfile.Password);
            Assert.IsTrue(login);

            DisabledDataDialog(loginWindow);
        }

// Hotovo
        [TestCase(TestName = "Přihlášení pomocí deaktivovaného účtu s heslem", Description = "Automatický test kontroluje přihlášení pomocí hesla"), Timeout(180000)]
        public void PrihlaseniDeaktovanymUctemHeslem()
        {
            ScreenRecorder.StartRecording(TestContext.CurrentContext.Test.Name, "Základní funkce", "AspeEsticon");
            Assert.NotNull(EsticonSession, "Esticon session byla prázdná");

            LoginHelper.TryLogin(EsticonSession, UserHelper.EsticonUser.Login, UserHelper.EsticonUser.Password);

            EsticonSession = Setup.ConnectToRunningProcess(RootSession, "AspeEsticon");

            var adminButton = SearchHelper.FindElementByName("Admin", EsticonSession);
            Assert.NotNull(adminButton, "Uživatel není admin, nebo tlačítko Admin není vidět");
            adminButton.Click();

            var adminWindow = (WindowsElement)SearchHelper.WaitForElementByName("Administrátor", EsticonSession, 5, 500);

            var uzivateleTab = SearchHelper.FindElementByName("Uživatelé", adminWindow);
            Assert.NotNull(uzivateleTab);
            uzivateleTab.Click();

            AppiumWebElement createNewUserButton = null;
            WindowsElement gridData = (WindowsElement)SearchHelper.FindElementByAccessibilityId("gcGrid", adminWindow);
            bool? isCreated = null;

            TestContext.WriteLine(SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).Where(e => e.GetAttribute("Value.Value") == UserHelper.NewUserProfile.Login).Count());
            var parallelTask = Parallel.For(0, SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).Count(), e =>
            {
                isCreated = SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).Where(e => e.GetAttribute("Value.Value") == UserHelper.NewUserProfile.Login).Count() != 0;
                if (isCreated == true) return;
            });
            //! Pokud se nenajde uživatel, tak se vytvoří, jinak otevře detail a zkontroluje stav offline
            if (isCreated == false)
            {
                createNewUserButton = SearchHelper.FindElementsByAccessibilityId("BarButtonItemLink", adminWindow).Where(e => e.GetAttribute("HelpText") == "Nový záznam").FirstOrDefault();
                Assert.NotNull(createNewUserButton, "FindElementByAccessibilityId se tlačítko Nový záznam");
                createNewUserButton.Click();

                var newUserWindow = SearchHelper.FindElementByName("Uživatel: < >", EsticonSession);
                Assert.NotNull(newUserWindow, "Okno pro nový účet se neotevřelo");
                CreateUser(newUserWindow, UserType.PasswordAuth, UserHelper.NewUserProfile);

                var createButton = SearchHelper.FindElementByName("OK", EsticonSession);
                createButton.Click();
            }
            else
            {
                var windowsUser = SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).Where(e => e.GetAttribute("Value.Value") == UserHelper.NewUserProfile.Login).First();
                Assert.NotNull(windowsUser, "FindElementByAccessibilityId se tlačítko Nový záznam");
                MouseActionsHelper.DoubleClick(windowsUser);

                var newUserWindow = SearchHelper.FindElementByName($"Uživatel: <{UserHelper.NewUserProfile.LastName} {UserHelper.NewUserProfile.FirstName}>", RootSession);
                Assert.NotNull(newUserWindow, "Okno pro nový účet se neotevřelo");

                var offlineCheckBox = newUserWindow.FindElementByName("Aktivní");
                Assert.NotNull(offlineCheckBox);

                if (bool.Parse(offlineCheckBox.GetAttribute("Value.Value")) == true) offlineCheckBox.Click();

                var createButton = SearchHelper.FindElementByName("OK", EsticonSession);
                createButton.Click();
            }
            var adminOkButton = SearchHelper.FindElementByName("OK", adminWindow);
            Assert.NotNull(adminOkButton, "Nenašlo se tlačítko OK u okna Administrátor");
            adminOkButton.Click();

            Logout();

            var loginWindow = Setup.ConnectToRunningProcess(RootSession, "Přihlášení");
            var login = LoginHelper.TryLogin(loginWindow, UserHelper.NewUserProfile.Login, UserHelper.NewUserProfile.Password);
            Assert.IsTrue(login);

            DisabledDataDialog(loginWindow);
        }


        [TearDown]
        public void TearDown()
        {
            if (EsticonSession != null)
            {
                //EsticonSession.Quit();
                EsticonSession.Dispose();
            }


            RootSession.Quit();
            RootSession.Dispose();

            Setup.WinAppDriverProcessClose();
            Setup.KillEveryInstance();

            ScreenRecorder.StopRecording();
        }


        /*-----------------------------------------------------------------------------------------------------------------------------------------------------------*/
        enum UserType
        {
            PasswordAuth,
            WindowsAuth
        }

        /// <summary>
        /// 
        /// </summary>
        private void Logout()
        {
            var ucetButton = SearchHelper.WaitForElementByName("Účet", EsticonSession, 5, 100);
            Assert.NotNull(ucetButton, "Tlačítko Účet");
            ucetButton.Click();
            WindowsElement odhlasitButton = SearchHelper.FindElementByName("Odhlásit", EsticonSession);
            odhlasitButton.Click();
            Assert.NotNull(odhlasitButton);
        }
        /// <summary>
        /// 
        /// </summary>
        private void WrongDataDialog()
        {
            var errorDialog = SearchHelper.FindElementByClassName("#32770", EsticonSession);
            Assert.NotNull(errorDialog, "Nevyskočil varovný dialog o nepovedeném přihlášení");

            var errorText = SearchHelper.FindElementByAccessibilityId("65535", errorDialog);
            Assert.AreEqual(@"Přihlášení se nezdařilo.
Chybně zadané heslo nebo uživatelské jméno.", errorText.Text, "Byla vypsána jiná chybový hláška");

            var errorButton = SearchHelper.FindElementByName("OK", errorDialog);
            Assert.NotNull(errorButton, "Chybí tlačítko");

            errorButton.Click();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginWindow"></param>
        private static void DisabledDataDialog(WindowsDriver<WindowsElement> loginWindow)
        {
            var errorWindow = loginWindow.FindElementByClassName("#32770");
            var errorText = errorWindow.FindElementByAccessibilityId("65535");
            var errorButton = errorWindow.FindElementByAccessibilityId("2");

            Assert.AreEqual(@"Tento účet byl deaktivován.
Kontaktujte administrátora.", errorText.Text);

            errorButton.Click();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newUserWindow"></param>
        private static void CreateUser(WindowsElement newUserWindow, UserType userType, UserProfile profile)
        {
            AppiumWebElement kodInput = null;
            AppiumWebElement jmenoInput = null;
            AppiumWebElement prijmeniInput = null;
            AppiumWebElement emailInput = null;
            var passwordInputs = SearchHelper.FindElementsByClassName("PasswordBoxEdit", newUserWindow);

            Task.Run(() =>
            {
                jmenoInput = newUserWindow.FindElementByXPath("//*[@Name='Jméno']/following-sibling::*");
                prijmeniInput = newUserWindow.FindElementByXPath("//*[@Name='Příjmení']/following-sibling::*");
                emailInput = newUserWindow.FindElementByXPath("//*[@Name='Email']/following-sibling::*");
            });

            switch (userType)
            {
                case UserType.PasswordAuth:
                    var loginInput = SearchHelper.FindElementByAccessibilityId("textEditLogin", newUserWindow);
                    Assert.NotNull(loginInput, "Nenašel se element Login");
                    loginInput.SendKeys(profile.Login);
                    Assert.AreEqual(profile.Login.Length, loginInput.Text.Length);


                    Assert.NotNull(passwordInputs, "Nenašly se elementy pro heslo");
                    for (int i = 0; i < passwordInputs.Count(); i++)
                    {
                        passwordInputs.ElementAt(i).SendKeys(UserHelper.NewUserProfile.Password);
                        Assert.AreEqual(UserHelper.NewUserProfile.Password.Length, passwordInputs.ElementAt(i).Text.Length);
                    }
                    break;
                case UserType.WindowsAuth:
                    var pswdloginCheckbox = newUserWindow.FindElementByName("povolit přihlášení jménem a heslem");
                    Assert.NotNull(pswdloginCheckbox);
                    pswdloginCheckbox.Click();

                    var windowsLoginCheckbox = newUserWindow.FindElementByName("povolit přihlášení Windows účtem");
                    Assert.NotNull(windowsLoginCheckbox);
                    windowsLoginCheckbox.Click();

                    var windowsInput = newUserWindow.FindElementByClassName("ButtonEdit");
                    Assert.NotNull(windowsInput);
                    windowsInput.SendKeys(profile.Login);
                    Assert.AreEqual(profile.Login, windowsInput.Text);

                    break;
            }

            kodInput = newUserWindow.FindElementByXPath("//*[@Name='Kód']/following-sibling::*");
            kodInput.SendKeys(profile.Kod);
            jmenoInput.SendKeys(profile.FirstName);
            prijmeniInput.SendKeys(profile.LastName);
            emailInput.SendKeys(profile.Email);

            var offlineCheckBox = newUserWindow.FindElementByName("Aktivní");
            Assert.NotNull(offlineCheckBox);
            offlineCheckBox.Click();
        }
    }
}
