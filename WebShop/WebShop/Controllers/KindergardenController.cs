using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Domain;
using WebShop.Core.Dto;
using WebShop.Core.ServiceInterface;
using WebShop.Data;
using WebShop.Models.Kindergardens;

namespace WebShop.Controllers
{
    public class KindergardensController : Controller
    {
        private readonly WebShopContext _context;
        private readonly IKindergardenServices _kindergardenServices;

        public KindergardensController
            (
                WebShopContext context,
                IKindergardenServices kindergardenServices
            )
        {
            _context = context;
            _kindergardenServices = kindergardenServices;
        }

        public IActionResult Index()
        {
            var result = _context.Kindergardens
                .Select(x => new KindergardenIndexViewModel
                {
                    Id = x.Id,
                    GroupName = x.GroupName,
                    ChildrenCount = x.ChildrenCount,
                    KindergardenName = x.KindergardenName,
                    TeacherName = x.TeacherName,
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            KindergardenCreateUpdateViewModel result = new();

            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(KindergardenCreateUpdateViewModel vm)
        {
            var dto = new KindergardenDto()
            {
                Id = vm.Id,
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergardenName = vm.KindergardenName,
                TeacherName = vm.TeacherName,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt
            };

            var result = await _kindergardenServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var kindergarden = await _kindergardenServices.DetailAsync(id);

            if (kindergarden == null)
            {
                return NotFound();
            }

            var vm = new KindergardenCreateUpdateViewModel
            {
                Id = kindergarden.Id,
                GroupName = kindergarden.GroupName,
                ChildrenCount = kindergarden.ChildrenCount,
                KindergardenName = kindergarden.KindergardenName,
                TeacherName = kindergarden.TeacherName,
                CreatedAt = kindergarden.CreatedAt,
                UpdatedAt = kindergarden.UpdatedAt
            };

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(KindergardenCreateUpdateViewModel vm)
        {
            var dto = new KindergardenDto()
            {
                Id = vm.Id,
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergardenName = vm.KindergardenName,
                TeacherName = vm.TeacherName,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt
            };

            var result = await _kindergardenServices.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var kindergarden = await _kindergardenServices.DetailAsync(id);

            if (kindergarden == null)
            {
                return NotFound();
            }

            var vm = new KindergardenDeleteViewModel
            {
                Id = kindergarden.Id,
                GroupName = kindergarden.GroupName,
                ChildrenCount = kindergarden.ChildrenCount,
                KindergardenName = kindergarden.KindergardenName,
                TeacherName = kindergarden.TeacherName,
                CreatedAt = kindergarden.CreatedAt,
                UpdatedAt = kindergarden.UpdatedAt
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var kindergarden = await _kindergardenServices.Delete(id);

            if (kindergarden == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var kindergarden = await _kindergardenServices.DetailAsync(id);

            if (kindergarden == null)
            {
                return NotFound();
            }

            var vm = new KindergardenDetailsViewModel
            {
                Id = kindergarden.Id,
                GroupName = kindergarden.GroupName,
                ChildrenCount = kindergarden.ChildrenCount,
                KindergardenName = kindergarden.KindergardenName,
                TeacherName = kindergarden.TeacherName,
                CreatedAt = kindergarden.CreatedAt,
                UpdatedAt = kindergarden.UpdatedAt
            };

            return View(vm);
        }
    }
}
