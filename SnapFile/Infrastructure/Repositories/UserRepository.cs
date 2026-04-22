using Microsoft.EntityFrameworkCore;
using SnapFile.Application.IRepositories;
using SnapFile.Domain.Entities;
using SnapFile.Infrastructure.Data;

namespace SnapFile.Infrastructure.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Position)
                .Include(u => u.Department)
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _context.Users
                .Include(u => u.Position)
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                throw new InvalidOperationException("User not found");

            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            var existing = await _context.Users
                .Include(u => u.Position)
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            if (existing == null)
                throw new InvalidOperationException("User not found");

            existing.IsAdmin = user.IsAdmin;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<User> UpdateProfileAsync(User user)
        {
            var existing = await _context.Users
                .Include(u => u.Position)
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            if (existing == null)
                throw new InvalidOperationException("User not found");

            existing.FirstName = user.FirstName;
            existing.LastName = user.LastName;
            existing.MiddleName = user.MiddleName;
            existing.PositionId = user.PositionId;
            existing.DepartmentId = user.DepartmentId;
            existing.Phone = user.Phone;

            await _context.SaveChangesAsync();

            await _context.Entry(existing).Reference(u => u.Position).LoadAsync();
            await _context.Entry(existing).Reference(u => u.Department).LoadAsync();

            return existing;
        }
    }
}
