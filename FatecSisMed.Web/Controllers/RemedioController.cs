using FatecSisMed.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using FatecSisMed.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;

namespace FatecSisMed.Web.Controllers
{
    public class RemedioController : Controller
    {
        private readonly IRemedioService _remedioService;
        private readonly IConvenioService _convenioService;
        private readonly IEspecialidadeService _especialidadeService;
        private readonly IMarcaService _marcaService;

        public RemedioController(IRemedioService remedioService, IConvenioService convenioService, 
            IEspecialidadeService especialidadeService, IMarcaService marcaService)
        {
            _remedioService = remedioService;
            _convenioService = convenioService;
            _especialidadeService = especialidadeService;
            _marcaService = marcaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RemedioViewModel>>> Index()
        {
            var result = await _remedioService.GetAllRemedios(await GetAccessToken());
            if (result is null) return View("Error");
            return View(result);
        }


        [HttpGet]
        public async Task<IActionResult> CreateRemedio()
        {
            ViewBag.EspecialidadeId = new SelectList(await _especialidadeService.GetAllEspecialidades(await GetAccessToken()), "Id", "Nome");
            ViewBag.ConvenioId = new SelectList(await _convenioService.GetAllConvenios(await GetAccessToken()), "Id", "Nome");
            ViewBag.MarcaId = new SelectList(await _marcaService.GetAllMarcas(await GetAccessToken()), "Id", "Nome");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRemedio(RemedioViewModel remedioViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _remedioService.CreateRemedio(remedioViewModel, await GetAccessToken());
                if (result != null) return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.EspecialidadeId = new SelectList(await
                                     _especialidadeService.GetAllEspecialidades(await GetAccessToken()), "Id", "Nome");
                ViewBag.BrandId = new SelectList(await
                                     _convenioService.GetAllConvenios(await GetAccessToken()), "Id", "Nome");
                ViewBag.MarcaId = new SelectList(await
                                     _marcaService.GetAllMarcas(await GetAccessToken()), "Id", "Nome");
            }

            return View(remedioViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRemedio(int id)
        {

            ViewBag.EspecialidadeId = new
            SelectList(await _especialidadeService.GetAllEspecialidades(await GetAccessToken()), "Id", "Nome");
            ViewBag.ConvenioId = new
            SelectList(await _convenioService.GetAllConvenios(await GetAccessToken()), "Id", "Nome");
            ViewBag.MarcaId = new
            SelectList(await _marcaService.GetAllMarcas(await GetAccessToken()), "Id", "Nome");

            var result = await _remedioService.FindRemedioById(id, await GetAccessToken());

            if (result is null) return View("Error");

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRemedio(RemedioViewModel remedioViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _remedioService.UpdateRemedio(remedioViewModel, await GetAccessToken());
                if (result is not null) return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest("Error");
            }

            return View(remedioViewModel);
        }

        [HttpGet]
        public async Task<ActionResult<RemedioViewModel>> DeleteRemedio(int id)
        {
            var result = await _remedioService.FindRemedioById(id, await GetAccessToken());
            if (result is null) return View("Error");
            return View(result);
        }

        [HttpPost(), ActionName("DeleteRemedio")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _remedioService.DeleteRemedioById(id, await GetAccessToken());
            if (!result) return View("Error");
            return RedirectToAction("Index");
        }

        private async Task<string> GetAccessToken()
        {
            return await HttpContext.GetTokenAsync("acess_token");
        }
    }
}
