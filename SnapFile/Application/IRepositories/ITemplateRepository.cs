using SnapFile.Domain.Entities;

namespace SnapFile.Application.IRepositories
{
    public interface ITemplateRepository
    {
        Task<List<Template>> GetAll();
        Task<Template> GetById(int id);
        Task<List<Template>> GetByRequestTypeId(int requestTypeId);
        Task<Template> Create(Template template);
        Task<Template> Update(Template template);
        void DeleteById(int id);
    }
}
