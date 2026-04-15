using SnapFile.Application.DTOs.TemplateApproverDTOs;

namespace SnapFile.Application.Services.Interfaces
{
    public interface ITemplateApproverService
    {
        Task<List<TemplateApproverDto>> GetAllAsync();
        Task<TemplateApproverDto> GetByIdAsync(int id);
        Task<List<TemplateApproverDto>> GetByTemplateIdAsync(int templateId);
        Task<TemplateApproverDto> CreateAsync(TemplateApproverCreateDto dto);
        Task<TemplateApproverDto> UpdateAsync(TemplateApproverUpdateDto dto);
        Task DeleteByIdAsync(int id);
    }
}
