using Microsoft.EntityFrameworkCore;
using SnapFile.Application.IRepositories;
using SnapFile.Domain.Entities;
using SnapFile.Infrastructure.Data;

namespace SnapFile.Infrastructure.Repositories
{
    public class RequestTypeRepository : IRequestTypeRepository
    {
        private readonly AppDbContext _context;

        public RequestTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<RequestType>> GetAll()
        {
            return await _context.RequestTypes
                .Include(rt => rt.Templates)
                .ToListAsync();
        }

        public async Task<RequestType> GetById(int id)
        {
            var result = await _context.RequestTypes
                .Include(rt => rt.Templates)
                .FirstOrDefaultAsync(rt => rt.Id == id);
            if (result == null)
                throw new InvalidOperationException("RequestType not found");
            return result;
        }

        public async Task<RequestType> Create(RequestType requestType)
        {
            _context.RequestTypes.Add(requestType);
            await _context.SaveChangesAsync();
            return requestType;
        }

        public async Task<RequestType> Update(RequestType requestType)
        {
            var existing = await _context.RequestTypes.FindAsync(requestType.Id);
            if (existing == null)
                throw new InvalidOperationException("RequestType not found");

            existing.Name = requestType.Name;

            await _context.SaveChangesAsync();
            return existing;
        }

        public void DeleteById(int id)
        {
            var entity = _context.RequestTypes.Find(id);
            if (entity == null)
                throw new InvalidOperationException("RequestType not found");

            _context.RequestTypes.Remove(entity);
            _context.SaveChanges();
        }
    }
}
