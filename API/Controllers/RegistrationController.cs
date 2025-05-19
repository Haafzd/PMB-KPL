// Controllers/RegistrationController.cs
using Microsoft.AspNetCore.Mvc;
using API;
using System.Collections.Generic;
using System.Linq;
using PMB.Models;

namespace API
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly List<User> _users;

        public RegistrationController(List<User> users)
        {
            _users = users;
        }

        [HttpPost]
        public IActionResult Register([FromBody] Registration registration)
        {
            if (registration.NewPassword != registration.ReEnterPassword)
            {
                return BadRequest("Passwords do not match.");
            }
            if (_users.Any(u => u.Email == registration.email))
            {
                return BadRequest("Email already registered.");
            }

            // Transition state
            registration.State = RegistrationState.Registered;

            var user = new User
            {
                Email = registration.email,
                PasswordHashWithSalt = registration.NewPassword
            };

            _users.Add(user);

            return Ok("Registration successful.");
        }
    }
}