using AutoMapper;
using SnapFile.Application.DTOs.TemplateApproverDTOs;
using SnapFile.Application.IRepositories;
using SnapFile.Application.Services.Interfaces;
using SnapFile.Domain.Entities;

namespace SnapFile.Application.Services.Implementations
{
    public class TemplateApproverService : ITemplateApproverService
    {
        private readonly ITemplateApproverRepository _repository;
        private readonly IMapper _mapper;

        public TemplateApproverService(ITemplateApproverRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<TemplateApproverDto>> GetAllAsync()
        {
            var approvers = await _repository.GetAll();
            return _mapper.Map<List<TemplateApproverDto>>(approvers);
        }

        public async Task<TemplateApproverDto> GetByIdAsync(int id)
        {
            var approver = await _repository.GetById(id);
            if (approver == null)
                throw new KeyNotFoundException($"TemplateApprover with id {id} not found");

            return _mapper.Map<TemplateApproverDto>(approver);
        }

        public async Task<List<TemplateApproverDto>> GetByTemplateIdAsync(int templateId)
        {
            var approvers = await _repository.GetByTemplateId(templateId);
            return _mapper.Map<List<TemplateApproverDto>>(approvers);
        }

        public async Task<TemplateApproverDto> CreateAsync(TemplateApproverCreateDto dto)
        {
            var approver = _mapper.Map<TemplateApprover>(dto);
            var created = await _repository.Create(approver);
            return _mapper.Map<TemplateApproverDto>(created);
        }

        public async Task<TemplateApproverDto> UpdateAsync(TemplateApproverUpdateDto dto)
        {
            var approver = _mapper.Map<TemplateApprover>(dto);
            var updated = await _repository.Update(approver);
            return _mapper.Map<TemplateApproverDto>(updated);
        }

        public async Task DeleteByIdAsync(int id)
        {
            _repository.DeleteById(id);
            await Task.CompletedTask;
        }
    }
}
