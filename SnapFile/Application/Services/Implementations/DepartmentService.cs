using AutoMapper;
using SnapFile.Application.DTOs.DepartmentDTOs;
using SnapFile.Application.IRepositories;
using SnapFile.Application.Services.Interfaces;
using SnapFile.Domain.Entities;

namespace SnapFile.Application.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<DepartmentDto>> GetAllAsync()
        {
            var departments = await _repository.GetAll();
            return _mapper.Map<List<DepartmentDto>>(departments);
        }

        public async Task<DepartmentDto> GetByIdAsync(int id)
        {
            var department = await _repository.GetById(id);
            if (department == null)
                throw new KeyNotFoundException($"Department with id {id} not found");

            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task<DepartmentDto> CreateAsync(DepartmentCreateDto dto)
        {
            var department = _mapper.Map<Department>(dto);
            var created = await _repository.Create(department);
            return _mapper.Map<DepartmentDto>(created);
        }

        public async Task<DepartmentDto> UpdateAsync(DepartmentUpdateDto dto)
        {
            var department = _mapper.Map<Department>(dto);
            var updated = await _repository.Update(department);
            return _mapper.Map<DepartmentDto>(updated);
        }

        public async Task DeleteByIdAsync(int id)
        {
            _repository.DeleteById(id);
            await Task.CompletedTask;
        }
    }
}
