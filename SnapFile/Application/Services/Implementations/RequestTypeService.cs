using AutoMapper;
using SnapFile.Application.DTOs.RequestTypeDTOs;
using SnapFile.Application.IRepositories;
using SnapFile.Application.Services.Interfaces;
using SnapFile.Domain.Entities;

namespace SnapFile.Application.Services.Implementations
{
    public class RequestTypeService : IRequestTypeService
    {
        private readonly IRequestTypeRepository _repository;
        private readonly IMapper _mapper;

        public RequestTypeService(IRequestTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<RequestTypeDto>> GetAllAsync()
        {
            var requestTypes = await _repository.GetAll();
            return _mapper.Map<List<RequestTypeDto>>(requestTypes);
        }

        public async Task<RequestTypeDto> GetByIdAsync(int id)
        {
            var requestType = await _repository.GetById(id);
            if (requestType == null)
                throw new KeyNotFoundException($"RequestType with id {id} not found");

            return _mapper.Map<RequestTypeDto>(requestType);
        }

        public async Task<RequestTypeDto> CreateAsync(RequestTypeCreateDto dto)
        {
            var requestType = _mapper.Map<RequestType>(dto);
            var created = await _repository.Create(requestType);
            return _mapper.Map<RequestTypeDto>(created);
        }

        public async Task<RequestTypeDto> UpdateAsync(RequestTypeUpdateDto dto)
        {
            var requestType = _mapper.Map<RequestType>(dto);
            var updated = await _repository.Update(requestType);
            return _mapper.Map<RequestTypeDto>(updated);
        }

        public async Task DeleteByIdAsync(int id)
        {
            _repository.DeleteById(id);
            await Task.CompletedTask;
        }
    }
}
