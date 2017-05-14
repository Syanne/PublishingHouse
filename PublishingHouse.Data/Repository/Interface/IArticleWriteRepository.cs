using PublishingHouse.Data.Entities;
using System.Threading.Tasks;

namespace PublishingHouse.Data.Repository.Interface
{
    public interface IArticleWriteRepository
    {
        Task<Article> AddArticle(Article article);
        Task UpdateArticle(Article article);
        Task RemoveArticle(Article article);
    }
}
