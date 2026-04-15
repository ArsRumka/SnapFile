using SnapFile.Domain.Entities;

namespace SnapFile.Application.IRepositories
{
    public interface IRequestTypeRepository
    {
        Task<List<RequestType>> GetAll();
        Task<RequestType> GetById(int id);
        Task<RequestType> Create(RequestType requestType);
        Task<RequestType> Update(RequestType requestType);
        void DeleteById(int id);
    }
}
