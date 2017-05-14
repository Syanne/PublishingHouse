using PublishingHouse.Data.Entities;
using System.Threading.Tasks;

namespace PublishingHouse.Data.Repository.Interface
{
    public interface INGramsWriteRepository
    {
        Task AddNGram(NGram ngram);
    }
}
