using FatecSisMed.MedicoAPI.Context.Entities;
using FatecSisMed.MedicoAPI.Model.Entities;
using FatecSisMed.MedicoAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FatecSisMed.MedicoAPI.Repositories.Entities;

public class ConvenioRepository : IConvenioRepository
{

    // o que os repositories fazem?
    // fazem com que os métodos tenham acesso ao BD

    private readonly AppDbContext _dbContext;

    public ConvenioRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Convenio>> GetAll()
    {
        return await _dbContext.Convenios.ToListAsync();
    }

    public async Task<Convenio> GetById(int id)
    {
        return await _dbContext.Convenios.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Convenio>> GetConvenioMedicos()
    {
        return await _dbContext.Convenios.Include(
            c => c.Medicos).ToListAsync();
    }

    public async Task<Convenio> Create(Convenio convenio)
    {
        _dbContext.Convenios.Add(convenio);
        await _dbContext.SaveChangesAsync();
        return convenio;

    }

    public async Task<Convenio> Update(Convenio convenio)
    {
        _dbContext.Entry(convenio).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return convenio;
    }

    public async Task<Convenio> Delete(int id)
    {
        var convenio = await GetById(id);
        _dbContext.Convenios.Remove(convenio);
        await _dbContext.SaveChangesAsync();
        return convenio;
    }

}

