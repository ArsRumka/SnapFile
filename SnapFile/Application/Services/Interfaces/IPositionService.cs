using SnapFile.Application.DTOs.PositionDTOs;

namespace SnapFile.Application.Services.Interfaces
{
    public interface IPositionService
    {
        public Task<List<PositionDto>> GetAllAsync();
        public Task<PositionDto> GetByIdAsync(int id);
        public Task<List<PositionWithUsersDto>> GetPositionWithUsers();
        public Task<PositionDto> CreateAsync(PositionCreateDto positionDto);
        public Task<PositionDto> UpdateAsync(UpdatePositionDto updatePositionDto);
        public Task DeleteByIdAsync(int id);

    }
}
