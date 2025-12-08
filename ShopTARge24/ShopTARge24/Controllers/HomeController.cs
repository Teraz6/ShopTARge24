using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ShopTARge24.Data;
using ShopTARge24.Models;
using ShopTARge24.Models.Chat;
using System.Diagnostics;
using System.Security.Claims;

namespace ShopTARge24.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShopTARge24Context _context;

        public HomeController(ILogger<HomeController> logger, ShopTARge24Context context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize]
        public IActionResult Chat()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ChatVM chatVm = new()
            {
                Rooms = _context.ChatRoom.ToList(),
                MaxRoomAllowed = 4,
                UserId = userId,
            };
            return View(chatVm);
        }
        public IActionResult AdvancedChat()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ChatVM chatVm = new()
            {
                Rooms = _context.ChatRoom.ToList(),
                MaxRoomAllowed = 4,
                UserId = userId,
            };
            return View(chatVm);
        }
        public IActionResult BasicChat()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignalR()
        {
            return View();
        }

        public async Task<IActionResult> DeathlyHallows(string type)
        {
            if (SD.DealthyHallowRace.ContainsKey(type))
            {
                SD.DealthyHallowRace[type]++;
            }
            return Accepted();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
