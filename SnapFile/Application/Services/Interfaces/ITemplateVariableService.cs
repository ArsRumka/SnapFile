using SnapFile.Application.DTOs.TemplateVariableDTOs;

namespace SnapFile.Application.Services.Interfaces
{
    public interface ITemplateVariableService
    {
        Task<List<TemplateVariableDto>> GetAllAsync();
        Task<TemplateVariableDto> GetByIdAsync(int id);
        Task<List<TemplateVariableDto>> GetByTemplateIdAsync(int templateId);
        Task<TemplateVariableDto> CreateAsync(TemplateVariableCreateDto dto);
        Task<TemplateVariableDto> UpdateAsync(TemplateVariableUpdateDto dto);
        Task DeleteByIdAsync(int id);
    }
}
