using SnapFile.Domain.Entities;

namespace SnapFile.Application.IRepositories
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllAsync();
        public Task<User> GetByIdAsync(int id);
        public Task<User> UpdateAsync(User user);
        public Task<User> UpdateProfileAsync(User user);
    }
}
