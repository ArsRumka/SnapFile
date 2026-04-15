using SnapFile.Application.DTOs.TemplateDTOs;

namespace SnapFile.Application.Services.Interfaces
{
    public interface ITemplateService
    {
        Task<List<TemplateDto>> GetAllAsync();
        Task<TemplateDto> GetByIdAsync(int id);
        Task<List<TemplateDto>> GetByRequestTypeIdAsync(int requestTypeId);
        Task<TemplateDto> CreateAsync(TemplateCreateDto dto);
        Task<TemplateDto> UpdateAsync(TemplateUpdateDto dto);
        Task DeleteByIdAsync(int id);
    }
}
