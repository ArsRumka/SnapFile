using SnapFile.Application.DTOs.RequestTypeDTOs;

namespace SnapFile.Application.Services.Interfaces
{
    public interface IRequestTypeService
    {
        Task<List<RequestTypeDto>> GetAllAsync();
        Task<RequestTypeDto> GetByIdAsync(int id);
        Task<RequestTypeDto> CreateAsync(RequestTypeCreateDto dto);
        Task<RequestTypeDto> UpdateAsync(RequestTypeUpdateDto dto);
        Task DeleteByIdAsync(int id);
    }
}
