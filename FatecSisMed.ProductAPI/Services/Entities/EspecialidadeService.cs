using AutoMapper;
using FatecSisMed.MedicoAPI.DTO.Entities;
using FatecSisMed.MedicoAPI.Model.Entities;
using FatecSisMed.MedicoAPI.Repositories.Interfaces;
using FatecSisMed.MedicoAPI.Services.Interfaces;

namespace FatecSisMed.MedicoAPI.Services.Entities;

public class EspecialidadeService : IEspecialidadeService
{

    private readonly IEspecialidadeRepository _especialidadeRepository;
    private readonly IMapper _mapper;

    public EspecialidadeService(IEspecialidadeRepository especialidadeRepository,
        IMapper mapper)
    {
        _especialidadeRepository = especialidadeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EspecialidadeDTO>> GetAll()
    {
        var especialidades = await _especialidadeRepository.GetAll();
        return _mapper.Map<IEnumerable<EspecialidadeDTO>>(especialidades);
    }
    
    public async Task<EspecialidadeDTO> GetById(int id)
    {
        var especialidade = await _especialidadeRepository.GetById(id);
        return _mapper.Map<EspecialidadeDTO>(especialidade);
    }

    public async Task<IEnumerable<EspecialidadeDTO>> GetEspecialidadeMedicos()
    {
        var especialidades = await _especialidadeRepository.GetEspecialidadeMedicos();
        return _mapper.Map<IEnumerable<EspecialidadeDTO>>(especialidades);
    }

    public async Task Create(EspecialidadeDTO especialidadeDTO)
    {
        var especialidade = _mapper.Map<Especialidade>(especialidadeDTO);
        await _especialidadeRepository.Create(especialidade);
        especialidadeDTO.Id = especialidade.Id;
    }

    public async Task Update(EspecialidadeDTO especialidadeDTO)
    {
        var especialidade = _mapper.Map<Especialidade>(especialidadeDTO);
        await _especialidadeRepository.Update(especialidade);
    }

    public async Task Remove(int id)
    {
        await _especialidadeRepository.Delete(id);
    }
}

