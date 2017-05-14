using PublishingHouse.Data.Entities;
using System;
using System.Threading.Tasks;

namespace PublishingHouse.Data.Repository.Interface
{
    public interface IArticleReadRepository
    {
        Task<Article[]> GetArticles(int? limit);
        Task<Article> GetArticle(long id);
        Task<int> CountArticles();
    }
}
