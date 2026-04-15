using AutoMapper;
using SnapFile.Application.DTOs.TemplateDTOs;
using SnapFile.Application.IRepositories;
using SnapFile.Application.Services.Interfaces;
using SnapFile.Domain.Entities;

namespace SnapFile.Application.Services.Implementations
{
    public class TemplateService : ITemplateService
    {
        private readonly ITemplateRepository _repository;
        private readonly IMapper _mapper;

        public TemplateService(ITemplateRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<TemplateDto>> GetAllAsync()
        {
            var templates = await _repository.GetAll();
            return _mapper.Map<List<TemplateDto>>(templates);
        }

        public async Task<TemplateDto> GetByIdAsync(int id)
        {
            var template = await _repository.GetById(id);
            if (template == null)
                throw new KeyNotFoundException($"Template with id {id} not found");

            return _mapper.Map<TemplateDto>(template);
        }

        public async Task<List<TemplateDto>> GetByRequestTypeIdAsync(int requestTypeId)
        {
            var templates = await _repository.GetByRequestTypeId(requestTypeId);
            return _mapper.Map<List<TemplateDto>>(templates);
        }

        public async Task<TemplateDto> CreateAsync(TemplateCreateDto dto)
        {
            var template = _mapper.Map<Template>(dto);
            var created = await _repository.Create(template);
            return _mapper.Map<TemplateDto>(created);
        }

        public async Task<TemplateDto> UpdateAsync(TemplateUpdateDto dto)
        {
            var template = _mapper.Map<Template>(dto);
            var updated = await _repository.Update(template);
            return _mapper.Map<TemplateDto>(updated);
        }

        public async Task DeleteByIdAsync(int id)
        {
            _repository.DeleteById(id);
            await Task.CompletedTask;
        }
    }
}
