using AutoMapper;
using FatecSisMed.MedicoAPI.DTO.Entities;
using FatecSisMed.MedicoAPI.Model.Entities;
using FatecSisMed.MedicoAPI.Repositories.Interfaces;
using FatecSisMed.MedicoAPI.Services.Interfaces;

namespace FatecSisMed.MedicoAPI.Services.Entities;

public class MedicoService : IMedicoService
{

    private readonly IMedicoRepository _medicoRepository;
    private readonly IMapper _mapper;

    public MedicoService(IMedicoRepository medicoRepository, IMapper mapper)
    {
        _medicoRepository = medicoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MedicoDTO>> GetAll()
    {
        var medicos = await _medicoRepository.GetAll();
        return _mapper.Map<IEnumerable<MedicoDTO>>(medicos);
    }

    public async Task<MedicoDTO> GetById(int id)
    {
        var medico = await _medicoRepository.GetById(id);
        return _mapper.Map<MedicoDTO>(medico);
    }

    public async Task Create(MedicoDTO medicoDTO)
    {
        var medico = _mapper.Map<Medico>(medicoDTO);
        await _medicoRepository.Create(medico);
        medicoDTO.Id = medico.Id;
    }
    
    public async Task Update(MedicoDTO medicoDTO)
    {
        var medico = _mapper.Map<Medico>(medicoDTO);
        await _medicoRepository.Update(medico);
    }

    public async Task Remove(int id)
    {
        await _medicoRepository.Delete(id);
    }
}

