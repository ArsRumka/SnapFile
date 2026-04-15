using Microsoft.EntityFrameworkCore;
using SnapFile.Application.IRepositories;
using SnapFile.Domain.Entities;
using SnapFile.Infrastructure.Data;

namespace SnapFile.Infrastructure.Repositories
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly AppDbContext _context;

        public TemplateRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Template>> GetAll()
        {
            return await _context.Templates
                .Include(t => t.RequestType)
                .Include(t => t.Formulation)
                .Include(t => t.Variables)
                .Include(t => t.Approvers)
                .ToListAsync();
        }

        public async Task<Template> GetById(int id)
        {
            var result = await _context.Templates
                .Include(t => t.RequestType)
                .Include(t => t.Formulation)
                .Include(t => t.Variables)
                .Include(t => t.Approvers)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (result == null)
                throw new InvalidOperationException("Template not found");
            return result;
        }

        public async Task<List<Template>> GetByRequestTypeId(int requestTypeId)
        {
            return await _context.Templates
                .Include(t => t.RequestType)
                .Include(t => t.Formulation)
                .Include(t => t.Variables)
                .Include(t => t.Approvers)
                .Where(t => t.RequestTypeId == requestTypeId)
                .ToListAsync();
        }

        public async Task<Template> Create(Template template)
        {
            _context.Templates.Add(template);
            await _context.SaveChangesAsync();
            return template;
        }

        public async Task<Template> Update(Template template)
        {
            var existing = await _context.Templates.FindAsync(template.Id);
            if (existing == null)
                throw new InvalidOperationException("Template not found");

            existing.Name = template.Name;
            existing.RequestTypeId = template.RequestTypeId;
            existing.HtmlContent = template.HtmlContent;
            existing.FormulationId = template.FormulationId;

            await _context.SaveChangesAsync();
            return existing;
        }

        public void DeleteById(int id)
        {
            var entity = _context.Templates.Find(id);
            if (entity == null)
                throw new InvalidOperationException("Template not found");

            _context.Templates.Remove(entity);
            _context.SaveChanges();
        }
    }
}
