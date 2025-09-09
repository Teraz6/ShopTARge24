using WebShop.Core.Domain;
using WebShop.Core.Dto;

namespace WebShop.Core.ServiceInterface
{
    public interface ISpaceshipServices
    {
        Task<Spaceships> Create(SpaceshipDto dto);
        Task<Spaceships> DetailAsync(Guid id);
    }
}
