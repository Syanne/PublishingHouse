using PublishingHouse.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublishingHouse.Services.Service.Interface
{
    public interface ISignatureService
    {
        Task<IEnumerable<Article>> DoSearch(string query);
        Task DoIndexation(Article article);
    }
}