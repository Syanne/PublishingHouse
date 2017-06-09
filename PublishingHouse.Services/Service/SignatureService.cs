using PublishingHouse.Services.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PublishingHouse.Data.Entities;
using PublishingHouse.Data.Repository.Interface;
using PublishingHouse.Services.Algorithm.Interface;

namespace PublishingHouse.Services.Service
{
    class SignatureService : ISignatureService
    {
        private ISignatureWriteRepository signatureWriteRepository { get; set; }
        private ISignatureReadRepository signatureReadRepository { get; set; }
        private IArticleReadRepository articleReadRepository { get; set; }
        private IArticleWriteRepository articleWriteRepository { get; set; }
        private ISignatureAlgorithm signatureAlgorithm { get; set; }

        public SignatureService(
            ISignatureWriteRepository signatureWriteRepository,
            ISignatureReadRepository signatureReadRepository,
            IArticleReadRepository articleReadRepository,
            IArticleWriteRepository articleWriteRepository,
            ISignatureAlgorithm signatureAlgorithm
            )
        {
            this.signatureWriteRepository = signatureWriteRepository;
            this.articleReadRepository = articleReadRepository;
            this.signatureReadRepository = signatureReadRepository;
            this.articleWriteRepository = articleWriteRepository;
            this.signatureAlgorithm = signatureAlgorithm;
        }
        public async Task DoIndexation(Article article)
        {
                var query = String.Join(" ", article.Name, article.Authors, article.Annotation);

                var signatures = signatureAlgorithm.GetSignatures(query);
                
                foreach (var signature in signatures)
                {
                    var currentSignature = await signatureReadRepository.GetSignature(signature);

                    if (currentSignature == null)
                    {
                        currentSignature = new Signature() { Hash = signature };
                        await signatureWriteRepository.AddSinature(currentSignature);
                    }

                    article.Signatures.Add(currentSignature);
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
            var signatures = signatureAlgorithm.GetSignatures(query);   

            foreach (var signature in signatures)
            {
                var currentSignature = await signatureReadRepository.GetSignature(signature);
                if (currentSignature != null && currentSignature.Hash != signatureAlgorithm.GetDefaultSignature)
                    articles.AddRange(currentSignature.Articles);
            }

            return articles
                        .GroupBy(key => key.Id)
                        .OrderByDescending(key => key.Count())
                        .Select(key => key.First());
        }
    }
}
