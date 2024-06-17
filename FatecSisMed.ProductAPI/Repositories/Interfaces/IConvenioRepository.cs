using System;
using FatecSisMed.MedicoAPI.Model.Entities;

namespace FatecSisMed.MedicoAPI.Repositories.Interfaces
{
	public interface IConvenioRepository
	{
        Task<IEnumerable<Convenio>> GetAll();
        Task<IEnumerable<Convenio>> GetConvenioMedicos();
        Task<Convenio> GetById(int id);
        Task<Convenio> Create(Convenio convenio);
        Task<Convenio> Update(Convenio convenio);
        Task<Convenio> Delete(int id);
    }
}

