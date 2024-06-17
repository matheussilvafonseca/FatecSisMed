using FatecSisMed.MedicoAPI.Context.Entities;
using FatecSisMed.MedicoAPI.Model.Entities;
using FatecSisMed.MedicoAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FatecSisMed.MedicoAPI.Repositories.Entities;

public class MedicoRepository : IMedicoRepository
{

    private readonly AppDbContext _dbContext;

    public MedicoRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Medico>> GetAll()
    {
        return await _dbContext.Medicos.ToListAsync();
    }

    public async Task<Medico> GetById(int id)
    {
        return await _dbContext.Medicos.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Medico> Create(Medico medico)
    {
        _dbContext.Medicos.Add(medico);
        await _dbContext.SaveChangesAsync();
        return medico;
    }

    public async Task<Medico> Update(Medico medico)
    {
        _dbContext.Entry(medico).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return medico;
    }

    public async Task<Medico> Delete(int id)
    {
        var medico = await GetById(id);
        _dbContext.Remove(medico);
        await _dbContext.SaveChangesAsync();
        return medico;
    }

}

