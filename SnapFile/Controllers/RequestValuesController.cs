using Microsoft.AspNetCore.Mvc;
using SnapFile.Application.DTOs.RequestValueDTOs;
using SnapFile.Application.Services.Interfaces;

namespace SnapFile.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestValuesController : ControllerBase
    {
        private readonly IRequestValueService _requestValueService;

        public RequestValuesController(IRequestValueService requestValueService)
        {
            _requestValueService = requestValueService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RequestValueDto>>> GetAll()
        {
            var values = await _requestValueService.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RequestValueDto>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            try
            {
                var value = await _requestValueService.GetByIdAsync(id);
                return Ok(value);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("by-request/{requestId}")]
        public async Task<ActionResult<List<RequestValueDto>>> GetByRequestId(int requestId)
        {
            if (requestId <= 0)
                return BadRequest(new { message = "ID заявления должен быть положительным числом" });

            try
            {
                var values = await _requestValueService.GetByRequestIdAsync(requestId);
                return Ok(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при получении значений", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<RequestValueDto>> Create([FromBody] RequestValueCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var value = await _requestValueService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = value.Id }, value);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при создании значения", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RequestValueDto>> Update(int id, [FromBody] RequestValueUpdateDto dto)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            if (dto.Id != id)
                return BadRequest(new { message = "ID в URL и теле запроса должны совпадать" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var value = await _requestValueService.UpdateAsync(dto);
                return Ok(value);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при обновлении значения", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            try
            {
                await _requestValueService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при удалении значения", error = ex.Message });
            }
        }
    }
}
