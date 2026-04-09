using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnapFile.Application.Auth.DTOs;
using SnapFile.Infrastructure.Services;

namespace SnapFile.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;

        public AuthController(AuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            await _auth.Register(dto);
            return Ok();
        }

        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin(RegisterDto dto)
        {
            await _auth.RegisterAdmin(dto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _auth.Login(dto);

            return Ok(new
            {
                token
            });
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var user = new
            {
                Id = User.FindFirst("id")?.Value,
                FullName = User.FindFirst("fullName")?.Value,
                Role = User.FindFirst("role")?.Value
            };

            return Ok(user);
        }
    }
}
