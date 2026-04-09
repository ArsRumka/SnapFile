using Microsoft.EntityFrameworkCore;
using SnapFile.Application.IRepositories;
using SnapFile.Domain.Entities;
using SnapFile.Infrastructure.Data;
namespace SnapFile.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetAll()
        {
            return await _context.Departments
                .Include(d => d.Head) // подгружаем главу отдела
                .ToListAsync();
        }

        public async Task<Department> GetById(int id)
        {
            var result = await _context.Departments
                .Include(d => d.Head)
                .FirstOrDefaultAsync(d => d.Id == id);
            if (result == null)
                throw new InvalidOperationException("Department not found");
            return result;
        }

        public async Task<Department> Create(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Department> Update(Department department)
        {
            var existing = await _context.Departments.FindAsync(department.Id);
            if (existing == null)
                throw new InvalidOperationException("Department not found");

            existing.Name = department.Name;
            existing.HeadId = department.HeadId;

            await _context.SaveChangesAsync();
            return existing;
        }

        public void DeleteById(int id)
        {
            var entity = _context.Departments.Find(id);
            if (entity == null)
                throw new InvalidOperationException("Department not found");

            _context.Departments.Remove(entity);
            _context.SaveChanges();
        }
    }
}