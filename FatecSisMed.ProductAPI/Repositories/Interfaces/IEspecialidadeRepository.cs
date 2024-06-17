using FatecSisMed.MedicoAPI.Model.Entities;

namespace FatecSisMed.MedicoAPI.Repositories.Interfaces;

public interface IEspecialidadeRepository
{
    Task<IEnumerable<Especialidade>> GetAll();
    Task<IEnumerable<Especialidade>> GetEspecialidadeMedicos();
    Task<Especialidade> GetById(int id);
    Task<Especialidade> Create(Especialidade especialidade);
    Task<Especialidade> Update(Especialidade especialidade);
    Task<Especialidade> Delete(int id);
    
}

