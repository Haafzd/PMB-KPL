using API.Models.StateMachine;

namespace API.Models
{
    public class Registration
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ReEnterPassword { get; set; }
        public RegistrationState State { get; set; } = RegistrationState.Unregistered;

        public Registration(string email, string NewPassword, string ReEnterPassword)
        {
            this.Email = email;
            this.NewPassword = NewPassword;
            this.ReEnterPassword = ReEnterPassword;
        }
    }
}