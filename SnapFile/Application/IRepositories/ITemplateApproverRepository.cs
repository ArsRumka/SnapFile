using SnapFile.Domain.Entities;

namespace SnapFile.Application.IRepositories
{
    public interface ITemplateApproverRepository
    {
        Task<List<TemplateApprover>> GetAll();
        Task<TemplateApprover> GetById(int id);
        Task<List<TemplateApprover>> GetByTemplateId(int templateId);
        Task<TemplateApprover> Create(TemplateApprover templateApprover);
        Task<TemplateApprover> Update(TemplateApprover templateApprover);
        void DeleteById(int id);
    }
}
