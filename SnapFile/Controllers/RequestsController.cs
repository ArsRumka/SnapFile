using Microsoft.AspNetCore.Mvc;
using SnapFile.Application.DTOs.RequestDTOs;
using SnapFile.Application.Services.Interfaces;

namespace SnapFile.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestsController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RequestDto>>> GetAll()
        {
            var requests = await _requestService.GetAllAsync();
            return Ok(requests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RequestDto>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            try
            {
                var request = await _requestService.GetByIdAsync(id);
                return Ok(request);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<List<RequestDto>>> GetByCreatedByUserId(int userId)
        {
            if (userId <= 0)
                return BadRequest(new { message = "ID пользователя должен быть положительным числом" });

            try
            {
                var requests = await _requestService.GetByCreatedByUserIdAsync(userId);
                return Ok(requests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при получении заявлений", error = ex.Message });
            }
        }

        [HttpGet("by-status/{status}")]
        public async Task<ActionResult<List<RequestDto>>> GetByStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return BadRequest(new { message = "Статус не может быть пустым" });

            var validStatuses = new[] { "Draft", "Submitted", "InApproval", "Approved", "Rejected", "Cancelled" };
            if (!validStatuses.Contains(status))
                return BadRequest(new { message = $"Недопустимый статус. Допустимые: {string.Join(", ", validStatuses)}" });

            try
            {
                var requests = await _requestService.GetByStatusAsync(status);
                return Ok(requests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при получении заявлений", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<RequestDto>> Create([FromBody] RequestCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var request = await _requestService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при создании заявления", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RequestDto>> Update(int id, [FromBody] RequestUpdateDto dto)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            if (dto.Id != id)
                return BadRequest(new { message = "ID в URL и теле запроса должны совпадать" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var request = await _requestService.UpdateAsync(dto);
                return Ok(request);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при обновлении заявления", error = ex.Message });
            }
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult<RequestDto>> UpdateStatus(int id, [FromBody] UpdateStatusDto updateStatusDto)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            if (string.IsNullOrWhiteSpace(updateStatusDto?.Status))
                return BadRequest(new { message = "Статус не может быть пустым" });

            var validStatuses = new[] { "Draft", "Submitted", "InApproval", "Approved", "Rejected", "Cancelled" };
            if (!validStatuses.Contains(updateStatusDto.Status))
                return BadRequest(new { message = $"Недопустимый статус. Допустимые: {string.Join(", ", validStatuses)}" });

            try
            {
                var request = await _requestService.UpdateStatusAsync(id, updateStatusDto.Status);
                return Ok(request);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при обновлении статуса", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            try
            {
                await _requestService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при удалении заявления", error = ex.Message });
            }
        }
    }

    public class UpdateStatusDto
    {
        public string Status { get; set; }
    }
}
