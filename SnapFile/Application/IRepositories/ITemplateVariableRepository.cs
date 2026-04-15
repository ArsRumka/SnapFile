using SnapFile.Domain.Entities;

namespace SnapFile.Application.IRepositories
{
    public interface ITemplateVariableRepository
    {
        Task<List<TemplateVariable>> GetAll();
        Task<TemplateVariable> GetById(int id);
        Task<List<TemplateVariable>> GetByTemplateId(int templateId);
        Task<TemplateVariable> Create(TemplateVariable templateVariable);
        Task<TemplateVariable> Update(TemplateVariable templateVariable);
        void DeleteById(int id);
    }
}
