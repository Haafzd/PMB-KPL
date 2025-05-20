using PMB.Models;
using PMB.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB.StateMachine
{
    public class LoginStateMachine
    {
        public LoginState CurrentState { get; private set; }
        private readonly User _user;
        private int _failedAttempts;
        private const int MaxAttempts = 3;

        public LoginStateMachine(User user)
        {
            _user = user ?? throw new ArgumentNullException("User tidak valid");
            CurrentState = LoginState.NotLoggedIn;
        }

        // Proses validasi kredensial
        public bool ProvideCredentials(string email, string password)
        {
            if (CurrentState == LoginState.Locked)
                throw new InvalidOperationException("Akun terkunci");

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                throw new ArgumentException("Email dan password harus diisi");

            CurrentState = LoginState.WaitingCredentials;

            if (_user.Email == email && PasswordHasher.VerifyPassword(password, _user.PasswordHashWithSalt))
            {
                CurrentState = LoginState.Authenticated;
                return true;
            }

            _failedAttempts++;
            CurrentState = _failedAttempts >= MaxAttempts ? LoginState.Locked : LoginState.Failed;

            return false;
        }
    }
}
