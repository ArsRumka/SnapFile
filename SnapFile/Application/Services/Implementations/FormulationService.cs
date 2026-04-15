using AutoMapper;
using SnapFile.Application.DTOs.FormulationDTOs;
using SnapFile.Application.IRepositories;
using SnapFile.Application.Services.Interfaces;
using SnapFile.Domain.Entities;

namespace SnapFile.Application.Services.Implementations
{
    public class FormulationService : IFormulationService
    {
        private readonly IFormulationRepository _repository;
        private readonly IMapper _mapper;

        public FormulationService(IFormulationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FormulationDto>> GetAllAsync()
        {
            var formulations = await _repository.GetAll();
            return _mapper.Map<List<FormulationDto>>(formulations);
        }

        public async Task<FormulationDto> GetByIdAsync(int id)
        {
            var formulation = await _repository.GetById(id);
            if (formulation == null)
                throw new KeyNotFoundException($"Formulation with id {id} not found");

            return _mapper.Map<FormulationDto>(formulation);
        }

        public async Task<FormulationDto> CreateAsync(FormulationCreateDto dto)
        {
            var formulation = _mapper.Map<Formulation>(dto);
            var created = await _repository.Create(formulation);
            return _mapper.Map<FormulationDto>(created);
        }

        public async Task<FormulationDto> UpdateAsync(FormulationUpdateDto dto)
        {
            var formulation = _mapper.Map<Formulation>(dto);
            var updated = await _repository.Update(formulation);
            return _mapper.Map<FormulationDto>(updated);
        }

        public async Task DeleteByIdAsync(int id)
        {
            _repository.DeleteById(id);
            await Task.CompletedTask;
        }
    }
}
