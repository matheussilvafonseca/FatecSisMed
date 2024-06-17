using FatecSisMed.MedicoAPI.DTO.Entities;

namespace FatecSisMed.MedicoAPI.Services.Interfaces;

public interface IRemedioService
{
    Task<IEnumerable<RemedioDTO>> GetAll();
    Task<RemedioDTO> GetById(int id);
    Task<IEnumerable<RemedioDTO>> GetRemedioMedicos();
    Task Create(RemedioDTO remedioDTO);
    Task Update(RemedioDTO remedioDTO);
    Task Remove(int id);
}
