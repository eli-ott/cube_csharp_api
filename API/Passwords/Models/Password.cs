namespace MonApi.API.Passwords.Models
{
    public class Password
    {
        public string PasswordId { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public int NumberTries { get; set; }
    }
}
