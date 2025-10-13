using Shop.Core.Domain;
using Shop.Core.Dto;

namespace Shop.Core.ServiceInterface
{
    public interface IFileServices
    {
        void UploadFilesToDatabase(KindergartenDto dto, Kindergarten domain);
    }
}
