using PublishingHouse.Data.Entities;
using PublishingHouse.Data.Repository.Interface;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PublishingHouse.Data.Repository
{
    internal class ArticleWriteRepository : IArticleWriteRepository
    {
        DataContext dataContext { get; set; }
        DbSet<Article> articles { get; set; }

        public ArticleWriteRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
            this.articles = this.dataContext.Articles;
        }

        public async Task<Article> AddArticle(Article article)
        {
            articles.Add(article);
            await dataContext.SaveChangesAsync();
            return article;
        }

        public Task RemoveArticle(Article article)
        {
            articles.Remove(article);
            return dataContext.SaveChangesAsync();
        }

        public async Task UpdateArticle(Article article)
        {
            var result = await dataContext
                                .Articles
                                .SingleOrDefaultAsync(b => b.Id == article.Id);

            if (result != null)
            {
                result = article;
                dataContext.SaveChanges();
            }
        }
    }
}
