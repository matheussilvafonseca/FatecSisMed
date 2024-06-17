using FatecSisMed.Web.Models;
using FatecSisMed.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace FatecSisMed.Web.Controllers
{
    public class MarcaController : Controller
    {
        private readonly IMarcaService _marcaService;

        public MarcaController(IMarcaService marcaService)
        {
            _marcaService = marcaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcaViewModel>>> Index()
        {
            var result = await _marcaService.GetAllMarcas(await GetAccessToken());
            if (result is null) return View("Error");
            return View(result);
        }

        // criar a view CreateMarca
        [HttpGet]
        public async Task<IActionResult> CreateMarca()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>
            CreateMarca(MarcaViewModel marcaViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await
                    _marcaService.CreateMarca(marcaViewModel, await GetAccessToken());
                if (result is not null) return RedirectToAction(nameof(Index));
            }
            else
                return BadRequest("Error");

            return View(marcaViewModel);
        }

        // Criar a view UpdateMarca
        [HttpGet]
        public async Task<IActionResult> UpdateMarca(int id)
        {
            var result = await _marcaService.FindMarcaById(id, await GetAccessToken());
            if (result is null) return View("Error");
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult>
            UpdateMarca(MarcaViewModel marcaViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await
                    _marcaService.UpdateMarca(marcaViewModel, await GetAccessToken());
                if (result is not null) return RedirectToAction(nameof(Index));
            }

            return View(marcaViewModel);
        }

        // criar a view delete Marca
        [HttpGet]
        public async Task<ActionResult
            <MarcaViewModel>> DeleteMarca(int id)
        {
            var result = await _marcaService.FindMarcaById(id, await GetAccessToken());
            if (result is null) return View("Error");
            return View(result);
        }

        // nesse caso os dois precisariam ter o msm nome, só que como não pode ter 
        // duas assinaturas de métodos iguais, foi nomeado como DeleteConfirmed
        // porém é necessário chamar uma ação DeleteBrand
        // por isso tem o ActionName
        [HttpPost(), ActionName("DeleteMarca")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _marcaService.DeleteMarcaById(id, await GetAccessToken());
            if (!result) return View("Error");
            return RedirectToAction("Index");
        }

        private async Task<string> GetAccessToken()
        {
            return await HttpContext.GetTokenAsync("acess_token");
        }
    }
}
