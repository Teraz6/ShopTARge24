using ShopTARge24.Core.Dto;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IChuckNorrisServices
    {
        Task<ChuckNorrisDto> ChuckNorrisResult(ChuckNorrisDto dto);
    }
}
