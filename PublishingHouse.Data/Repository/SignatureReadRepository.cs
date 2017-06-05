using PublishingHouse.Data.Entities;
using PublishingHouse.Data.Repository.Interface;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PublishingHouse.Data.Repository
{
    class SignatureReadRepository : ISignatureReadRepository
    {
        DataContext dataContext { get; set; }
        public SignatureReadRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public Task<Signature> GetSignature(string signature)
        {
            return DoGetSignatures()
                    .FirstOrDefaultAsync(value => value.Hash.Equals(signature));
        }

        public Task<Signature[]> GetSignatureCollection(long id)
        {
            return DoGetSignatures()
                .Where(signature => signature.Articles.Any(article => article.Id == id))
                .ToArrayAsync();
        }
        private IQueryable<Signature> DoGetSignatures()
        {
            return dataContext
                       .Signatures
                       .Include(s => s.Articles);
        }
    }
}
