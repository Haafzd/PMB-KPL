using Microsoft.AspNetCore.Mvc;
using API.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userService.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
                return BadRequest("Email belum terdaftar.");

            if (PasswordHasher.VerifyPassword(request.Password, user.PasswordHashWithSalt))
                return Ok("Login berhasil.");

            return BadRequest("Password salah.");
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
