using SnapFile.Application.DTOs.RequestValueDTOs;

namespace SnapFile.Application.Services.Interfaces
{
    public interface IRequestValueService
    {
        Task<List<RequestValueDto>> GetAllAsync();
        Task<RequestValueDto> GetByIdAsync(int id);
        Task<List<RequestValueDto>> GetByRequestIdAsync(int requestId);
        Task<RequestValueDto> CreateAsync(RequestValueCreateDto dto);
        Task<RequestValueDto> UpdateAsync(RequestValueUpdateDto dto);
        Task DeleteByIdAsync(int id);
    }
}
