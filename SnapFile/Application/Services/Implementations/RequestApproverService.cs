using AutoMapper;
using SnapFile.Application.DTOs.RequestApproverDTOs;
using SnapFile.Application.IRepositories;
using SnapFile.Application.Services.Interfaces;
using SnapFile.Domain.Entities;

namespace SnapFile.Application.Services.Implementations
{
    public class RequestApproverService : IRequestApproverService
    {
        private readonly IRequestApproverRepository _repository;
        private readonly IMapper _mapper;

        public RequestApproverService(IRequestApproverRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<RequestApproverDto>> GetAllAsync()
        {
            var approvers = await _repository.GetAll();
            return _mapper.Map<List<RequestApproverDto>>(approvers);
        }

        public async Task<RequestApproverDto> GetByIdAsync(int id)
        {
            var approver = await _repository.GetById(id);
            if (approver == null)
                throw new KeyNotFoundException($"RequestApprover with id {id} not found");

            return _mapper.Map<RequestApproverDto>(approver);
        }

        public async Task<List<RequestApproverDto>> GetByRequestIdAsync(int requestId)
        {
            var approvers = await _repository.GetByRequestId(requestId);
            return _mapper.Map<List<RequestApproverDto>>(approvers);
        }

        public async Task<RequestApproverDto> CreateAsync(RequestApproverCreateDto dto)
        {
            var approver = _mapper.Map<RequestApprover>(dto);
            var created = await _repository.Create(approver);
            return _mapper.Map<RequestApproverDto>(created);
        }

        public async Task<RequestApproverDto> UpdateAsync(RequestApproverUpdateDto dto)
        {
            var approver = _mapper.Map<RequestApprover>(dto);
            var updated = await _repository.Update(approver);
            return _mapper.Map<RequestApproverDto>(updated);
        }

        public async Task DeleteByIdAsync(int id)
        {
            _repository.DeleteById(id);
            await Task.CompletedTask;
        }
    }
}
