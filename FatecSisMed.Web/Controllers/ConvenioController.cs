using FatecSisMed.Web.Models;
using FatecSisMed.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace FatecSisMed.Web.Controllers;

public class ConvenioController : Controller
{

    private readonly IConvenioService _convenioService;

    public ConvenioController(IConvenioService convenioService)
    {
        _convenioService = convenioService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ConvenioViewModel>>> Index()
    {
        var result = await _convenioService.GetAllConvenios(await GetAccessToken());
        if (result is null) return View("Error");
        return View(result);
    }

    // criar a view CreateConvenio
    [HttpGet]
    public async Task<IActionResult> CreateConvenio()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult>
        CreateConvenio(ConvenioViewModel convenioViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await
                _convenioService.CreateConvenio(convenioViewModel, await GetAccessToken());
            if (result is not null) return RedirectToAction(nameof(Index));
        }
        else
            return BadRequest("Error");

        return View(convenioViewModel);
    }

    // Criar a view UpdateConvenio
    [HttpGet]
    public async Task<IActionResult> UpdateConvenio(int id)
    {
        var result = await _convenioService.FindConvenioById(id, await GetAccessToken());
        if (result is null) return View("Error");
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult>
        UpdateConvenio(ConvenioViewModel convenioViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await
                _convenioService.UpdateConvenio(convenioViewModel, await GetAccessToken());
            if (result is not null) return RedirectToAction(nameof(Index));
        }

        return View(convenioViewModel);
    }

    // criar a view delete convenio
    [HttpGet]
    public async Task<ActionResult
        <ConvenioViewModel>> DeleteConvenio(int id)
    {
        var result = await _convenioService.FindConvenioById(id, await GetAccessToken());
        if (result is null) return View("Error");
        return View(result);
    }

    // nesse caso os dois precisariam ter o msm nome, só que como não pode ter 
    // duas assinaturas de métodos iguais, foi nomeado como DeleteConfirmed
    // porém é necessário chamar uma ação DeleteBrand
    // por isso tem o ActionName
    [HttpPost(), ActionName("DeleteConvenio")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _convenioService.DeleteConvenioById(id, await GetAccessToken());
        if (!result) return View("Error");
        return RedirectToAction("Index");
    }

    private async Task<string> GetAccessToken()
    {
        return await HttpContext.GetTokenAsync("acess_token");
    }
}

