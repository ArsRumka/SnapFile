using Microsoft.EntityFrameworkCore;
using SnapFile.Application.IRepositories;
using SnapFile.Domain.Entities;
using SnapFile.Infrastructure.Data;

namespace SnapFile.Infrastructure.Repositories
{
    public class TemplateVariableRepository : ITemplateVariableRepository
    {
        private readonly AppDbContext _context;

        public TemplateVariableRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TemplateVariable>> GetAll()
        {
            return await _context.TemplateVariables
                .Include(tv => tv.Template)
                .ToListAsync();
        }

        public async Task<TemplateVariable> GetById(int id)
        {
            var result = await _context.TemplateVariables
                .Include(tv => tv.Template)
                .FirstOrDefaultAsync(tv => tv.Id == id);
            if (result == null)
                throw new InvalidOperationException("TemplateVariable not found");
            return result;
        }

        public async Task<List<TemplateVariable>> GetByTemplateId(int templateId)
        {
            return await _context.TemplateVariables
                .Include(tv => tv.Template)
                .Where(tv => tv.TemplateId == templateId)
                .ToListAsync();
        }

        public async Task<TemplateVariable> Create(TemplateVariable templateVariable)
        {
            _context.TemplateVariables.Add(templateVariable);
            await _context.SaveChangesAsync();
            return templateVariable;
        }

        public async Task<TemplateVariable> Update(TemplateVariable templateVariable)
        {
            var existing = await _context.TemplateVariables.FindAsync(templateVariable.Id);
            if (existing == null)
                throw new InvalidOperationException("TemplateVariable not found");

            existing.Name = templateVariable.Name;
            existing.Type = templateVariable.Type;
            existing.IsRequired = templateVariable.IsRequired;

            await _context.SaveChangesAsync();
            return existing;
        }

        public void DeleteById(int id)
        {
            var entity = _context.TemplateVariables.Find(id);
            if (entity == null)
                throw new InvalidOperationException("TemplateVariable not found");

            _context.TemplateVariables.Remove(entity);
            _context.SaveChanges();
        }
    }
}
