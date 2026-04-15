using SnapFile.Application.DTOs.RequestApproverDTOs;

namespace SnapFile.Application.Services.Interfaces
{
    public interface IRequestApproverService
    {
        Task<List<RequestApproverDto>> GetAllAsync();
        Task<RequestApproverDto> GetByIdAsync(int id);
        Task<List<RequestApproverDto>> GetByRequestIdAsync(int requestId);
        Task<RequestApproverDto> CreateAsync(RequestApproverCreateDto dto);
        Task<RequestApproverDto> UpdateAsync(RequestApproverUpdateDto dto);
        Task DeleteByIdAsync(int id);
    }
}
