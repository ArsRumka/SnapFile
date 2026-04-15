using SnapFile.Domain.Entities;

namespace SnapFile.Application.IRepositories
{
    public interface IRequestRepository
    {
        Task<List<Request>> GetAll();
        Task<Request> GetById(int id);
        Task<List<Request>> GetByCreatedByUserId(int userId);
        Task<List<Request>> GetByStatus(string status);
        Task<Request> Create(Request request);
        Task<Request> Update(Request request);
        void DeleteById(int id);
    }
}
