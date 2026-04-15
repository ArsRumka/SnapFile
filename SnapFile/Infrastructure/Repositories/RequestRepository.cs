using Microsoft.EntityFrameworkCore;
using SnapFile.Application.IRepositories;
using SnapFile.Domain.Entities;
using SnapFile.Infrastructure.Data;

namespace SnapFile.Infrastructure.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly AppDbContext _context;

        public RequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Request>> GetAll()
        {
            return await _context.Requests
                .Include(r => r.Template)
                .Include(r => r.CreatedByUser)
                .Include(r => r.RecipientUser)
                .Include(r => r.RecipientDepartment)
                .Include(r => r.Formulation)
                .Include(r => r.Values)
                .Include(r => r.Approvers)
                .ToListAsync();
        }

        public async Task<Request> GetById(int id)
        {
            var result = await _context.Requests
                .Include(r => r.Template)
                .Include(r => r.CreatedByUser)
                .Include(r => r.RecipientUser)
                .Include(r => r.RecipientDepartment)
                .Include(r => r.Formulation)
                .Include(r => r.Values)
                .Include(r => r.Approvers)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (result == null)
                throw new InvalidOperationException("Request not found");
            return result;
        }

        public async Task<List<Request>> GetByCreatedByUserId(int userId)
        {
            return await _context.Requests
                .Include(r => r.Template)
                .Include(r => r.CreatedByUser)
                .Include(r => r.RecipientUser)
                .Include(r => r.RecipientDepartment)
                .Include(r => r.Formulation)
                .Include(r => r.Values)
                .Include(r => r.Approvers)
                .Where(r => r.CreatedByUserId == userId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Request>> GetByStatus(string status)
        {
            return await _context.Requests
                .Include(r => r.Template)
                .Include(r => r.CreatedByUser)
                .Include(r => r.RecipientUser)
                .Include(r => r.RecipientDepartment)
                .Include(r => r.Formulation)
                .Include(r => r.Values)
                .Include(r => r.Approvers)
                .Where(r => r.Status == status)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<Request> Create(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<Request> Update(Request request)
        {
            var existing = await _context.Requests.FindAsync(request.Id);
            if (existing == null)
                throw new InvalidOperationException("Request not found");

            existing.TemplateId = request.TemplateId;
            existing.RecipientType = request.RecipientType;
            existing.RecipientUserId = request.RecipientUserId;
            existing.RecipientName = request.RecipientName;
            existing.RecipientPosition = request.RecipientPosition;
            existing.RecipientDepartmentId = request.RecipientDepartmentId;
            existing.Status = request.Status;
            existing.FormulationId = request.FormulationId;

            await _context.SaveChangesAsync();
            return existing;
        }

        public void DeleteById(int id)
        {
            var entity = _context.Requests.Find(id);
            if (entity == null)
                throw new InvalidOperationException("Request not found");

            _context.Requests.Remove(entity);
            _context.SaveChanges();
        }
    }
}
