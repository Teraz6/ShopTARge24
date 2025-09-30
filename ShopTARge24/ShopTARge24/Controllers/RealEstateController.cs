using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;
using ShopTARge24.Models.RealEstate;
using ShopTARge24.Models.Spaceships;


namespace ShopTARge24.Controllers
{
    public class RealEstateController : Controller
    {
        private readonly ShopTARge24Context _context;
        private readonly IRealEstateServices _realEstateServices;
        private readonly IFileServices _fileServices;

        public RealEstateController
            (
                ShopTARge24Context context,
                IRealEstateServices realEstateServices,
                IFileServices fileServices
            )
        {
            _context = context;
            _realEstateServices = realEstateServices;
            _fileServices = fileServices;
        }


        public IActionResult Index()
        {
            var result = _context.RealEstates
                 .Select(x => new RealEstateIndexViewModel
                 {
                     Id = x.Id,
                     Area = x.Area,
                     Location = x.Location,
                     RoomNumber = x.RoomNumber,
                     BuildingType = x.BuildingType,
                     
                 });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            RealEstateCreateUpdateViewModel result = new();

            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Area = vm.Area,
                Location = vm.Location,
                RoomNumber = vm.RoomNumber,
                BuildingType = vm.BuildingType,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                FileToApiDtos = vm.Image
                    .Select(x => new FileToApiDto
                    {
                        Id = x.ImageId,
                        ExistingFilePath = x.FilePath,
                        RealEstateId = x.RealEstateId
                    }).ToArray()
            };

            var result = await _realEstateServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var realEstate = await _realEstateServices.DetailAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            var images = await _context.FileToApis
                .Where(x => x.RealEstateId == id)
                .Select(y => new ImageViewModel
                {
                    Filepath = y.ExistingFilePath,
                    ImageId = y.Id
                }).ToArrayAsync();

            var vm = new RealEstateCreateUpdateViewModel();

            vm.Id = realEstate.Id;
            vm.Area = realEstate.Area;
            vm.Location = realEstate.Location;
            vm.RoomNumber = realEstate.RoomNumber;
            vm.BuildingType = realEstate.BuildingType;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.ModifiedAt = realEstate.ModifiedAt;
            vm.Image.AddRange(images);

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Area = vm.Area,
                Location = vm.Location,
                RoomNumber = vm.RoomNumber,
                BuildingType = vm.BuildingType,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                FileToApiDtos = vm.Image
                    .Select(x => new FileToApiDto
                    {
                        Id = x.ImageId,
                        ExistingFilePath = x.FilePath,
                        RealEstateId = x.RealEstateId,
                    }).ToArray()
            };

            var result = await _realEstateServices.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var realEstate = await _realEstateServices.DetailAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            var images = await _context.FileToApis
                .Where(x => x.RealEstateId == id)
                .Select(y => new ImageViewModel
                {
                    Filepath = y.ExistingFilepath,
                    ImageId = y.Id
                }).ToArrayAsync();

            var vm = new RealEstateDeleteViewModel();

            vm.Id = realEstate.Id;
            vm.Area = realEstate.Area;
            vm.Location = realEstate.Location;
            vm.RoomNumber = realEstate.RoomNumber;
            vm.BuildingType = realEstate.BuildingType;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.ModifiedAt = realEstate.ModifiedAt;
            vm.ImageViewModels.AddRange(images);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var realEstate = await _realEstateServices.Delete(id);

            if (realEstate == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var realEstate = await _realEstateServices.DetailAsync(id);

            if(realEstate == null)
            {
                return NotFound();
            }

            var images = await _context.FileToApis
                .Where(x => x.RealEstateId == id)
                .Select(y => new ImageViewModel
                {
                    Filepath = y.ExistingFilePath,
                    ImageId = y.Id,
                }).ToArrayAsync();

            var vm = new RealEstateDetailsViewModel();

            vm.Id = realEstate.Id;
            vm.Area = realEstate.Area;
            vm.Location = realEstate.Location;
            vm.RoomNumber = realEstate.RoomNumber;
            vm.BuildingType = realEstate.BuildingType;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.ModifiedAt = realEstate.ModifiedAt;
            vm.Images.AddRange(images);

            return View(vm);
        }

        public async Task<IActionResult> RemoveImage(ImageViewModel vm)
        {
            var dto = new FileToApiDto()
            {
                Id = vm.ImageId
            };

            var image = await _fileServices.RemoveImageFromApi(dto);

            if(image == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
