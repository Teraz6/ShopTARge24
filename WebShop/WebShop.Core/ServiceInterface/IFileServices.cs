using WebShop.Core.Domain;
using WebShop.Core.Dto;

namespace WebShop.Core.ServiceInterface
{
    public interface IFileServices
    {
        void FilesToApi(SpaceshipDto dto, Spaceships domain);
    }
}