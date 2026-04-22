using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SnapFile.Application.DTOs.UserDTOs;
using SnapFile.Application.Services.Interfaces;

namespace SnapFile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            var users = await _service.GetAllAsync();
            return Ok(users);
        }

        [Authorize]
        [HttpPut("me")]
        public async Task<ActionResult<UserDto>> UpdateMyProfile([FromBody] UpdateUserProfileDto dto)
        {
            var idClaim = User.FindFirst("id")?.Value;

            if (!int.TryParse(idClaim, out var userId))
                return Unauthorized();

            try
            {
                var user = await _service.UpdateProfileAsync(userId, dto);
                return Ok(user);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPatch("{id}/admin")]
        public async Task<ActionResult<UserDto>> SetAdminStatus(int id, [FromBody] UpdateUserAdminDto dto)
        {
            try
            {
                var user = await _service.SetAdminStatusAsync(id, dto);
                return Ok(user);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
    }
}
