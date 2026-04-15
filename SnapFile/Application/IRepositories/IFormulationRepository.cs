using SnapFile.Domain.Entities;

namespace SnapFile.Application.IRepositories
{
    public interface IFormulationRepository
    {
        Task<List<Formulation>> GetAll();
        Task<Formulation> GetById(int id);
        Task<Formulation> Create(Formulation formulation);
        Task<Formulation> Update(Formulation formulation);
        void DeleteById(int id);
    }
}
