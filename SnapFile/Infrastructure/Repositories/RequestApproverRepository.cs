using Microsoft.EntityFrameworkCore;
using SnapFile.Application.IRepositories;
using SnapFile.Domain.Entities;
using SnapFile.Infrastructure.Data;

namespace SnapFile.Infrastructure.Repositories
{
    public class RequestApproverRepository : IRequestApproverRepository
    {
        private readonly AppDbContext _context;

        public RequestApproverRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<RequestApprover>> GetAll()
        {
            return await _context.RequestApprovers
                .Include(ra => ra.Request)
                .Include(ra => ra.User)
                .OrderBy(ra => ra.Order)
                .ToListAsync();
        }

        public async Task<RequestApprover> GetById(int id)
        {
            var result = await _context.RequestApprovers
                .Include(ra => ra.Request)
                .Include(ra => ra.User)
                .FirstOrDefaultAsync(ra => ra.Id == id);
            if (result == null)
                throw new InvalidOperationException("RequestApprover not found");
            return result;
        }

        public async Task<List<RequestApprover>> GetByRequestId(int requestId)
        {
            return await _context.RequestApprovers
                .Include(ra => ra.Request)
                .Include(ra => ra.User)
                .Where(ra => ra.RequestId == requestId)
                .OrderBy(ra => ra.Order)
                .ToListAsync();
        }

        public async Task<RequestApprover> Create(RequestApprover requestApprover)
        {
            _context.RequestApprovers.Add(requestApprover);
            await _context.SaveChangesAsync();
            return requestApprover;
        }

        public async Task<RequestApprover> Update(RequestApprover requestApprover)
        {
            var existing = await _context.RequestApprovers.FindAsync(requestApprover.Id);
            if (existing == null)
                throw new InvalidOperationException("RequestApprover not found");

            existing.UserId = requestApprover.UserId;
            existing.Order = requestApprover.Order;
            existing.DecisionDate = requestApprover.DecisionDate;

            await _context.SaveChangesAsync();
            return existing;
        }

        public void DeleteById(int id)
        {
            var entity = _context.RequestApprovers.Find(id);
            if (entity == null)
                throw new InvalidOperationException("RequestApprover not found");

            _context.RequestApprovers.Remove(entity);
            _context.SaveChanges();
        }
    }
}
