using PublishingHouse.Data.Entities;
using System.Threading.Tasks;

namespace PublishingHouse.Data.Repository.Interface
{
    public interface ISignatureReadRepository
    {
        Task<Signature[]> GetSignatureCollection(long id);
        Task<Signature> GetSignature(string signature);
    }
}
