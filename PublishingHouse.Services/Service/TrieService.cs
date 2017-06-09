using PublishingHouse.Data.Entities;
using PublishingHouse.Services.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublishingHouse.Services.Service
{
    internal class TrieService : ITrieSearchService
    {
        public Task DoIndexation(Article article)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Article>> DoSearch(string query)
        {
            throw new NotImplementedException();
        }
    }
}
