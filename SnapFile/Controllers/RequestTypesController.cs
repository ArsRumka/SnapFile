using Microsoft.AspNetCore.Mvc;
using SnapFile.Application.DTOs.RequestTypeDTOs;
using SnapFile.Application.Services.Interfaces;

namespace SnapFile.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestTypesController : ControllerBase
    {
        private readonly IRequestTypeService _requestTypeService;

        public RequestTypesController(IRequestTypeService requestTypeService)
        {
            _requestTypeService = requestTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RequestTypeDto>>> GetAll()
        {
            var requestTypes = await _requestTypeService.GetAllAsync();
            return Ok(requestTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RequestTypeDto>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            try
            {
                var requestType = await _requestTypeService.GetByIdAsync(id);
                return Ok(requestType);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<RequestTypeDto>> Create([FromBody] RequestTypeCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var requestType = await _requestTypeService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = requestType.Id }, requestType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при создании типа заявления", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RequestTypeDto>> Update(int id, [FromBody] RequestTypeUpdateDto dto)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            if (dto.Id != id)
                return BadRequest(new { message = "ID в URL и теле запроса должны совпадать" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var requestType = await _requestTypeService.UpdateAsync(dto);
                return Ok(requestType);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при обновлении типа заявления", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            try
            {
                await _requestTypeService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при удалении типа заявления", error = ex.Message });
            }
        }
    }
}
