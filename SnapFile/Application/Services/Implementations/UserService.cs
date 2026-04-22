using AutoMapper;
using SnapFile.Application.DTOs.UserDTOs;
using SnapFile.Application.IRepositories;
using SnapFile.Application.Services.Interfaces;
using SnapFile.Domain.Entities;

namespace SnapFile.Application.Services.Implementations
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> SetAdminStatusAsync(int id, UpdateUserAdminDto dto)
        {
            var user = await _repository.GetByIdAsync(id);
            user.IsAdmin = dto.IsAdmin;

            var updated = await _repository.UpdateAsync(user);
            return _mapper.Map<UserDto>(updated);
        }

        public async Task<UserDto> UpdateProfileAsync(int id, UpdateUserProfileDto dto)
        {
            var user = new User
            {
                Id = id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                PositionId = dto.PositionId,
                DepartmentId = dto.DepartmentId,
                Phone = dto.Phone
            };

            var updated = await _repository.UpdateProfileAsync(user);
            return _mapper.Map<UserDto>(updated);
        }
    }
}
