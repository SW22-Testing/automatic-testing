namespace automatic_testing.Classes
{
    public class UserProfile
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Kod { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public UserProfile(string login, string password, string kod, string firstName, string lastName, string email, bool isAdmin = false)
        {
            Login = login;
            Password = password;
            Kod = kod;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            IsAdmin = isAdmin;
        }
        public UserProfile(string login, string password)
        {
            Login = login;
            Password = password;
        }
        public UserProfile(string login, string password, bool isAdmin)
        {
            Login = login;
            Password = password;
            IsAdmin = isAdmin;
        }
    }
}
