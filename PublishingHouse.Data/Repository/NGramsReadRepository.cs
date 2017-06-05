using PublishingHouse.Data.Entities;
using PublishingHouse.Data.Repository.Interface;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PublishingHouse.Data.Repository
{
    internal class NGramsReadRepository : INGramsReadRepository
    {
        DataContext dataContext { get; set; }
        public NGramsReadRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public Task<NGram[]> GetNGramCollection(long id)
        {
            return DoGetNGrams()
                .Where(nGram => nGram.Articles.Any(article => article.Id == id))
                .ToArrayAsync();
        }

        public Task<NGram> GetNGram(string ngram)
        {
            return DoGetNGrams()
                    .FirstOrDefaultAsync(value => value.NGramValue.ToLower().Contains(ngram.ToLower()));
        }

        private IQueryable<NGram> DoGetNGrams()
        {
            return dataContext
                       .NGrams
                       .Include(nGram => nGram.Articles);
        }
    }
}
