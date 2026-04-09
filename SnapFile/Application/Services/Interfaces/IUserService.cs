using SnapFile.Application.DTOs.UserDTOs;

namespace SnapFile.Application.Services.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserDto>> GetAllAsync();
    }
}
