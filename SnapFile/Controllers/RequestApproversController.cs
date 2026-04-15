using Microsoft.AspNetCore.Mvc;
using SnapFile.Application.DTOs.RequestApproverDTOs;
using SnapFile.Application.Services.Interfaces;

namespace SnapFile.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestApproversController : ControllerBase
    {
        private readonly IRequestApproverService _requestApproverService;

        public RequestApproversController(IRequestApproverService requestApproverService)
        {
            _requestApproverService = requestApproverService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RequestApproverDto>>> GetAll()
        {
            var approvers = await _requestApproverService.GetAllAsync();
            return Ok(approvers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RequestApproverDto>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            try
            {
                var approver = await _requestApproverService.GetByIdAsync(id);
                return Ok(approver);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("by-request/{requestId}")]
        public async Task<ActionResult<List<RequestApproverDto>>> GetByRequestId(int requestId)
        {
            if (requestId <= 0)
                return BadRequest(new { message = "ID заявления должен быть положительным числом" });

            try
            {
                var approvers = await _requestApproverService.GetByRequestIdAsync(requestId);
                return Ok(approvers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при получении одобрителей", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<RequestApproverDto>> Create([FromBody] RequestApproverCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var approver = await _requestApproverService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = approver.Id }, approver);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при создании одобрителя", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RequestApproverDto>> Update(int id, [FromBody] RequestApproverUpdateDto dto)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            if (dto.Id != id)
                return BadRequest(new { message = "ID в URL и теле запроса должны совпадать" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var approver = await _requestApproverService.UpdateAsync(dto);
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
                await _requestApproverService.DeleteByIdAsync(id);
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
