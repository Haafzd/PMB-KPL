using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.Controllers;
using API.Models.StateMachine;
using API.Services;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace Test1
{
    [TestClass]
    public class RegistrationControllerTests
    {
        private RegistrationController _controller;
        private UserService _users;

        [TestInitialize]
        public void Setup()
        {
            _users = new UserService();
            _controller = new RegistrationController(_users);
        }

        [TestMethod]
        public void Register_WithMatchingPasswords_ReturnsOk()
        {
            var registration = new Registration(
                "test@example.com",
                "password123",
                "password123"
            );

            var result = _controller.Register(registration);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual("Registration successful.", okResult.Value);
            Assert.AreEqual(1, _users.Users.Count());
        }

        [TestMethod]
        public void Register_WithNonMatchingPasswords_ReturnsBadRequest()
        {
            var registration = new Registration(
                "test@example.com",
                "password123",
                "passwordBeda"
            );

            var result = _controller.Register(registration);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual("Passwords do not match.", badRequestResult.Value);
            Assert.AreEqual(0, _users.Users.Count());

        }

        [TestMethod]
        public void Register_WithExistingEmail_ReturnsBadRequest()
        {
            var existingEmail = "existing@example.com";
            _users.AddUser(new User { Email = existingEmail });

            var registration = new Registration(
                existingEmail,
                "password123",
                "password123"
            );

            var result = _controller.Register(registration);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual("Email already registered.", badRequestResult.Value);
            Assert.AreEqual(1, _users.Users.Count());
        }

        [TestMethod]
        public void Register_ValidRequest_SavesUserToList()
        {
            var registration = new Registration(
                "test@example.com",
                "password123",
                "password123"
            );

            var result = _controller.Register(registration);

            Assert.AreEqual(1, _users.Users.Count());
            var savedUser = _users.Users.First();
            Assert.AreEqual(registration.Email, savedUser.Email);
            Assert.AreEqual(registration.NewPassword, savedUser.PasswordHashWithSalt);
        }

        [TestMethod]
        public void Register_ValidRequest_ChangesStateToRegistered()
        {
            var registration = new Registration(
                "test@example.com",
                "password123",
                "password123"
            );

            var result = _controller.Register(registration);

            Assert.AreEqual(RegistrationState.Registered, registration.State);
        }
    }
}
