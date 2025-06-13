namespace API.Models
{
    public class User
    {
        public string Email { get; set; }
        public string PasswordHashWithSalt { get; set; }
    }
}
