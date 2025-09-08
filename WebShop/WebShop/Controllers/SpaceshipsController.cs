using Microsoft.AspNetCore.Mvc;
using WebShop.Data;
using WebShop.Models.Spaceships;
using WebShop.Core;

namespace WebShop.Controllers
{
    public class SpaceshipsController : Controller
    {
        private readonly WebShopContext _context;

        public SpaceshipsController
            (
                WebShopContext context
            )
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var result = _context.Spaceships
                .Select(x => new SpaceshipIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Classification = x.Classification,
                    BuiltDate = x.BuiltDate,
                    Crew = x.Crew,
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            SpaceshipCreateViewModel result = new();

            return View("Create", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
