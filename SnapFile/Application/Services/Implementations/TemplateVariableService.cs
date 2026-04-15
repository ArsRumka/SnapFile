using AutoMapper;
using SnapFile.Application.DTOs.TemplateVariableDTOs;
using SnapFile.Application.IRepositories;
using SnapFile.Application.Services.Interfaces;
using SnapFile.Domain.Entities;

namespace SnapFile.Application.Services.Implementations
{
    public class TemplateVariableService : ITemplateVariableService
    {
        private readonly ITemplateVariableRepository _repository;
        private readonly IMapper _mapper;

        public TemplateVariableService(ITemplateVariableRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<TemplateVariableDto>> GetAllAsync()
        {
            var variables = await _repository.GetAll();
            return _mapper.Map<List<TemplateVariableDto>>(variables);
        }

        public async Task<TemplateVariableDto> GetByIdAsync(int id)
        {
            var variable = await _repository.GetById(id);
            if (variable == null)
                throw new KeyNotFoundException($"TemplateVariable with id {id} not found");

            return _mapper.Map<TemplateVariableDto>(variable);
        }

        public async Task<List<TemplateVariableDto>> GetByTemplateIdAsync(int templateId)
        {
            var variables = await _repository.GetByTemplateId(templateId);
            return _mapper.Map<List<TemplateVariableDto>>(variables);
        }

        public async Task<TemplateVariableDto> CreateAsync(TemplateVariableCreateDto dto)
        {
            var variable = _mapper.Map<TemplateVariable>(dto);
            var created = await _repository.Create(variable);
            return _mapper.Map<TemplateVariableDto>(created);
        }

        public async Task<TemplateVariableDto> UpdateAsync(TemplateVariableUpdateDto dto)
        {
            var variable = _mapper.Map<TemplateVariable>(dto);
            var updated = await _repository.Update(variable);
            return _mapper.Map<TemplateVariableDto>(updated);
        }

        public async Task DeleteByIdAsync(int id)
        {
            _repository.DeleteById(id);
            await Task.CompletedTask;
        }
    }
}
