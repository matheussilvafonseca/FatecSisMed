using FatecSisMed.MedicoAPI.DTO.Entities;

namespace FatecSisMed.MedicoAPI.Services.Interfaces;

public interface IConvenioService
{
    Task<IEnumerable<ConvenioDTO>> GetAll();
    Task<ConvenioDTO> GetById(int id);
    Task<IEnumerable<ConvenioDTO>> GetConvenioMedicos();
    Task Create(ConvenioDTO convenioDTO);
    Task Update(ConvenioDTO convenioDTO);
    Task Remove(int id);
}

