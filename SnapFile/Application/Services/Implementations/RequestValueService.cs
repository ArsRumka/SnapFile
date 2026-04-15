using AutoMapper;
using SnapFile.Application.DTOs.RequestValueDTOs;
using SnapFile.Application.IRepositories;
using SnapFile.Application.Services.Interfaces;
using SnapFile.Domain.Entities;

namespace SnapFile.Application.Services.Implementations
{
    public class RequestValueService : IRequestValueService
    {
        private readonly IRequestValueRepository _repository;
        private readonly IMapper _mapper;

        public RequestValueService(IRequestValueRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<RequestValueDto>> GetAllAsync()
        {
            var values = await _repository.GetAll();
            return _mapper.Map<List<RequestValueDto>>(values);
        }

        public async Task<RequestValueDto> GetByIdAsync(int id)
        {
            var value = await _repository.GetById(id);
            if (value == null)
                throw new KeyNotFoundException($"RequestValue with id {id} not found");

            return _mapper.Map<RequestValueDto>(value);
        }

        public async Task<List<RequestValueDto>> GetByRequestIdAsync(int requestId)
        {
            var values = await _repository.GetByRequestId(requestId);
            return _mapper.Map<List<RequestValueDto>>(values);
        }

        public async Task<RequestValueDto> CreateAsync(RequestValueCreateDto dto)
        {
            var value = _mapper.Map<RequestValue>(dto);
            var created = await _repository.Create(value);
            return _mapper.Map<RequestValueDto>(created);
        }

        public async Task<RequestValueDto> UpdateAsync(RequestValueUpdateDto dto)
        {
            var value = _mapper.Map<RequestValue>(dto);
            var updated = await _repository.Update(value);
            return _mapper.Map<RequestValueDto>(updated);
        }

        public async Task DeleteByIdAsync(int id)
        {
            _repository.DeleteById(id);
            await Task.CompletedTask;
        }
    }
}
