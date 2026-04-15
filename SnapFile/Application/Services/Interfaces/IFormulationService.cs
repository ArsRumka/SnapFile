using SnapFile.Application.DTOs.FormulationDTOs;

namespace SnapFile.Application.Services.Interfaces
{
    public interface IFormulationService
    {
        Task<List<FormulationDto>> GetAllAsync();
        Task<FormulationDto> GetByIdAsync(int id);
        Task<FormulationDto> CreateAsync(FormulationCreateDto dto);
        Task<FormulationDto> UpdateAsync(FormulationUpdateDto dto);
        Task DeleteByIdAsync(int id);
    }
}
