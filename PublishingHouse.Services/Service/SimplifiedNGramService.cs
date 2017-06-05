using PublishingHouse.Data.Entities;
using PublishingHouse.Data.Repository.Interface;
using PublishingHouse.Services.Algorithm.Interface;
using PublishingHouse.Services.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublishingHouse.Services.Service
{
    internal class SimplifiedNGramService : ISearchService
    {
        private INGramsReadRepository nGramsReadRepository { get; set; }
        private INGramsWriteRepository nGramsWriteRepository { get; set; }
        private IArticleReadRepository articleReadRepository { get; set; }
        private IArticleWriteRepository articleWriteRepository { get; set; }
        private INGramAlgorithm nGramAlgorithm { get; set; }

        public SimplifiedNGramService(
            INGramsReadRepository nGramsReadRepository,
            INGramsWriteRepository nGramsWriteRepository,
            IArticleReadRepository articleReadRepository,
            IArticleWriteRepository articleWriteRepository,
            INGramAlgorithm nGramAlgorithm
            )
        {
            this.nGramsReadRepository = nGramsReadRepository;
            this.articleReadRepository = articleReadRepository;
            this.nGramsWriteRepository = nGramsWriteRepository;
            this.articleWriteRepository = articleWriteRepository;
            this.nGramAlgorithm = nGramAlgorithm;
        }

        public async Task<IEnumerable<Article>> DoSearch(string query)
        {
            var articles = await articleReadRepository.GetArticles(null);
            if (query == "")
            {
                return articles;
            }

            var suitableArticles = new List<Article>();
            var nGrams = nGramAlgorithm.GetNGramsCollection(query);

            foreach (var nGram in nGrams)
            {
                var articlesToAdd = articles.Where(article => article.Name.Contains(nGram) ||
                                                               article.Authors.Contains(nGram) ||
                                                               article.Annotation.Contains(nGram))
                                                               .ToArray();
                
                if (articlesToAdd.Count() > 0)
                    suitableArticles.AddRange(articlesToAdd);
            }

            return suitableArticles
                        .GroupBy(key => key.Id)
                        .OrderByDescending(key => key.Count())
                        .Select(key => key.First());
        }

        public Task DoIndexation(Article article)
        {
            return Task.FromResult(0);
        }
    }
}
