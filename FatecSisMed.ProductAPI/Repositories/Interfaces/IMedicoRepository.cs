using System;
using FatecSisMed.MedicoAPI.Model.Entities;

namespace FatecSisMed.MedicoAPI.Repositories.Interfaces
{
	public interface IMedicoRepository
	{
        Task<IEnumerable<Medico>> GetAll();
        Task<Medico> GetById(int id);
        Task<Medico> Create(Medico medico);
        Task<Medico> Update(Medico medico);
        Task<Medico> Delete(int id);
    }
}

