using PublishingHouse.Data.Entities;
using PublishingHouse.Data.Repository.Interface;
using System.Threading.Tasks;

namespace PublishingHouse.Data.Repository
{
    internal class NGramsWriteRepository : INGramsWriteRepository
    {
        DataContext dataContext { get; set; }
        public NGramsWriteRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public Task AddNGram(NGram ngram)
        {
            dataContext.NGram.Add(ngram);
            return dataContext.SaveChangesAsync();
        }
    }
}
