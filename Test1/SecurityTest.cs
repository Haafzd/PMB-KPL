using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMB.Models;
using PMB.Services;
using PMB.Security;
using PMB.StateMachine;
using System;
using System.Collections.Generic;

namespace PMB.Tests
{
    [TestClass]
    public class LoginStateMachineTests
    {
        private User CreateTestUser(string username, string password)
        {
            return new User
            {
                Username = username,
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
