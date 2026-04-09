using SnapFile.Application.DTOs.DepartmentDTOs;
using SnapFile.Domain.Entities;

namespace SnapFile.Application.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetAllAsync();
        Task<DepartmentDto> GetByIdAsync(int id);
        Task<DepartmentDto> CreateAsync(DepartmentCreateDto departmentDto);
        Task<DepartmentDto> UpdateAsync(DepartmentUpdateDto departmentDto);
        Task DeleteByIdAsync(int id);
    }
}