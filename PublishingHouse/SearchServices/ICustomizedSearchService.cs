using PublishingHouse.Data.Entities;
using PublishingHouse.Models;
using PublishingHouse.Services.Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublishingHouse.SearchServices
{    
    public interface ICustomizedSearchService
    {
        Task<IEnumerable<Article>> DoSearch(SearchRequest request);
    }
}