using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Models.ChuckNorris;

namespace ShopTARge24.Controllers
{
    public class ChuckNorrisController : Controller
    {
        private readonly IChuckNorrisServices _chuckNorrisServices;

        public ChuckNorrisController
            (
                IChuckNorrisServices chuckNorrisServices
            )
        {
            _chuckNorrisServices = chuckNorrisServices;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetNorrisFacts(ChuckNorrisViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ChuckNorris", new { Id = model.Id });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ChuckNorris(string Id)
        {

            var dto = new ChuckNorrisDto { Id = Id };
            var result = await _chuckNorrisServices.ChuckNorrisResult(dto);;

            var viewModel = new ChuckNorrisViewModel
            {
                IconUrl = dto.IconUrl,
                Id = dto.Id,
                Url = dto.Url,
                Value = dto.Value
            };

            return View(viewModel);
        }
    }
}
