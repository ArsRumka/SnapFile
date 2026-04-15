using Microsoft.AspNetCore.Mvc;
using SnapFile.Application.DTOs.FormulationDTOs;
using SnapFile.Application.Services.Interfaces;

namespace SnapFile.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormulationsController : ControllerBase
    {
        private readonly IFormulationService _formulationService;

        public FormulationsController(IFormulationService formulationService)
        {
            _formulationService = formulationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<FormulationDto>>> GetAll()
        {
            var formulations = await _formulationService.GetAllAsync();
            return Ok(formulations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FormulationDto>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            try
            {
                var formulation = await _formulationService.GetByIdAsync(id);
                return Ok(formulation);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<FormulationDto>> Create([FromBody] FormulationCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var formulation = await _formulationService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = formulation.Id }, formulation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при создании формулировки", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FormulationDto>> Update(int id, [FromBody] FormulationUpdateDto dto)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            if (dto.Id != id)
                return BadRequest(new { message = "ID в URL и теле запроса должны совпадать" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var formulation = await _formulationService.UpdateAsync(dto);
                return Ok(formulation);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при обновлении формулировки", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "ID должен быть положительным числом" });

            try
            {
                await _formulationService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при удалении формулировки", error = ex.Message });
            }
        }
    }
}
