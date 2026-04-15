using SnapFile.Domain.Entities;

namespace SnapFile.Application.IRepositories
{
    public interface IRequestValueRepository
    {
        Task<List<RequestValue>> GetAll();
        Task<RequestValue> GetById(int id);
        Task<List<RequestValue>> GetByRequestId(int requestId);
        Task<RequestValue> Create(RequestValue requestValue);
        Task<RequestValue> Update(RequestValue requestValue);
        void DeleteById(int id);
    }
}
