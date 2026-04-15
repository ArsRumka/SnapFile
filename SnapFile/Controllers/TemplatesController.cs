using Microsoft.AspNetCore.Mvc;
using SnapFile.Application.DTOs.TemplateDTOs;
using SnapFile.Application.Services.Interfaces;

namespace SnapFile.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemplatesController : ControllerBase
    {
        private readonly ITemplateService _templateService;

        public TemplatesController(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TemplateDto>>> GetAll()
        {
            var templates = await _templateService.GetAllAsync();
            return Ok(templates);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateDto>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            try
            {
                var template = await _templateService.GetByIdAsync(id);
                return Ok(template);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("by-request-type/{requestTypeId}")]
        public async Task<ActionResult<List<TemplateDto>>> GetByRequestTypeId(int requestTypeId)
        {
            if (requestTypeId <= 0)
                return BadRequest(new { message = "ID типа заявления должен быть положительным числом" });

            try
            {
                var templates = await _templateService.GetByRequestTypeIdAsync(requestTypeId);
                return Ok(templates);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при получении шаблонов", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<TemplateDto>> Create([FromBody] TemplateCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var template = await _templateService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = template.Id }, template);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при создании шаблона", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TemplateDto>> Update(int id, [FromBody] TemplateUpdateDto dto)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            if (dto.Id != id)
                return BadRequest(new { message = "ID в URL и теле запроса должны совпадать" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var template = await _templateService.UpdateAsync(dto);
                return Ok(template);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при обновлении шаблона", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            try
            {
                await _templateService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при удалении шаблона", error = ex.Message });
            }
        }
    }
}
