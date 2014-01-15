namespace TinderApp.Lib
{
    public class AuthResponse
    {
        public Globals globals { get; set; }

        public string token { get; set; }

        public User user { get; set; }

        public Versions versions { get; set; }
    }
}