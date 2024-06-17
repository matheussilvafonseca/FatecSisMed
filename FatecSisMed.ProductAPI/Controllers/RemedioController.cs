using FatecSisMed.MedicoAPI.DTO.Entities;
using FatecSisMed.MedicoAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FatecSisMed.MedicoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RemedioController : Controller
{
    private readonly IRemedioService _remedioService;

    public RemedioController(IRemedioService remedioService)
    {
        _remedioService = remedioService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RemedioDTO>>> Get()
    {
        var remediosDTO = await _remedioService.GetAll();
        if (remediosDTO is null)
            return NotFound("Não foi encontrado nenhum médico!");
        return Ok(remediosDTO);
    }

    [HttpGet("{id:int}", Name = "GetRemedio")]
    public async Task<ActionResult<RemedioDTO>> Get(int id)
    {
        var remedioDTO = await _remedioService.GetById(id);
        if (remedioDTO == null) return NotFound("Remédio não encontrado!");
        return Ok(remedioDTO);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] RemedioDTO remedioDTO)
    {
        if (remedioDTO is null) return BadRequest("Dados inválidos!");
        await _remedioService.Create(remedioDTO);
        return new CreatedAtRouteResult("GetRemedio",
            new { id = remedioDTO.Id }, remedioDTO);
    }

    [HttpPut()]
    public async Task<ActionResult> Put([FromBody] RemedioDTO remedioDTO)
    {
        if (remedioDTO is null) return BadRequest("Dados inválidos!");
        await _remedioService.Update(remedioDTO);
        return Ok(remedioDTO);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<RemedioDTO>> Delete(int id)
    {
        var remedioDTO = await _remedioService.GetById(id);
        await _remedioService.Remove(id);
        return Ok(remedioDTO);
    }
}
