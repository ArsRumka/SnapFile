using Microsoft.EntityFrameworkCore;
using SnapFile.Application.IRepositories;
using SnapFile.Domain.Entities;
using SnapFile.Infrastructure.Data;

namespace SnapFile.Infrastructure.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly AppDbContext _context;

        public PositionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Position>> GetAllAsync()
        {
            return await _context.Positions.ToListAsync();
        }

        public async Task<Position> GetByIdAsync(int id)
        {
            var result = await _context.Positions.FindAsync(id);
            if (result == null)
                throw new InvalidOperationException("Position not found");
            return result;
        }

        public async Task<List<Position>> GetPositionWithUsersAsync()
        {
            return await _context.Positions
                .Include(p => p.Users)
                .ThenInclude(u => u.Department)
                .ToListAsync();
        }

        public async Task<Position> CreateAsync(Position position)
        {
            _context.Positions.Add(position);
            await _context.SaveChangesAsync();
            return position;
        }

        public async Task<Position> UpdateAsync(Position position)
        {
            var existing = await _context.Positions.FindAsync(position.Id);
            if (existing == null)
                throw new InvalidOperationException("Position not found");
            existing.Name = position.Name;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task DeleteById(int id)
        {
            var entity = await _context.Positions.FindAsync(id);
            if (entity == null)
                throw new InvalidOperationException("Position not found");
            _context.Positions.Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
