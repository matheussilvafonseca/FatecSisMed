using FatecSisMed.MedicoAPI.Context.Entities;
using FatecSisMed.MedicoAPI.Model.Entities;
using FatecSisMed.MedicoAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FatecSisMed.MedicoAPI.Repositories.Entities
{
    public class EspecialidadeRepository : IEspecialidadeRepository
    {

        private readonly AppDbContext _dbContext;

        public EspecialidadeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Especialidade>> GetAll()
        {
            return await _dbContext.Especialidades.ToListAsync();
        }

        public async Task<Especialidade> GetById(int id)
        {
            return await _dbContext.Especialidades.FirstOrDefaultAsync(c => c.Id == id);;
        }

        public async Task<IEnumerable<Especialidade>> GetEspecialidadeMedicos()
        {
            return await _dbContext.Especialidades.Include(e => e.Medicos).ToListAsync();
        }

        public async Task<Especialidade> Create(Especialidade especialidade)
        {
            _dbContext.Especialidades.Add(especialidade);
            await _dbContext.SaveChangesAsync();
            return especialidade;
        }

        public async Task<Especialidade> Update(Especialidade especialidade)
        {
            _dbContext.Entry(especialidade).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return especialidade;
        }

        public async Task<Especialidade> Delete(int id)
        {
            var especialidade = await GetById(id);
            _dbContext.Especialidades.Remove(especialidade);
            await _dbContext.SaveChangesAsync();
            return especialidade;
        }

    }
}

