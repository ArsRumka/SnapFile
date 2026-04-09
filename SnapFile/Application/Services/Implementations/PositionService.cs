using AutoMapper;
using SnapFile.Application.DTOs.PositionDTOs;
using SnapFile.Application.IRepositories;
using SnapFile.Application.Services.Interfaces;
using SnapFile.Domain.Entities;

namespace SnapFile.Application.Services.Implementations
{
    public class PositionService:IPositionService
    {
        private readonly IPositionRepository _repository;
        private readonly IMapper _mapper;

        public PositionService(IPositionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<PositionDto>> GetAllAsync()
        {
            var positions = await _repository.GetAllAsync();
            return _mapper.Map<List<PositionDto>>(positions);
        }

        public async Task<PositionDto> GetByIdAsync(int id)
        {
            var position = await _repository.GetByIdAsync(id);
            if (position == null)
                throw new KeyNotFoundException($"Position with id {id} not found");
            return _mapper.Map<PositionDto>(position);
        }

        public async Task<PositionDto> CreateAsync(PositionDto dto)
        {
            var position = _mapper.Map<Position>(dto);
            var created = await _repository.CreateAsync(position);
            return _mapper.Map<PositionDto>(created);
        }

        public async Task<PositionDto> UpdateAsync(UpdatePositionDto dto)
        {
            var position = _mapper.Map<Position>(dto);
            var updated = await _repository.UpdateAsync(position);
            return _mapper.Map<PositionDto>(updated);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _repository.DeleteById(id);
        }

        public async Task<List<PositionWithUsersDto>> GetPositionWithUsers()
        {
            var positions = await _repository.GetPositionWithUsersAsync();
            return _mapper.Map<List<PositionWithUsersDto>>(positions);
        }

    }
}
