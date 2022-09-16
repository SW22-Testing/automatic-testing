using automatic_web_testing.Classes;

namespace automatic_web_testing.Helper.Predefined
{
    public abstract class UserHelper
    {
        /// <summary>
        /// Údaje pro místní přihlášení ale správně zadané.
        /// </summary>
        public static readonly UserProfile CorrectUser = new UserProfile("Testerprohub@protonmail.com", "*ics=VwB–^7_)N?");
        /// <summary>
        /// Údaje pro místní přihlášení ale špatně zadané.
        /// </summary>
        public static readonly UserProfile IncorrectUser = new UserProfile("Testrprhub@protonmail.com", "*ics=V–^7_)N?");
        /// <summary>
        /// Údaje pro přihlášení do Microsoftu
        /// </summary>
        public static readonly UserProfile MicrosoftUser = new UserProfile("Testerprohub@protonmail.com", "ics=VwB7N");
        /// <summary>
        /// Údaje pro přihlášení do Googlu
        /// </summary>
        public static readonly UserProfile GoogleUser = new UserProfile("Testerprohub@gmail.com", "ics=VwB7N");
    }
}
