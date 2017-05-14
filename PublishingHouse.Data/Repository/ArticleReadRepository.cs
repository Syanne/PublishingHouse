using PublishingHouse.Data.Entities;
using PublishingHouse.Data.Repository.Interface;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PublishingHouse.Data.Repository
{
    internal class ArticleReadRepository : IArticleReadRepository
    {
        DataContext dataContext { get; set; }
        DbSet<Article> articles { get; set; }

        public ArticleReadRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
            this.articles = this.dataContext.Article;
        }
        public Task<int> CountArticles()
        {
            return articles.CountAsync();
        }

        public Task<Article> GetArticle(long id)
        {
            return articles
                .Include(article => article.NGrams)
                .FirstOrDefaultAsync(article => article.Id == id);
        }

        public Task<Article[]> GetArticles(int? limit)
        {
            var foundArticles = articles
                                .OrderByDescending(article => article.Id);

            if (limit.HasValue)
                return articles.Take(limit.Value).ToArrayAsync();

            return articles.ToArrayAsync();
        }
    }
}
