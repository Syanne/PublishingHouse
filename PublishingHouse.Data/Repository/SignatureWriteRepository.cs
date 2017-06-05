using PublishingHouse.Data.Entities;
using PublishingHouse.Data.Repository.Interface;
using System;
using System.Threading.Tasks;

namespace PublishingHouse.Data.Repository
{
    class SignatureWriteRepository : ISignatureWriteRepository
    {
        DataContext dataContext { get; set; }
        public SignatureWriteRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public Task AddSinature(Signature signature)
        {
            dataContext.Signatures.Add(signature);
            return dataContext.SaveChangesAsync();
        }
    }
}
