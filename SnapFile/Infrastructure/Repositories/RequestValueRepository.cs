using Microsoft.EntityFrameworkCore;
using SnapFile.Application.IRepositories;
using SnapFile.Domain.Entities;
using SnapFile.Infrastructure.Data;

namespace SnapFile.Infrastructure.Repositories
{
    public class RequestValueRepository : IRequestValueRepository
    {
        private readonly AppDbContext _context;

        public RequestValueRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<RequestValue>> GetAll()
        {
            return await _context.RequestValues
                .Include(rv => rv.Request)
                .Include(rv => rv.TemplateVariable)
                .ToListAsync();
        }

        public async Task<RequestValue> GetById(int id)
        {
            var result = await _context.RequestValues
                .Include(rv => rv.Request)
                .Include(rv => rv.TemplateVariable)
                .FirstOrDefaultAsync(rv => rv.Id == id);
            if (result == null)
                throw new InvalidOperationException("RequestValue not found");
            return result;
        }

        public async Task<List<RequestValue>> GetByRequestId(int requestId)
        {
            return await _context.RequestValues
                .Include(rv => rv.Request)
                .Include(rv => rv.TemplateVariable)
                .Where(rv => rv.RequestId == requestId)
                .ToListAsync();
        }

        public async Task<RequestValue> Create(RequestValue requestValue)
        {
            _context.RequestValues.Add(requestValue);
            await _context.SaveChangesAsync();
            return requestValue;
        }

        public async Task<RequestValue> Update(RequestValue requestValue)
        {
            var existing = await _context.RequestValues.FindAsync(requestValue.Id);
            if (existing == null)
                throw new InvalidOperationException("RequestValue not found");

            existing.Value = requestValue.Value;

            await _context.SaveChangesAsync();
            return existing;
        }

        public void DeleteById(int id)
        {
            var entity = _context.RequestValues.Find(id);
            if (entity == null)
                throw new InvalidOperationException("RequestValue not found");

            _context.RequestValues.Remove(entity);
            _context.SaveChanges();
        }
    }
}
