using AutoMapper;
using FatecSisMed.MedicoAPI.DTO.Entities;
using FatecSisMed.MedicoAPI.Model.Entities;
using FatecSisMed.MedicoAPI.Repositories.Interfaces;
using FatecSisMed.MedicoAPI.Services.Interfaces;

namespace FatecSisMed.MedicoAPI.Services.Entities;

public class MarcaService : IMarcaService
{
    private readonly IMarcaRepository _marcaRepository;
    private readonly IMapper _mapper;

    public MarcaService(IMarcaRepository marcaRepository,
        IMapper mapper)
    {
        _marcaRepository = marcaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MarcaDTO>> GetAll()
    {
        var marcas = await _marcaRepository.GetAll();
        return _mapper.Map<IEnumerable<MarcaDTO>>(marcas);
    }

    public async Task<MarcaDTO> GetById(int id)
    {
        var marca = await _marcaRepository.GetById(id);
        return _mapper.Map<MarcaDTO>(marca);
    }

    public async Task<IEnumerable<MarcaDTO>> GetMarcaMedicos()
    {
        var marcas = await _marcaRepository.GetMarcaMedicos();
        return _mapper.Map<IEnumerable<MarcaDTO>>(marcas);
    }

    public async Task Create(MarcaDTO marcaDTO)
    {
        var marca = _mapper.Map<Marca>(marcaDTO);
        await _marcaRepository.Create(marca);
        marcaDTO.Id = marca.Id;
    }

    public async Task Update(MarcaDTO marcaDTO)
    {
        var marca = _mapper.Map<Marca>(marcaDTO);
        await _marcaRepository.Update(marca);
    }

    public async Task Remove(int id)
    {
        await _marcaRepository.Delete(id);
    }
}
