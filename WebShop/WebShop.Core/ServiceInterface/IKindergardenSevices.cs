using WebShop.Core.Dto;
using WebShop.Core.Domain;

namespace WebShop.Core.ServiceInterface
{
    public interface IKindergardenServices
    {
        Task<Kindergarden> Create(KindergardenDto dto);
        Task<Kindergarden> Update(KindergardenDto dto);
        Task<Kindergarden> Delete(Guid id);
        Task<Kindergarden> DetailAsync(Guid id);
    }
}
