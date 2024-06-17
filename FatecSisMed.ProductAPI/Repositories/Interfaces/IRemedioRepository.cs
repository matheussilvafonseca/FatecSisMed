using FatecSisMed.MedicoAPI.Model.Entities;

namespace FatecSisMed.MedicoAPI.Repositories.Interfaces
{
    public interface IRemedioRepository
    {
        Task<IEnumerable<Remedio>> GetAll();
        Task<IEnumerable<Remedio>> GetRemedioMedicos();
        Task<Remedio> GetById(int id);
        Task<Remedio> Create(Remedio remedio);
        Task<Remedio> Update(Remedio remedio);
        Task<Remedio> Delete(int id);
    }
}
