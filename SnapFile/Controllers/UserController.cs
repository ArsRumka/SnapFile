using Microsoft.AspNetCore.Http;
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
    }
}
