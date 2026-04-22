using SnapFile.Application.DTOs.UserDTOs;

namespace SnapFile.Application.Services.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserDto>> GetAllAsync();
        public Task<UserDto> SetAdminStatusAsync(int id, UpdateUserAdminDto dto);
        public Task<UserDto> UpdateProfileAsync(int id, UpdateUserProfileDto dto);
    }
}
