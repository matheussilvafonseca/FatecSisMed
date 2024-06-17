using AutoMapper;
using FatecSisMed.MedicoAPI.DTO.Entities;
using FatecSisMed.MedicoAPI.Model.Entities;
using FatecSisMed.MedicoAPI.Repositories.Interfaces;
using FatecSisMed.MedicoAPI.Services.Interfaces;

namespace FatecSisMed.MedicoAPI.Services.Entities;

public class RemedioService : IRemedioService
{
    private readonly IRemedioRepository _remedioRepository;
    private readonly IMapper _mapper;

    public RemedioService(IRemedioRepository remedioRepository,
        IMapper mapper)
    {
        _remedioRepository = remedioRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RemedioDTO>> GetAll()
    {
        var remedios = await _remedioRepository.GetAll();
        return _mapper.Map<IEnumerable<RemedioDTO>>(remedios);
    }

    public async Task<RemedioDTO> GetById(int id)
    {
        var remedio = await _remedioRepository.GetById(id);
        return _mapper.Map<RemedioDTO>(remedio);
    }

    public async Task<IEnumerable<RemedioDTO>> GetRemedioMedicos()
    {
        var remedios = await _remedioRepository.GetRemedioMedicos();
        return _mapper.Map<IEnumerable<RemedioDTO>>(remedios);
    }

    public async Task Create(RemedioDTO remedioDTO)
    {
        var remedio = _mapper.Map<Remedio>(remedioDTO);
        await _remedioRepository.Create(remedio);
        remedioDTO.Id = remedio.Id;
    }

    public async Task Update(RemedioDTO remedioDTO)
    {
        var remedio = _mapper.Map<Remedio>(remedioDTO);
        await _remedioRepository.Update(remedio);
    }

    public async Task Remove(int id)
    {
        await _remedioRepository.Delete(id);
    }
}
