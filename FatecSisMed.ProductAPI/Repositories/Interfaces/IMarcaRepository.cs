using FatecSisMed.MedicoAPI.Model.Entities;

namespace FatecSisMed.MedicoAPI.Repositories.Interfaces
{
    public interface IMarcaRepository
    {
        Task<IEnumerable<Marca>> GetAll();
        Task<IEnumerable<Marca>> GetMarcaMedicos();
        Task<Marca> GetById(int id);
        Task<Marca> Create(Marca marca);
        Task<Marca> Update(Marca marca);
        Task<Marca> Delete(int id);
    }
}
