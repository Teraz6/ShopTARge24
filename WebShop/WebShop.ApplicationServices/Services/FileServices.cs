using WebShop.Core.Dto;
using WebShop.Core.Domain;
using WebShop.Core.ServiceInterface;
using Microsoft.Extensions.Hosting;
using WebShop.Data;

namespace WebShop.ApplicationServices.Services
{
    public class FileServices : IFileServices
    {
        private readonly IHostEnvironment _webHost;
        private readonly WebShopContext _context;

        public FileServices
            (
                IHostEnvironment webHost,
                WebShopContext context
            )
        {
            _webHost = webHost;
            _context = context;
        }
        public void FilesToApi(SpaceshipDto dto, Spaceships domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                if (!Directory.Exists(_webHost.ContentRootPath + "\\multipleFileUpload\\"))
                {
                    Directory.CreateDirectory(_webHost.ContentRootPath + "\\multipleFileUpload\\");
                }

                foreach (var file in dto.Files)
                {
                    string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "multipleFileUpload");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var filestream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(filestream);

                        FileToApi path = new FileToApi
                        {
                            Id = Guid.NewGuid(),
                            ExistingFilePath = uniqueFileName,
                            SpaceshipId = domain.Id
                        };

                        _context.FileToApis.AddAsync(path);
                    }
                }
            }
        }
    }
}