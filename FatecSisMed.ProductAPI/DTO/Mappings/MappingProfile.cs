using AutoMapper;
using FatecSisMed.MedicoAPI.DTO.Entities;
using FatecSisMed.MedicoAPI.Model.Entities;

namespace FatecSisMed.MedicoAPI.DTO.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Convenio, ConvenioDTO>().ReverseMap();
        CreateMap<Especialidade, EspecialidadeDTO>().ReverseMap();
        CreateMap<Medico, MedicoDTO>().ReverseMap();
    }

}

