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
    internal class NGramService : ISearchService
    {
        private INGramsReadRepository nGramsReadRepository { get; set; }
        private INGramsWriteRepository nGramsWriteRepository { get; set; }
        private IArticleReadRepository articleReadRepository { get; set; }
        private IArticleWriteRepository articleWriteRepository { get; set; }
        private INGramAlgorithm nGramAlgorithm { get; set; }

        public NGramService(
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

        public async Task DoIndexation(Article article)
        {
            var query = String.Join(" ", article.Name, article.Authors, article.Annotation);

            var nGrams = nGramAlgorithm.GetNGramsCollection(query);

            foreach (var nGram in nGrams)
            {
               var currentNGram = await nGramsReadRepository.GetNGram(nGram);

                if (currentNGram == null)
                {
                    currentNGram = new NGram() { NGramValue = nGram };
                    await nGramsWriteRepository.AddNGram(currentNGram);
                }

                article.NGrams.Add(currentNGram);
            }

            await articleWriteRepository.UpdateArticle(article);
        }

        public async Task<IEnumerable<Article>> DoSearch(string query)
        {
            if (query == "")
            {
                return await articleReadRepository.GetArticles(null);
            }

            var articles = new List<Article>();
            var nGrams = nGramAlgorithm.GetNGramsCollection(query);

            foreach (var nGram in nGrams)
            {
                var currentNGram = await nGramsReadRepository.GetNGram(nGram);
                if (currentNGram != null)
                    articles.AddRange(currentNGram.Articles);
            }

            return articles
                        .GroupBy(key => key.Id)
                        .OrderByDescending(key => key.Count())
                        .Select(key => key.First());
        }
    }
}
