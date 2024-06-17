using FatecSisMed.MedicoAPI.Context.Entities;
using FatecSisMed.MedicoAPI.Model.Entities;
using FatecSisMed.MedicoAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FatecSisMed.MedicoAPI.Repositories.Entities
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly AppDbContext _dbContext;

        public MarcaRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Marca>> GetAll()
        {
            return await _dbContext.Marcas.ToListAsync();
        }

        public async Task<Marca> GetById(int id)
        {
            return await _dbContext.Marcas.FirstOrDefaultAsync(ma => ma.Id == id);
        }

        public async Task<IEnumerable<Marca>> GetMarcaMedicos()
        {
            return await _dbContext.Marcas.Include(
                ma => ma.Remedios).ToListAsync();
        }

        public async Task<Marca> Create(Marca marca)
        {
            _dbContext.Marcas.Add(marca);
            await _dbContext.SaveChangesAsync();
            return marca;

        }

        public async Task<Marca> Update(Marca marca)
        {
            _dbContext.Entry(marca).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return marca;
        }

        public async Task<Marca> Delete(int id)
        {
            var marca = await GetById(id);
            _dbContext.Marcas.Remove(marca);
            await _dbContext.SaveChangesAsync();
            return marca;
        }
    }
}
