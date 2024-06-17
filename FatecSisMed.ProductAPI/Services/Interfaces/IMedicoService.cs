using FatecSisMed.MedicoAPI.DTO.Entities;

namespace FatecSisMed.MedicoAPI.Services.Interfaces;

public interface IMedicoService
{
    Task<IEnumerable<MedicoDTO>> GetAll();
    Task<MedicoDTO> GetById(int id);
    Task Create(MedicoDTO medicoDTO);
    Task Update(MedicoDTO medicoDTO);
    Task Remove(int id);
}

