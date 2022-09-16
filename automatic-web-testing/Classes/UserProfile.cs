namespace automatic_web_testing.Classes
{
    public class UserProfile
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public UserProfile(string email, string password)
        {
            Email = email;
            Password = password;

        }
    }
}
