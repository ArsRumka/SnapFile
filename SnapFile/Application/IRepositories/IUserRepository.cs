using SnapFile.Domain.Entities;

namespace SnapFile.Application.IRepositories
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllAsync();
    }
}
