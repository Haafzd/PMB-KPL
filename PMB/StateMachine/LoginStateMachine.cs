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
        public LoginState CurrentState { get; private set; } = LoginState.NotLoggedIn;
        private readonly User _user;
        private int failedAttempts = 0;
        private const int MaxAttempts = 3;

        public LoginStateMachine(User user)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
        }

        public bool ProvideCredentials(string username, string password)
        {
            if (CurrentState == LoginState.Locked)
                throw new InvalidOperationException("User is locked.");

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new ArgumentException("Username or password is empty.");

            CurrentState = LoginState.WaitingCredentials;

            if (_user.Username == username &&
                PasswordHasher.VerifyPassword(password, _user.PasswordHashWithSalt))
            {
                CurrentState = LoginState.Authenticated;
                return true;
            }

            failedAttempts++;
            if (failedAttempts >= MaxAttempts)
            {
                CurrentState = LoginState.Locked;
            }
            else
            {
                CurrentState = LoginState.Failed;
            }

            return false;
        }
    }

}
