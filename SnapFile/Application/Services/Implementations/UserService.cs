using AutoMapper;
using SnapFile.Application.DTOs.UserDTOs;
using SnapFile.Application.IRepositories;
using SnapFile.Application.Services.Interfaces;

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
    }
}
