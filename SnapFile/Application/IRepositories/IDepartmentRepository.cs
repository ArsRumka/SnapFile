using SnapFile.Domain.Entities;

namespace SnapFile.Application.IRepositories
{
    public interface IDepartmentRepository
    {
        public Task<List<Department>> GetAll();
        public Task<Department> GetById(int id);

        public Task<Department> Create(Department department);
        public Task<Department> Update(Department department);
        public void DeleteById(int id);

    }
}
