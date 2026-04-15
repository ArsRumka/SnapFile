using AutoMapper;
using SnapFile.Application.DTOs.RequestDTOs;
using SnapFile.Application.IRepositories;
using SnapFile.Application.Services.Interfaces;
using SnapFile.Domain.Entities;

namespace SnapFile.Application.Services.Implementations
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _repository;
        private readonly IMapper _mapper;

        public RequestService(IRequestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<RequestDto>> GetAllAsync()
        {
            var requests = await _repository.GetAll();
            return _mapper.Map<List<RequestDto>>(requests);
        }

        public async Task<RequestDto> GetByIdAsync(int id)
        {
            var request = await _repository.GetById(id);
            if (request == null)
                throw new KeyNotFoundException($"Request with id {id} not found");

            return _mapper.Map<RequestDto>(request);
        }

        public async Task<List<RequestDto>> GetByCreatedByUserIdAsync(int userId)
        {
            var requests = await _repository.GetByCreatedByUserId(userId);
            return _mapper.Map<List<RequestDto>>(requests);
        }

        public async Task<List<RequestDto>> GetByStatusAsync(string status)
        {
            var requests = await _repository.GetByStatus(status);
            return _mapper.Map<List<RequestDto>>(requests);
        }

        public async Task<RequestDto> CreateAsync(RequestCreateDto dto)
        {
            var request = _mapper.Map<Request>(dto);
            request.CreatedAt = DateTime.UtcNow;
            request.Status = "Draft";

            var created = await _repository.Create(request);
            return _mapper.Map<RequestDto>(created);
        }

        public async Task<RequestDto> UpdateAsync(RequestUpdateDto dto)
        {
            var request = _mapper.Map<Request>(dto);
            var updated = await _repository.Update(request);
            return _mapper.Map<RequestDto>(updated);
        }

        public async Task<RequestDto> UpdateStatusAsync(int id, string status)
        {
            var request = await _repository.GetById(id);
            if (request == null)
                throw new KeyNotFoundException($"Request with id {id} not found");

            request.Status = status;
            var updated = await _repository.Update(request);
            return _mapper.Map<RequestDto>(updated);
        }

        public async Task DeleteByIdAsync(int id)
        {
            _repository.DeleteById(id);
            await Task.CompletedTask;
        }
    }
}
