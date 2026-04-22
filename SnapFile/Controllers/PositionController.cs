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
        public async Task<ActionResult<List<PositionDto>>> GetAll()
        {
            var positions = await _service.GetAllAsync();
            return Ok(positions);
        }

        [HttpGet("with-users")]
        public async Task<ActionResult<List<PositionWithUsersDto>>> GetAllWithUsers()
        {
            var positions = await _service.GetPositionWithUsers();
            return Ok(positions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PositionDto>> GetById(int id)
        {
            try
            {
                var position = await _service.GetByIdAsync(id);
                return Ok(position);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<PositionDto>> Create([FromBody] PositionCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PositionDto>> Update(int id, [FromBody] UpdatePositionDto dto)
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
        public async Task<IActionResult> Delete(int id)
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
