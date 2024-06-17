using FatecSisMed.MedicoAPI.DTO.Entities;
using FatecSisMed.MedicoAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FatecSisMed.MedicoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicoController : Controller
{
    private readonly IMedicoService _medicoService;

    public MedicoController(IMedicoService medicoService)
    {
        _medicoService = medicoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MedicoDTO>>> Get()
    {
        var medicosDTO = await _medicoService.GetAll();
        if (medicosDTO is null)
            return NotFound("Não foi encontrado nenhum médico!");
        return Ok(medicosDTO);
    }

    [HttpGet("{id:int}", Name = "GetMedico")]
    public async Task<ActionResult<MedicoDTO>> Get(int id)
    {
        var medicoDTO = await _medicoService.GetById(id);
        if (medicoDTO == null) return NotFound("Médico não encontrado!");
        return Ok(medicoDTO);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] MedicoDTO medicoDTO)
    {
        if (medicoDTO is null) return BadRequest("Dados inválidos!");
        await _medicoService.Create(medicoDTO);
        return new CreatedAtRouteResult("GetMedico",
            new { id = medicoDTO.Id }, medicoDTO);
    }

    [HttpPut()]
    public async Task<ActionResult> Put([FromBody] MedicoDTO medicoDTO)
    {
        if (medicoDTO is null) return BadRequest("Dados inválidos!");
        await _medicoService.Update(medicoDTO);
        return Ok(medicoDTO);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<MedicoDTO>> Delete(int id)
    {
        var medicoDTO = await _medicoService.GetById(id);
        await _medicoService.Remove(id);
        return Ok(medicoDTO);
    }

}

