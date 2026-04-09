using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnapFile.Application.DTOs.PositionDTOs;
using SnapFile.Application.Services.Interfaces;

namespace SnapFile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _service;
        public PositionController(IPositionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PositionDto>>> GetAllAsync()
        {
            var positions = await _service.GetAllAsync();
            return Ok(positions);
        }

        [HttpGet("with-users")]
        public async Task<ActionResult<List<PositionWithUsersDto>>> GetAllWithUsersAsync()
        {
            var positions = await _service.GetPositionWithUsers();
            return Ok(positions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PositionDto>> GetByIdAsync(int id)
        {
            try
            {
                var position = await _service.GetByIdAsync(id);
                return Ok(position);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] PositionDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PositionDto>> UpdateAsync(int id, [FromBody] UpdatePositionDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");
            try
            {
                var updated = await _service.UpdateAsync(dto);
                return Ok(updated);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            try
            {
                await _service.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
    }
}
