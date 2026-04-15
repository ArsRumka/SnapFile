using Microsoft.EntityFrameworkCore;
using SnapFile.Application.IRepositories;
using SnapFile.Domain.Entities;
using SnapFile.Infrastructure.Data;

namespace SnapFile.Infrastructure.Repositories
{
    public class FormulationRepository : IFormulationRepository
    {
        private readonly AppDbContext _context;

        public FormulationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Formulation>> GetAll()
        {
            return await _context.Formulations.ToListAsync();
        }

        public async Task<Formulation> GetById(int id)
        {
            var result = await _context.Formulations
                .FirstOrDefaultAsync(f => f.Id == id);
            if (result == null)
                throw new InvalidOperationException("Formulation not found");
            return result;
        }

        public async Task<Formulation> Create(Formulation formulation)
        {
            _context.Formulations.Add(formulation);
            await _context.SaveChangesAsync();
            return formulation;
        }

        public async Task<Formulation> Update(Formulation formulation)
        {
            var existing = await _context.Formulations.FindAsync(formulation.Id);
            if (existing == null)
                throw new InvalidOperationException("Formulation not found");

            existing.Text = formulation.Text;

            await _context.SaveChangesAsync();
            return existing;
        }

        public void DeleteById(int id)
        {
            var entity = _context.Formulations.Find(id);
            if (entity == null)
                throw new InvalidOperationException("Formulation not found");

            _context.Formulations.Remove(entity);
            _context.SaveChanges();
        }
    }
}
