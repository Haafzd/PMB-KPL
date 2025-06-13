using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;
using API.Models.StateMachine;
using Lib.Utils;
using Microsoft.AspNetCore.Identity;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly UserService _userService;

        public RegistrationController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Register([FromBody] Registration registration)
        {
            if (string.IsNullOrWhiteSpace(registration.Email) ||
                string.IsNullOrWhiteSpace(registration.NewPassword) ||
                string.IsNullOrWhiteSpace(registration.ReEnterPassword))
                return BadRequest("Semua field wajib diisi.");

            if (!InputHelper.ValidateEmail(registration.Email))
                return BadRequest("Format email tidak valid.");

            if (registration.NewPassword != registration.ReEnterPassword)
                return BadRequest("Password tidak sama.");

            if (_userService.Users.Any(u => u.Email == registration.Email))
                return BadRequest("Email sudah terdaftar.");

            var user = new User
            {
                Email = registration.Email,
                PasswordHashWithSalt = PasswordHasher.HashPassword(registration.NewPassword)
            };

            _userService.AddUser(user);
            registration.State = RegistrationState.Registered;

            return Ok("Registrasi berhasil.");
        }
    }
}
