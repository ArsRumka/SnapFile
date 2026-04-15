using Microsoft.AspNetCore.Mvc;
using SnapFile.Application.DTOs.TemplateVariableDTOs;
using SnapFile.Application.Services.Interfaces;

namespace SnapFile.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemplateVariablesController : ControllerBase
    {
        private readonly ITemplateVariableService _templateVariableService;

        public TemplateVariablesController(ITemplateVariableService templateVariableService)
        {
            _templateVariableService = templateVariableService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TemplateVariableDto>>> GetAll()
        {
            var variables = await _templateVariableService.GetAllAsync();
            return Ok(variables);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateVariableDto>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            try
            {
                var variable = await _templateVariableService.GetByIdAsync(id);
                return Ok(variable);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("by-template/{templateId}")]
        public async Task<ActionResult<List<TemplateVariableDto>>> GetByTemplateId(int templateId)
        {
            if (templateId <= 0)
                return BadRequest(new { message = "ID шаблона должен быть положительным числом" });

            try
            {
                var variables = await _templateVariableService.GetByTemplateIdAsync(templateId);
                return Ok(variables);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при получении переменных", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<TemplateVariableDto>> Create([FromBody] TemplateVariableCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var variable = await _templateVariableService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = variable.Id }, variable);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при создании переменной", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TemplateVariableDto>> Update(int id, [FromBody] TemplateVariableUpdateDto dto)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            if (dto.Id != id)
                return BadRequest(new { message = "ID в URL и теле запроса должны совпадать" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var variable = await _templateVariableService.UpdateAsync(dto);
                return Ok(variable);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при обновлении переменной", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            try
            {
                await _templateVariableService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при удалении переменной", error = ex.Message });
            }
        }
    }
}
