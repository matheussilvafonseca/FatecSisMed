using AutoMapper;
using FatecSisMed.MedicoAPI.DTO.Entities;
using FatecSisMed.MedicoAPI.Model.Entities;
using FatecSisMed.MedicoAPI.Repositories.Interfaces;
using FatecSisMed.MedicoAPI.Services.Interfaces;

namespace FatecSisMed.MedicoAPI.Services.Entities;

public class ConvenioService : IConvenioService
{
    
    // O que os services fazem?
    // eles fazem as chamadas dos métodos
    // que realizarão as operações
    // no banco de dados, ou seja, os repositories

    private readonly IConvenioRepository _convenioRepository;
    private readonly IMapper _mapper;

    public ConvenioService(IConvenioRepository convenioRepository,
        IMapper mapper)
    {
        _convenioRepository = convenioRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ConvenioDTO>> GetAll()
    {
        var convenios = await _convenioRepository.GetAll();
        return _mapper.Map<IEnumerable<ConvenioDTO>>(convenios);
    }

    public async Task<ConvenioDTO> GetById(int id)
    {
        var convenio = await _convenioRepository.GetById(id);
        return _mapper.Map<ConvenioDTO>(convenio);
    }

    public async Task<IEnumerable<ConvenioDTO>> GetConvenioMedicos()
    {
        var convenios = await _convenioRepository.GetConvenioMedicos();
        return _mapper.Map<IEnumerable<ConvenioDTO>>(convenios);
    }

    public async Task Create(ConvenioDTO convenioDTO)
    {
        var convenio = _mapper.Map<Convenio>(convenioDTO);
        await _convenioRepository.Create(convenio);
        convenioDTO.Id = convenio.Id;
    }

    public async Task Update(ConvenioDTO convenioDTO)
    {
        var convenio = _mapper.Map<Convenio>(convenioDTO);
        await _convenioRepository.Update(convenio);
    }
    
    public async Task Remove(int id)
    {
        await _convenioRepository.Delete(id);
    }

}

