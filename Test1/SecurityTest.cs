using API.Models;
using API.Services;
using API.Models.StateMachine;

namespace PMB.Tests
{
    [TestClass]
    public class LoginStateMachineTests
    {
        private User CreateTestUser(string email, string password)
        {
            return new User
            {
              Email = email,
                PasswordHashWithSalt = PasswordHasher.HashPassword(password)
            };
        }
        [TestMethod]
        public void ProvideCredentials_CorrectLogin_Authenticated()
        {
            var user = CreateTestUser("ucup", "password");
            var fsm = new LoginStateMachine(user);

            bool result = fsm.ProvideCredentials("ucup", "password");

            Assert.IsTrue(result);
            Assert.AreEqual(LoginState.Authenticated, fsm.CurrentState);
        }
    }
}
