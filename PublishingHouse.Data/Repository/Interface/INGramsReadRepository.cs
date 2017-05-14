using PublishingHouse.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublishingHouse.Data.Repository.Interface
{
    public interface INGramsReadRepository
    {
        Task<NGram[]> GetNGramCollection(long id);
        Task<NGram> GetNGram(string ngram);
    }
}
