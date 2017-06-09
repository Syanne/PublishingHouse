using PublishingHouse.Data.Entities;
using PublishingHouse.Models;
using PublishingHouse.Services.Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublishingHouse.SearchServices
{
    public class CustomizedSearchService : ICustomizedSearchService
    {
        private readonly ISearchService searchService;
        private readonly ISignatureService signatureService;
        private readonly ITrieSearchService trieSearchService;
        public CustomizedSearchService(ISearchService searchService,
                                        ISignatureService signatureService,
                                        ITrieSearchService trieSearchService)
        {
            this.searchService = searchService;
            this.signatureService = signatureService;
            this.trieSearchService = trieSearchService;
        }
        public Task<IEnumerable<Article>> DoSearch(SearchRequest request)
        {
            switch (request.SearchMode)
            {
                case SearchMode.Signature:
                    {
                        return signatureService.DoSearch(request.SearchString);
                    }
                    break;
                case SearchMode.Trie:
                    {
                        return trieSearchService.DoSearch(request.SearchString);
                    }
                    break;
                default:
                    {
                        return searchService.DoSearch(request.SearchString);
                    }
                    break;
            }
        }
    }
}