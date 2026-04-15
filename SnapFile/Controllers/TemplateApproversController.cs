using Microsoft.AspNetCore.Mvc;
using SnapFile.Application.DTOs.TemplateApproverDTOs;
using SnapFile.Application.Services.Interfaces;

namespace SnapFile.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemplateApproversController : ControllerBase
    {
        private readonly ITemplateApproverService _templateApproverService;

        public TemplateApproversController(ITemplateApproverService templateApproverService)
        {
            _templateApproverService = templateApproverService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TemplateApproverDto>>> GetAll()
        {
            var approvers = await _templateApproverService.GetAllAsync();
            return Ok(approvers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateApproverDto>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            try
            {
                var approver = await _templateApproverService.GetByIdAsync(id);
                return Ok(approver);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("by-template/{templateId}")]
        public async Task<ActionResult<List<TemplateApproverDto>>> GetByTemplateId(int templateId)
        {
            if (templateId <= 0)
                return BadRequest(new { message = "ID шаблона должен быть положительным числом" });

            try
            {
                var approvers = await _templateApproverService.GetByTemplateIdAsync(templateId);
                return Ok(approvers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при получении одобрителей", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<TemplateApproverDto>> Create([FromBody] TemplateApproverCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var approver = await _templateApproverService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = approver.Id }, approver);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при создании одобрителя", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TemplateApproverDto>> Update(int id, [FromBody] TemplateApproverUpdateDto dto)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            if (dto.Id != id)
                return BadRequest(new { message = "ID в URL и теле запроса должны совпадать" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var approver = await _templateApproverService.UpdateAsync(dto);
                return Ok(approver);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при обновлении одобрителя", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            try
            {
                await _templateApproverService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при удалении одобрителя", error = ex.Message });
            }
        }
    }
}
