using Microsoft.AspNetCore.Mvc;

namespace WebShop.Controllers
{
    public class SpaceshipsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
