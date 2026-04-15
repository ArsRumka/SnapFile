using Microsoft.EntityFrameworkCore;
using SnapFile.Application.IRepositories;
using SnapFile.Domain.Entities;
using SnapFile.Infrastructure.Data;

namespace SnapFile.Infrastructure.Repositories
{
    public class TemplateApproverRepository : ITemplateApproverRepository
    {
        private readonly AppDbContext _context;

        public TemplateApproverRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TemplateApprover>> GetAll()
        {
            return await _context.TemplateApprovers
                .Include(ta => ta.Template)
                .Include(ta => ta.User)
                .OrderBy(ta => ta.Order)
                .ToListAsync();
        }

        public async Task<TemplateApprover> GetById(int id)
        {
            var result = await _context.TemplateApprovers
                .Include(ta => ta.Template)
                .Include(ta => ta.User)
                .FirstOrDefaultAsync(ta => ta.Id == id);
            if (result == null)
                throw new InvalidOperationException("TemplateApprover not found");
            return result;
        }

        public async Task<List<TemplateApprover>> GetByTemplateId(int templateId)
        {
            return await _context.TemplateApprovers
                .Include(ta => ta.Template)
                .Include(ta => ta.User)
                .Where(ta => ta.TemplateId == templateId)
                .OrderBy(ta => ta.Order)
                .ToListAsync();
        }

        public async Task<TemplateApprover> Create(TemplateApprover templateApprover)
        {
            _context.TemplateApprovers.Add(templateApprover);
            await _context.SaveChangesAsync();
            return templateApprover;
        }

        public async Task<TemplateApprover> Update(TemplateApprover templateApprover)
        {
            var existing = await _context.TemplateApprovers.FindAsync(templateApprover.Id);
            if (existing == null)
                throw new InvalidOperationException("TemplateApprover not found");

            existing.UserId = templateApprover.UserId;
            existing.Order = templateApprover.Order;

            await _context.SaveChangesAsync();
            return existing;
        }

        public void DeleteById(int id)
        {
            var entity = _context.TemplateApprovers.Find(id);
            if (entity == null)
                throw new InvalidOperationException("TemplateApprover not found");

            _context.TemplateApprovers.Remove(entity);
            _context.SaveChanges();
        }
    }
}
