using SnapFile.Domain.Entities;

namespace SnapFile.Application.IRepositories
{
    public interface IPositionRepository
    {
        public Task<List<Position>> GetAllAsync();
        public Task<Position> GetByIdAsync(int id);
        public Task<List<Position>> GetPositionWithUsersAsync();
        public Task<Position> CreateAsync(Position position);
        public Task<Position> UpdateAsync(Position position);
        public Task DeleteById(int id);
    }
}
