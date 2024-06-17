using FatecSisMed.MedicoAPI.DTO.Entities;

namespace FatecSisMed.MedicoAPI.Services.Interfaces;

public interface IMarcaService
{
    Task<IEnumerable<MarcaDTO>> GetAll();
    Task<MarcaDTO> GetById(int id);
    Task<IEnumerable<MarcaDTO>> GetMarcaMedicos();
    Task Create(MarcaDTO marcaDTO);
    Task Update(MarcaDTO marcaDTO);
    Task Remove(int id);
}
