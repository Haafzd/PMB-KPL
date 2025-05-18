using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMB.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using API;

namespace Test1
{
    [TestClass]
    public class RegistrationControllerTests
    {
        private RegistrationController _controller;
        private List<User> _users;

        [TestInitialize]
        public void Setup()
        {
            _users = new List<User>();
            _controller = new RegistrationController(_users);
        }

        [TestMethod]
        public void Register_WithMatchingPasswords_ReturnsOk()
        {
            var registration = new Registration(
                "Test User",
                "test@example.com",
                "1234567890",
                "123 Test St",
                "2000-01-01",
                "password123",
                "password123"
            );

            var result = _controller.Register(registration);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual("Registration successful.", okResult.Value);
            Assert.AreEqual(1, _users.Count);
        }

        [TestMethod]
        public void Register_WithNonMatchingPasswords_ReturnsBadRequest()
        {
            var registration = new Registration(
                "Test User",
                "test@example.com",
                "1234567890",
                "123 Test St",
                "2000-01-01",
                "password123",
                "passwordBeda"
            );

            var result = _controller.Register(registration);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual("Passwords do not match.", badRequestResult.Value);
            Assert.AreEqual(0, _users.Count);
        }

        [TestMethod]
        public void Register_WithExistingEmail_ReturnsBadRequest()
        {
            var existingEmail = "existing@example.com";
            _users.Add(new User { Email = existingEmail });

            var registration = new Registration(
                "Test User",
                existingEmail,
                "1234567890",
                "123 Test St",
                "2000-01-01",
                "password123",
                "password123"
            );

            var result = _controller.Register(registration);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual("Email already registered.", badRequestResult.Value);
            Assert.AreEqual(1, _users.Count);
        }

        [TestMethod]
        public void Register_ValidRequest_SavesUserToList()
        {
            var registration = new Registration(
                "Test User",
                "test@example.com",
                "1234567890",
                "123 Test St",
                "2000-01-01",
                "password123",
                "password123"
            );

            var result = _controller.Register(registration);

            Assert.AreEqual(1, _users.Count);
            var savedUser = _users.First();
            Assert.AreEqual(registration.email, savedUser.Email);
            Assert.AreEqual(registration.NewPassword, savedUser.PasswordHashWithSalt);
        }

        [TestMethod]
        public void Register_ValidRequest_ChangesStateToRegistered()
        {
            var registration = new Registration(
                "Test User",
                "test@example.com",
                "1234567890",
                "123 Test St",
                "2000-01-01",
                "password123",
                "password123"
            );

            var result = _controller.Register(registration);

            Assert.AreEqual(RegistrationState.Registered, registration.State);
        }
    }
}
