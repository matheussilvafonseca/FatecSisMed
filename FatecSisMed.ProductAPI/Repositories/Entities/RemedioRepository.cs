using FatecSisMed.MedicoAPI.Context.Entities;
using FatecSisMed.MedicoAPI.Model.Entities;
using FatecSisMed.MedicoAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FatecSisMed.MedicoAPI.Repositories.Entities;

public class RemedioRepository : IRemedioRepository
{
    private readonly AppDbContext _dbContext;

    public RemedioRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Remedio>> GetAll()
    {
        return await _dbContext.Remedios.ToListAsync();
    }

    public async Task<Remedio> GetById(int id)
    {
        return await _dbContext.Remedios.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Remedio>> GetRemedioMedicos()
    {
        return await _dbContext.Remedios.Include(
            r => r.Medicos).ToListAsync();
    }

    public async Task<Remedio> Create(Remedio remedio)
    {
        _dbContext.Remedios.Add(remedio);
        await _dbContext.SaveChangesAsync();
        return remedio;

    }

    public async Task<Remedio> Update(Remedio remedio)
    {
        _dbContext.Entry(remedio).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return remedio;
    }

    public async Task<Remedio> Delete(int id)
    {
        var remedio = await GetById(id);
        _dbContext.Remedios.Remove(remedio);
        await _dbContext.SaveChangesAsync();
        return remedio;
    }
}
