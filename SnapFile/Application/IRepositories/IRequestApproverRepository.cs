using SnapFile.Domain.Entities;

namespace SnapFile.Application.IRepositories
{
    public interface IRequestApproverRepository
    {
        Task<List<RequestApprover>> GetAll();
        Task<RequestApprover> GetById(int id);
        Task<List<RequestApprover>> GetByRequestId(int requestId);
        Task<RequestApprover> Create(RequestApprover requestApprover);
        Task<RequestApprover> Update(RequestApprover requestApprover);
        void DeleteById(int id);
    }
}
