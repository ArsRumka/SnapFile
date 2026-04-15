using SnapFile.Application.DTOs.RequestDTOs;

namespace SnapFile.Application.Services.Interfaces
{
    public interface IRequestService
    {
        Task<List<RequestDto>> GetAllAsync();
        Task<RequestDto> GetByIdAsync(int id);
        Task<List<RequestDto>> GetByCreatedByUserIdAsync(int userId);
        Task<List<RequestDto>> GetByStatusAsync(string status);
        Task<RequestDto> CreateAsync(RequestCreateDto dto);
        Task<RequestDto> UpdateAsync(RequestUpdateDto dto);
        Task<RequestDto> UpdateStatusAsync(int id, string status);
        Task DeleteByIdAsync(int id);
    }
}
