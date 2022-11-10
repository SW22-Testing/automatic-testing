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

namespace automatic_testing.AutomaticTests.ZakladniFunkce
{
    [TestFixture]
    public class PrihlaseniOdhlaseni
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

            var login = LoginHelper.TryLogin(EsticonSession, UserHelper.EsticonUser.Login, UserHelper.EsticonUser.Password);
            Assert.IsTrue(login);

            EsticonSession = Setup.ConnectToRunningProcess(RootSession, "AspeEsticon");
            Assert.NotNull(EsticonSession, "Nepovedlo se napojit na instance AspeEsticon");

            var adminButton = SearchHelper.GetClickableElementByName(EsticonSession, "Admin", "Nepovedlo se najít tlačítko Admin");
            adminButton.Click();

            var adminWindow = SearchHelper.WaitForElementByName("Administrátor", EsticonSession, 5, 100);
            Assert.NotNull(adminWindow);

            var uzivateleTab = SearchHelper.GetClickableElementByName(adminWindow, "Uživatelé", "Nepoedlo se najít tlačítko Uživatelé");
            uzivateleTab.Click();

            //TODO: Předělat část kódu, aby fungovala s AppiumWebElement
            var gridData = (WindowsElement)SearchHelper.FindElementByAccessibilityId("gcGrid", adminWindow);
            var isCreated = false;

            TestContext.WriteLine(SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).Count(e => e.GetAttribute("Value.Value") == UserHelper.WindowsUser.Login));
            Parallel.For(0, SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).Count(), e =>
            {
                isCreated = SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).Count(f => f.GetAttribute("Value.Value") == UserHelper.WindowsUser.Login) != 0;
            });

            //! Pokud se nenajde uživatel, tak se vytvoří, jinak otevře detail a zkontroluje stav offline
            //? Zkusit zkontrolovat při hledání uživatele a pokud je online, tak změnit na offline
            if (isCreated == false)
            {
                var createNewUserButton = SearchHelper
                    .GetClickableElementsByClassName(adminWindow, "LightweightBarItemLinkControl", "Nový záznam")
                    .FirstOrDefault(e => e.GetAttribute("HelpText") == "Nový záznam");

                createNewUserButton?.Click();

                var newUserWindow = SearchHelper.GetParentElementByName(EsticonSession, "Uživatel: < >", "Nenašlo se okno Nového uživatele");

                CreateUser(newUserWindow as WindowsElement, UserType.WindowsAuth, UserHelper.WindowsUser);

                var createButton = SearchHelper.GetClickableElementByName(EsticonSession, "OK", "Problém s tlačítkem Vytvoření nového uživatele");
                createButton.Click();
            }
            else
            {
                var windowsUser = SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).First(e => e.GetAttribute("Value.Value") == UserHelper.WindowsUser.Login);
                Assert.NotNull(windowsUser, "Nenašel se správně záznam");
                MouseActionsHelper.DoubleClick(windowsUser);

                //? Mozná zbytečné tady (Ve windows účtu není definované jméno a přijmění
                var user = $"{UserHelper.NewUserProfile.LastName} {UserHelper.NewUserProfile.FirstName}";

                var newUserWindow = SearchHelper.GetParentElementByName(
                    RootSession, $"Uživatel: < >"
                    , $"Nepovedlo se najít okno detailu uzivatele {user}");
                Assert.NotNull(newUserWindow, "Okno pro nový účet se neotevřelo");

                var offlineCheckBox = SearchHelper.GetClickableElementByName(newUserWindow, "Aktivní", "Problém s checkboxem přepnutí na Offline");

                if (bool.Parse(offlineCheckBox.GetAttribute("Value.Value"))) offlineCheckBox.Click();

                var createButton = SearchHelper.GetClickableElementByName(EsticonSession, "OK", "Problém s tlačítkem Vytvoření nového uživatele");
                createButton.Click();
            }
            var adminOkButton = SearchHelper.GetClickableElementByName(adminWindow, "OK", "Problém s tlačítkem OK (ADMIN OKNO)");
            adminOkButton.Click();

            Logout();

            var loginWindow = Setup.ConnectToRunningProcess(RootSession, "Přihlášení");
            login = LoginHelper.TryLogin(loginWindow);
            Assert.IsTrue(login);

            DisabledDataDialog(loginWindow);
        }

        // Hotovo
        [TestCase(TestName = "Přihlášení pomocí správného účtu a špatného hesla", Description = "Automatický test kontroluje přihlášení pomocí hesla"), Timeout(20000)]
        public void PrihlaseniSeSpatnymHeslem()
        {
            ScreenRecorder.StartRecording(TestContext.CurrentContext.Test.Name, "Základní funkce", Version, "AspeEsticon");

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
            ScreenRecorder.StartRecording(TestContext.CurrentContext.Test.Name, "Základní funkce", Version, "AspeEsticon");

            Assert.NotNull(EsticonSession, "Esticon session byla prázdná");
            #region Přihlášení
            var login = LoginHelper.TryLogin(EsticonSession, UserHelper.WrongUserProfile.Login,
                UserHelper.EsticonUser.Password);
            Assert.IsTrue(login);
            WrongDataDialog();

            #endregion

        }
        //[TestCase(TestName = "Přihlášení pomocí špatného Windows účtu", Description = "Automatický test kontroluje přihlášení pomocí hesla"), Timeout(180000)]
        ///*TODO: Přihlásit -> Admin -> Zkontrolovat uživatele    -> Pokud není -> Odhlásit a Přihlásit se pomocí Windows ověření
        // *                                                      -> Pokud je -> Odstranit uživatele -> Odhlásit a Přihlásit se pomocí Windows ověření
        // */
        //public void PrihlaseniSeSpatnymWindowsUctem()
        //{

        //    ScreenRecorder.StartRecording(TestContext.CurrentContext.Test.Name, "Základní funkce", "AspeEsticon");
        //    Assert.NotNull(EsticonSession, "Esticon session byla prázdná");
        //    #region Přihlášení   
        //    LoginHelper.TryLogin(EsticonSession, UserHelper.EsticonUser.Login, UserHelper.EsticonUser.Password);
        //    #endregion
        //    //esticonWindow = (WindowsElement)SearchHelper.WaitForElementByName("AspeEsticon", rootSession, 20, 100);
        //    EsticonSession = Setup.ConnectToRunningProcess(RootSession, "AspeEsticon");

        //    var adminButton = SearchHelper.GetClickableElementByName(EsticonSession, "Admin", "Nepovedlo se najít tlačítko Admin");
        //    adminButton.Click();

        //    var adminWindow = SearchHelper.FindElementByName("Administrátor", EsticonSession);
        //    while (adminWindow == null)
        //    {
        //        adminWindow = SearchHelper.FindElementByName("Administrátor", EsticonSession);
        //    }
        //    adminWindow = SearchHelper.FindElementByName("Administrátor", EsticonSession);

        //    var uzivateleTab = SearchHelper.GetClickableElementByName(adminWindow,"Uživatelé", "Nepovedlo se najít tlačítko Uživatelé (ADMIN OKNO)");
        //    uzivateleTab.Click();

        //    var createNewUserButton = SearchHelper.FindElementByAccessibilityId("BarButtonItemLink", adminWindow);
        //    while (createNewUserButton == null)
        //    {
        //        createNewUserButton = SearchHelper.FindElementByAccessibilityId("BarButtonItemLink", adminWindow);
        //    }
        //    createNewUserButton = SearchHelper.FindElementByAccessibilityId("BarButtonItemLink", adminWindow);
        //    Assert.NotNull(createNewUserButton, "Nenašlo se tlačítko Nový záznam");
        //    createNewUserButton.Click();

        //    var newUserWindow = SearchHelper.FindElementByName("Uživatel: < >", EsticonSession);
        //    Assert.NotNull(newUserWindow, "Okno pro nový účet se neotevřelo");

        //    var checkboxWindowLogin = SearchHelper.GetClickableElementByName(newUserWindow, "povolit přihlášení Windows účtem", "Nepovedlo se najít checkbox pro Windows login v oknu nového uživatele");
        //    if (bool.Parse(checkboxWindowLogin.GetAttribute("Value.Value")) == false)
        //        checkboxWindowLogin.Click();

        //    var userSearchButton = SearchHelper.GetClickableElementByName(newUserWindow, "...", "Nepovedlo se najít tlačítko ... v oknu nového uživatele");
        //    userSearchButton.Click();

        //    var usersWindow = SearchHelper.FindElementByName("Výběr Windows uživatelů", EsticonSession);
        //    Assert.NotNull(usersWindow, "Nenašlo se okno, nebo se neotevřelo");

        //    //TODO: Předělat hledání sloupce pro login

        //    var loginColumn = SearchHelper.FindElementByName("RowData.Row.LoginName", usersWindow);
        //    while (loginColumn == null)
        //    {
        //        loginColumn = SearchHelper.FindElementByName("RowData.Row.LoginName", usersWindow);
        //    }
        //    loginColumn = SearchHelper.FindElementByName("RowData.Row.LoginName", usersWindow);
        //    Assert.NotNull(loginColumn, "Nenašel se element");

        //    //var usernameFilter = Finder.FindElementByName("FilterButton", (WindowsElement)loginColumn);
        //    //usernameFilter.Click();
        //    //usernameFilter.SendKeys(Environment.UserName);

        //    //var ucetButton = Finder.FindElementByName("Účet", esticonWindow);
        //    //Assert.NotNull(ucetButton, "Tlačítko Účet");
        //    //ucetButton.Click();
        //    ////Finder.GetScreenshot("Kliknutí na tlačítko Účet.png", rootSession);

        //    //WindowsElement odhlasitButton = (WindowsElement)Finder.FindElementByName("Odhlásit", esticonWindow);
        //    //odhlasitButton.Click();
        //    //Assert.NotNull(odhlasitButton);
        //}
        [TestCase(TestName = "Přihlášení pomocí deaktovaného Windows účtu", Description = "Automatický test kontroluje přihlášení pomocí hesla"), Timeout(180000)]
        [Ignore("Není naprogramováno")]
        /*TODO: Přihlásit -> Admin -> Zkontrolovat uživatele    -> Pokud není -> Vytvořit nového uživatele s účtem, na kterém je přihlášen -> Odhlásit a Přihlásit se pomocí Windows ověření
         *                                                      -> Pokud je -> Zkontrolovat stav (online/offline)   -> Pokud online -> Zavřít -> Odhlásit a Přihlásit se pomocí Windows ověření
         *                                                                                                          -> Pokud offline -> Změnit stav na online -> Odhlásit a Přihlásit se pomocí Windows ověření
         */
        public void PrihlaseniDeaktivovanymUctemWindows()
        {

            ScreenRecorder.StartRecording(TestContext.CurrentContext.Test.Name, "Základní funkce", Version, "AspeEsticon");
            Assert.NotNull(EsticonSession, "Esticon session byla prázdná");
            #region Přihlášení   
            LoginHelper.TryLogin(EsticonSession, UserHelper.EsticonUser.Login, UserHelper.EsticonUser.Password);
            #endregion
            //esticonWindow = (WindowsElement)SearchHelper.WaitForElementByName("AspeEsticon", rootSession, 20, 100);
            EsticonSession = Setup.ConnectToRunningProcess(RootSession, "AspeEsticon");

            var adminButton = SearchHelper.GetClickableElementByName(EsticonSession, "Admin", "Nepovedlo se najít tlačítko Admin");
            adminButton.Click();

            var adminWindow = (WindowsElement)SearchHelper.WaitForElementByName("Administrátor", EsticonSession, 5, 500);

            var uzivateleTab = SearchHelper.GetClickableElementByName(adminWindow, "Uživatelé", "Nepoedlo se najít tlačítko Uživatelé");
            uzivateleTab.Click();

            WindowsElement gridData = (WindowsElement)SearchHelper.FindElementByAccessibilityId("gcGrid", adminWindow);
            bool? isCreated = null;

            TestContext.WriteLine(SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).Count(e => e.GetAttribute("Value.Value") == UserHelper.NewUserProfile.Login));
            Parallel.For(0, SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).Count(), e =>
            {
                isCreated = SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).Count(f => f.GetAttribute("Value.Value") == UserHelper.NewUserProfile.Login) != 0;
            });
            //! Pokud se nenajde uživatel, tak se vytvoří, jinak otevře detail a zkontroluje stav offline
            if (isCreated == false)
            {
                var createNewUserButton = SearchHelper.FindElementsByAccessibilityId("BarButtonItemLink", adminWindow).FirstOrDefault(e => e.GetAttribute("HelpText") == "Nový záznam");
                Assert.NotNull(createNewUserButton, "FindElementByAccessibilityId se tlačítko Nový záznam");
                createNewUserButton.Click();

                var newUserWindow = SearchHelper.GetParentElementByName(
                    RootSession, $"Uživatel: < >"
                    , $"Nepovedlo se najít okno detailu nového uzivatele");
                Assert.NotNull(newUserWindow, "Okno pro nový účet se neotevřelo");
                CreateUser(newUserWindow as WindowsElement, UserType.PasswordAuth, UserHelper.NewUserProfile);

                var createButton = SearchHelper.GetClickableElementByName(EsticonSession, "OK", "Problém s tlačítkem Vytvoření nového uživatele");
                createButton.Click();
            }
            else
            {
                var windowsUser = SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).First(e => e.GetAttribute("Value.Value") == UserHelper.NewUserProfile.Login);
                Assert.NotNull(windowsUser, "FindElementByAccessibilityId se tlačítko Nový záznam");
                MouseActionsHelper.DoubleClick(windowsUser);


                var user = $"{UserHelper.NewUserProfile.LastName} {UserHelper.NewUserProfile.FirstName}";
                var newUserWindow = SearchHelper.GetParentElementByName(
                    RootSession, $"Uživatel: <{user}>"
                    , $"Nepovedlo se najít okno detailu uzivatele {user}");
                Assert.NotNull(newUserWindow, "Okno pro nový účet se neotevřelo");

                var offlineCheckBox = newUserWindow.FindElementByName("Aktivní");
                Assert.NotNull(offlineCheckBox);

                if (bool.Parse(offlineCheckBox.GetAttribute("Value.Value"))) offlineCheckBox.Click();

                var createButton = SearchHelper.GetClickableElementByName(EsticonSession, "OK", "Nepovedlo se najít tlačítko pro vytvoření nového uživatele");
                createButton.Click();
            }
            var adminOkButton = SearchHelper.GetClickableElementByName(adminWindow, "OK", "Nenašlo se tlačítko OK u okna Administrátor");
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
            ScreenRecorder.StartRecording(TestContext.CurrentContext.Test.Name, "Základní funkce", Version, "AspeEsticon");
            Assert.NotNull(EsticonSession, "Esticon session byla prázdná");

            LoginHelper.TryLogin(EsticonSession, UserHelper.EsticonUser.Login, UserHelper.EsticonUser.Password);

            EsticonSession = Setup.ConnectToRunningProcess(RootSession, "AspeEsticon");

            var adminButton = SearchHelper.GetClickableElementByName(EsticonSession, "Admin", "Nepovedlo se najít tlačítko Admin");
            adminButton.Click();

            var adminWindow = (WindowsElement)SearchHelper.WaitForElementByName("Administrátor", EsticonSession, 5, 500);

            var uzivateleTab = SearchHelper.GetClickableElementByName(adminWindow, "Uživatelé", "Nepoedlo se najít tlačítko Uživatelé");
            uzivateleTab.Click();

            WindowsElement gridData = (WindowsElement)SearchHelper.FindElementByAccessibilityId("gcGrid", adminWindow);
            bool? isCreated = null;

            TestContext.WriteLine(SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).Count(e => e.GetAttribute("Value.Value") == UserHelper.NewUserProfile.Login));
            Parallel.For(0, SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).Count(), e =>
            {
                isCreated = SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).Count(f => f.GetAttribute("Value.Value") == UserHelper.NewUserProfile.Login) != 0;
            });
            //! Pokud se nenajde uživatel, tak se vytvoří, jinak otevře detail a zkontroluje stav offline
            if (isCreated == false)
            {
                var createNewUserButton = SearchHelper.FindElementsByAccessibilityId("BarButtonItemLink", adminWindow).FirstOrDefault(e => e.GetAttribute("HelpText") == "Nový záznam");
                Assert.NotNull(createNewUserButton, "FindElementByAccessibilityId se tlačítko Nový záznam");
                createNewUserButton.Click();

                var newUserWindow = SearchHelper.GetParentElementByName(
                    RootSession, $"Uživatel: < >"
                    , $"Nepovedlo se najít okno detailu nového uzivatele ");
                Assert.NotNull(newUserWindow, "Okno pro nový účet se neotevřelo");
                CreateUser(newUserWindow as WindowsElement, UserType.PasswordAuth, UserHelper.NewUserProfile);

                var createButton = SearchHelper.GetClickableElementByName(EsticonSession, "OK", "Problém s tlačítkem Vytvoření nového uživatele");
                createButton.Click();
            }
            else
            {
                var windowsUser = SearchHelper.FindElementsByAccessibilityId("RowData.Row.DataDto", gridData).First(e => e.GetAttribute("Value.Value") == UserHelper.NewUserProfile.Login);
                Assert.NotNull(windowsUser, "FindElementByAccessibilityId se tlačítko Nový záznam");
                MouseActionsHelper.DoubleClick(windowsUser);

                var user = $"{UserHelper.NewUserProfile.LastName} {UserHelper.NewUserProfile.FirstName}";
                var newUserWindow = SearchHelper.GetParentElementByName(
                    RootSession, $"Uživatel: <{user}>",
                    $"Nepovedlo se najít okno detailu uzivatele {user}");
                Assert.NotNull(newUserWindow, "Okno pro nový účet se neotevřelo");

                var offlineCheckBox = newUserWindow.FindElementByName("Aktivní");
                Assert.NotNull(offlineCheckBox);

                if (bool.Parse(offlineCheckBox.GetAttribute("Value.Value"))) offlineCheckBox.Click();

                var createButton = SearchHelper.GetClickableElementByName(EsticonSession, "OK", "Problém s tlačítkem Vytvoření nového uživatele");
                createButton.Click();
            }
            var adminOkButton = SearchHelper.GetClickableElementByName(adminWindow, "OK", "Problém s tlačítkem OK (ADMIN OKNO)");
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
            //EsticonSession.Quit();
            EsticonSession?.Dispose();


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
            AppiumWebElement odhlasitButton = SearchHelper.GetClickableElementByName(EsticonSession, "Odhlásit", "Nepovedlo se najít tlačítko Odhlásit");
            odhlasitButton.Click();
        }
        /// <summary>
        /// 
        /// </summary>
        private void WrongDataDialog()
        {
            var errorDialog = SearchHelper.GetParentElementByClassName(EsticonSession, "#32770", "");

            var errorText = SearchHelper.FindElementByAccessibilityId("65535", errorDialog);
            Assert.AreEqual(@"Přihlášení se nezdařilo.
Chybně zadané heslo nebo uživatelské jméno.", errorText.Text, "Byla vypsána jiná chybová hláška");

            var errorButton = SearchHelper.GetClickableElementByName(errorDialog, "OK", "Problém s tlačítkem v error dialogu");
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
            var errorButton = SearchHelper.GetClickableElementByAccessibilityId(errorWindow, "2", "Nepovedlo se najít tlačítko OK v ERROR DIALOGU");

            Assert.AreEqual(@"Tento účet byl deaktivován.
Kontaktujte administrátora.", errorText.Text);

            errorButton.Click();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newUserWindow"></param>
        /// <param name="userType"></param>
        /// <param name="profile"></param>
        private static void CreateUser(WindowsElement newUserWindow, UserType userType, UserProfile profile)
        {
            AppiumWebElement jmenoInput = null;
            AppiumWebElement prijmeniInput = null;
            AppiumWebElement emailInput = null;
            var passwordInputs = SearchHelper.GetClickableElementsByClassName(newUserWindow, "PasswordBoxEdit", "");

            var task = new Task(() =>
            {
                jmenoInput = newUserWindow.FindElementByXPath("//*[@Name='Jméno']/following-sibling::*");
                prijmeniInput = newUserWindow.FindElementByXPath("//*[@Name='Příjmení']/following-sibling::*");
                emailInput = newUserWindow.FindElementByXPath("//*[@Name='Email']/following-sibling::*");
            });
            task.Start();
            //Task.Run(() =>
            //{
            //});

            switch (userType)
            {
                case UserType.PasswordAuth:
                    var loginInput = SearchHelper.GetClickableElementByAccessibilityId(newUserWindow, "textEditLogin", "Nepovedlo se najít input pro Login");
                    loginInput.SendKeys(profile.Login);
                    Assert.AreEqual(profile.Login.Length, loginInput.Text.Length);


                    Assert.NotNull(passwordInputs, "Nenašly se elementy pro heslo");
                    var appiumWebElements = passwordInputs as AppiumWebElement[] ?? passwordInputs.ToArray();
                    for (var i = 0; i < appiumWebElements.Count(); i++)
                    {
                        appiumWebElements.ElementAt(i).SendKeys(UserHelper.NewUserProfile.Password);
                        Assert.AreEqual(UserHelper.NewUserProfile.Password.Length, appiumWebElements.ElementAt(i).Text.Length);
                    }
                    break;
                case UserType.WindowsAuth:
                    var pswdloginCheckbox = SearchHelper.GetClickableElementByName(newUserWindow, "povolit přihlášení jménem a heslem", "Nepovedlo se najít checkbox pro přihlášení heslem");
                    pswdloginCheckbox.Click();

                    var windowsLoginCheckbox = SearchHelper.GetClickableElementByName(newUserWindow, "povolit přihlášení Windows účtem", "Nepovedlo se najít checkbox pro windows přihlášení");

                    windowsLoginCheckbox.Click();

                    var windowsInput = newUserWindow.FindElementByClassName("ButtonEdit");
                    Assert.NotNull(windowsInput);
                    windowsInput.SendKeys(profile.Login.Replace(@"\", Keys.Alt + Keys.NumberPad9 + Keys.NumberPad2 + Keys.Alt));
                    Assert.AreEqual(profile.Login, windowsInput.Text);

                    break;
            }

            var kodInput = newUserWindow.FindElementByXPath("//*[@Name='Kód']/following-sibling::*");
            task.Wait();
            kodInput.SendKeys(profile.Kod);
            jmenoInput.SendKeys(profile.FirstName);
            prijmeniInput.SendKeys(profile.LastName);
            emailInput.SendKeys(profile.Email);

            var offlineCheckBox = SearchHelper.GetClickableElementByName(newUserWindow, "Aktivní", "Nepovedlo se najít checkbox pro checkbox Online/Offline (NOVÝ ÚČET)");
            offlineCheckBox.Click();
        }
    }
}
