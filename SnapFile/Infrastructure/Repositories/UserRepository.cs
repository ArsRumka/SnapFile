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
    }
}
