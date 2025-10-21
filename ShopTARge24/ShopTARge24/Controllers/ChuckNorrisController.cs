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
        public IActionResult GetNorrisFacts(ChuckNorrisViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction();
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Id(string Id)
        {
            ChuckNorrisDto dto = new();
            dto.Id = Id;

            _chuckNorrisServices.ChuckNorrisResult(dto);
            ChuckNorrisViewModel vm = new();

            vm.IconUrl = dto.IconUrl;
            vm.Id = dto.Id;
            vm.Url = dto.Url;
            vm.Value = dto.Value;

            return View(vm);
        }
    }
}
