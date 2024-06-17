using FatecSisMed.MedicoAPI.DTO.Entities;
using FatecSisMed.MedicoAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FatecSisMed.MedicoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadeController : Controller
    {
        private readonly IEspecialidadeService _especialidadeService;

        public EspecialidadeController(IEspecialidadeService especialidadeService)
        {
            _especialidadeService = especialidadeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EspecialidadeDTO>>> Get()
        {
            var especialidadesDTO = await _especialidadeService.GetAll();
            if (especialidadesDTO is null)
                return NotFound("Nenhuma especialidade foi encontrada!!!");
            return Ok(especialidadesDTO);
        }

        [HttpGet("especialidades")]
        public async Task<ActionResult<
            IEnumerable<EspecialidadeDTO>>> GetEspecialidadeMedicos()
        {
            var especialidadesDTO = await _especialidadeService.GetEspecialidadeMedicos();
            if (especialidadesDTO is null)
                return NotFound("Especialidades não encontradas!");
            return Ok(especialidadesDTO);
        }

        [HttpGet("{id:int}", Name = "GetEspecialidade")]
        public async Task<ActionResult<EspecialidadeDTO>> Get(int id)
        {
            var especialidadeDTO = await _especialidadeService.GetById(id);
            if (especialidadeDTO == null) return NotFound("Especialidade não encontrada!");
            return Ok(especialidadeDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EspecialidadeDTO especialidadeDTO)
        {
            if (especialidadeDTO is null) return BadRequest("Dados inválidos!");
            await _especialidadeService.Create(especialidadeDTO);
            return new CreatedAtRouteResult("GetEspecialidade",
                new { id = especialidadeDTO.Id }, especialidadeDTO);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] EspecialidadeDTO especialidadeDTO)
        {
            if (especialidadeDTO is null) return BadRequest("Dados inválidos!");
            await _especialidadeService.Update(especialidadeDTO);
            return Ok(especialidadeDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EspecialidadeDTO>> Delete(int id)
        {
            var especialidadeDTO = await _especialidadeService.GetById(id);
            if (especialidadeDTO is null) return NotFound("Especialidade não encontrada!");
            await _especialidadeService.Remove(id);
            return Ok(especialidadeDTO);
        }
    }
}

