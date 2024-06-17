using FatecSisMed.Web.Models;
using FatecSisMed.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FatecSisMed.Web.Controllers;

public class MedicoController : Controller
{

    private readonly IMedicoService _medicoService;
    private readonly IConvenioService _convenioService;
    private readonly IEspecialidadeService _especialidadeService;

    public MedicoController(IMedicoService medicoService, IConvenioService convenioService, IEspecialidadeService especialidadeService)
    {
        _medicoService = medicoService;
        _convenioService = convenioService;
        _especialidadeService = especialidadeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MedicoViewModel>>> Index()
    {
        var result = await _medicoService.GetAllMedicos(await GetAccessToken());
        if (result is null) return View("Error");
        return View(result);
    }

    
    [HttpGet]
    public async Task<IActionResult> CreateMedico()
    {
        ViewBag.EspecialidadeId = new SelectList(await _especialidadeService.GetAllEspecialidades(await GetAccessToken()), "Id", "Nome");
        ViewBag.ConvenioId = new SelectList(await _convenioService.GetAllConvenios(await GetAccessToken()), "Id", "Nome");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateMedico(MedicoViewModel medicoViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _medicoService.CreateMedico(medicoViewModel, await GetAccessToken());
            if (result != null) return RedirectToAction(nameof(Index));
        }
        else
        {
            ViewBag.EspecialidadeId = new SelectList(await
                                 _especialidadeService.GetAllEspecialidades(await GetAccessToken()), "Id", "Nome");
            ViewBag.BrandId = new SelectList(await
                                 _convenioService.GetAllConvenios(await GetAccessToken()), "Id", "Nome");
        }

        return View(medicoViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateMedico(int id)
    {

        ViewBag.EspecialidadeId = new
        SelectList(await _especialidadeService.GetAllEspecialidades(await GetAccessToken()), "Id", "Nome");
        ViewBag.ConvenioId = new
        SelectList(await _convenioService.GetAllConvenios(await GetAccessToken()), "Id", "Nome");

        var result = await _medicoService.FindMedicoById(id, await GetAccessToken());

        if (result is null) return View("Error");

        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateMedico(MedicoViewModel medicoViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _medicoService.UpdateMedico(medicoViewModel, await GetAccessToken());
            if (result is not null) return RedirectToAction(nameof(Index));
        }
        else
        {
            return BadRequest("Error");
        }

        return View(medicoViewModel);
    }

    [HttpGet]
    public async Task<ActionResult<MedicoViewModel>> DeleteMedico(int id)
    {
        var result = await _medicoService.FindMedicoById(id, await GetAccessToken());
        if (result is null) return View("Error");
        return View(result);
    }

    [HttpPost(), ActionName("DeleteMedico")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _medicoService.DeleteMedicoById(id, await GetAccessToken());
        if (!result) return View("Error");
        return RedirectToAction("Index");
    }

    private async Task<string> GetAccessToken()
    {
        return await HttpContext.GetTokenAsync("acess_token");
    }

}

