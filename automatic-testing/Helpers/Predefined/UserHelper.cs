using automatic_testing.Classes;
using System;

namespace automatic_testing.Helpers.Predefined
{
    public class UserHelper
    {
        /// <summary>
        /// Správné údaje pro přihlášení s Admin oprávněním v AspeEsticon
        /// </summary>
        public static UserProfile EsticonUser = new UserProfile("esti", "esti", true);
        /// <summary>
        /// Neexistující účet pro přihlášení v AspeEsticon
        /// </summary>
        public static UserProfile WrongUserProfile = new UserProfile("špatný údaj", "špatný údaj");
        /// <summary>
        /// Účet, se kterým budou hlavně pracovat automatické testy
        /// </summary>
        public static UserProfile AutoUserProfile = new UserProfile("Automatický účet", "Heslo automatického účtu", "autoKod", "Automatický účet", "Přijmení", "auto@auto.test");
        /// <summary>
        /// Účet, kterým se kontroluje založení nového uživatele
        /// </summary>
        public static UserProfile NewUserProfile = new UserProfile("Nový uživatel", "Heslo nového uživatele", "novyKod", "Nový účet", "Přijmení", "novy@novy.test");
        public static UserProfile WindowsUser = new UserProfile($@"{Environment.UserDomainName}\{Environment.UserName}", "", Environment.UserName, "", "", "");
    }
}
